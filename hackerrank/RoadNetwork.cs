/**
Fedor is a research scientist, who has recently found a road map
of Ancient Berland. Ancient Berland consisted of N cities that were
connected by M bidirectional roads. The road builders weren't knowledgable.
Hence, the start city and the end city for each road were always
chosen randomly and independently. As a result, there were more
than one road between some pairs of cities. Nevertheless, by luck,
the country remained connected (i.e. you were able to get from one city
to another via these M roads). And for any road, the start and the end
city were not the same.

Moreover, each road had it's own value of importance. This value was assigned by the
Road Minister of Ancient Berland. The Road Minister also was not knowledgable, so these
numbers were assigned to the roads randomly and independently from the other roads.

When there was a war with the neighboring countries (usually it was with Ancient Herland),
it was important to estimate separation number for some pairs of cities.

The separation number for a pair of cities - let's call these cities A and B - is explained below:

Consider a set of roads that were built. The subset of this set is good, if after removing
all roads from this set, there's no longer a way from A to B. The minimal possible sum of
roads' value of importance of any good subset is a separation number for the pair of cities (A, B).

For a research, Fedor would like to know the product of separation values over all
unordered pairs of cities. Please, find this number. It can be huge, so we ask you
to output its product modulo 10^9+7.

Input Format

The first line of input consist of two integers N and M, separated by a single space. Then, M lines
follow. Each of these lines consist of three integers Xi, Yi, Zi separated by a single space. It means
that there was a road between the city Xi and the city Yi with a value of importance equal to Zi.

Constraints
3 ≤ N ≤ 500
3 ≤ M ≤ 10'000
1 ≤ value of importance ≤ 100'000
The cities are indexed from 1 to N.

Output Format

An integer that represents the value, Fedor needs, modulo 10^9+7.

Sample Input
3 3
1 2 3
2 3 1
1 3 2

Sample Output
36

Explanation
There are three unordered pairs of cities: (1,2), (1,3) and (2,3).
Let's look at the separation numbers:

For (1,2) we have to remove 1st and 2nd roads. The sum of the importance values is 4.
For (1,3) we have to remove 2nd and 3rd roads. The sum of the importance values is 3.
For (2,3) we have to remove 2nd and 3rd roads. The sum of the importance values is 3.

So we get 4*3*3 = 36.

Scoring
In the 25% of the test data N = 50 and M = 300.
In another 25% of the test data N = 200 and M = 10'000
In the rest of the test data N = 500 and M = 10'000
*/
using System;
using System.Collections.Generic;
using System.IO;

namespace interview.hackerrank {
    public class RoadNetwork {
        public int separationValue(TextReader reader) {
            var buf = reader.ReadLine().Split(' ');
            var n = int.Parse(buf[0]);
            var m = int.Parse(buf[1]);
            var x = new int[m];
            var y = new int[m];
            var z = new int[m];
            for (var i = 0; i < m; ++i) {
                buf = reader.ReadLine().Split(' ');
                x[i] = int.Parse(buf[0]) - 1;
                y[i] = int.Parse(buf[1]) - 1;
                z[i] = int.Parse(buf[2]);
            }
            return separationValue(x, y, z, n, m);
        }

        public int separationValue(int[] x, int[] y, int[] z, int n, int m) {
            // the problem is related to finding minimum cuts between all pairs of vertices
            var graph = new List<int>[n];
            for (var i = 0; i < n; ++i) {
                graph[i] = new List<int>();
            }
            var capacity = new int[n, n];
            for (var i = 0; i < m; ++i) {
                graph[x[i]].Add(y[i]);
                graph[y[i]].Add(x[i]);
                capacity[x[i], y[i]] = z[i];
                capacity[y[i], x[i]] = z[i];
            }

            // build flow tree (not necessary cut tree) using Gusfield D. algorithm (Gomory and Hu showed
            // that the number of distinct cuts in the graph is at most n−1)
            var cut = new int[n, n];
            for (var i = 0; i < n; ++i) {
                for (var j = 0; j < n; ++j) {
                    cut[i, j] = int.MaxValue;
                }
            }
            var parent = new int[n];
            for (int source = 1, maxf; source < n; ++source) {
                var flow = maxFlow(graph, capacity, source, parent[source], out maxf);

                var component = reach(graph, capacity, flow, source, new bool[n], new List<int>());
                foreach (var node in component) {
                    if (node != source && node > source) {
                        if (parent[node] == parent[source]) {
                            parent[node] = source;
                        }
                    }
                }

                cut[source, parent[source]] = maxf;
                cut[parent[source], source] = maxf;
                for (var node = 0; node < source; ++node) {
                    cut[source, node] = cut[node, source] = Math.Min(maxf, cut[parent[source], node]);
                }
            }

            // find graph minimum-cut product
            var result = 1L;
            for (var i = 0; i < n; ++i)
                for (var j = i + 1; j < n; ++j) {
                    result *= cut[i, j];
                    result %= modulo;
                }
            return (int)result;
        }

        private List<int> reach(List<int>[] graph, int[,] capacity, int[,] flow, int source, bool[] visited, List<int> component) {
            visited[source] = true;
            component.Add(source);
            foreach (var next in graph[source]) {
                if (!visited[next]) {
                    if (capacity[source, next] > flow[source, next]) {
                        reach(graph, capacity, flow, next, visited, component);
                    }
                }
            }
            return component;
        }

        /** Dinic algorithm */
        private int[,] maxFlow(List<int>[] graph, int[,] capacity, int source, int target, out int maxf) {
            var flow = new int[graph.Length, graph.Length];
            var dist = new int[graph.Length];
            for (maxf = 0; bfs(graph, capacity, flow, dist, source, target);) {
                for (var idx = new int[graph.Length];;) {
                    var pushed = dfs(graph, idx, capacity, flow, dist, source, target, int.MaxValue);
                    if (pushed > 0) {
                        maxf += pushed;
                    }
                    else break;
                }
            }
            return flow;
        }

        private bool bfs(List<int>[] graph, int[,] capacity, int[,] flow, int[] distance, int source, int target) {
            for (var i = 0; i < graph.Length; ++i) {
                distance[i] = -1;
            }
            var queue = new Queue<int>();
            for (distance[source] = 0, queue.Enqueue(source); queue.Count > 0;) {
                var current = queue.Dequeue();
                foreach (var next in graph[current])
                    if (distance[next] == -1 && capacity[current, next] > flow[current, next]) {
                        distance[next] = distance[current] + 1;
                        queue.Enqueue(next);
                    }
            }
            return distance[target] != -1;
        }

        private int dfs(List<int>[] graph, int[] idx, int[,] capacity, int[,] flow, int[] distance, int source, int target, int residue) {
            if (source == target) {
                return residue;
            }
            for (; idx[source] < graph[source].Count; ++idx[source]) {
                var next = graph[source][idx[source]];
                if (distance[next] != distance[source] + 1) {
                    continue;
                }
                if (capacity[source, next] > flow[source, next]) {
                    var pushed = dfs(graph, idx, capacity, flow, distance, next, target, Math.Min(residue, capacity[source, next] - flow[source, next]));
                    if (pushed > 0) {
                        flow[source, next] += pushed;
                        flow[next, source] -= pushed;
                        return pushed;
                    }
                }
            }
            return 0;
        }

        /** Edmonds-Karp algorithm
        private int[,] maxFlow(List<int>[] graph, int[,] capacity, int source, int target, out int maxf) {
            var flow = new int[graph.Length, graph.Length];
            var prev = new int[graph.Length];
            for (maxf = 0; augment(graph, capacity, flow, prev, source, target);)
            {
                var by = int.MaxValue;
                for (var current = target; current != source;) {
                    var residue = capacity[prev[current], current] - flow[prev[current], current];
                    by = Math.Min(by, residue);
                    current = prev[current];
                }
                for (var current = target; current != source;) {
                    flow[prev[current], current] += by;
                    flow[current, prev[current]] -= by;
                    current = prev[current];
                }
                maxf += by;
            }
            return flow;
        }

        private bool augment(List<int>[] graph, int[,] capacity, int[,] flow, int[] prev, int source, int target) {
            for (var i = 0; i < graph.Length; ++i) {
                prev[i] = -1;
            }
            var queue = new Queue<int>();
            for (queue.Enqueue(source); queue.Count > 0;) {
                var current = queue.Dequeue();
                foreach (var next in graph[current]) {
                    if (prev[next] == -1) {
                        if (capacity[current, next] > flow[current, next]) {
                            prev[next] = current;
                            queue.Enqueue(next);
                        }
                    }
                }
            }
            return prev[target] != -1;
        }
        */

        private const long modulo = (long)1e9 + 7;
    }
}
