using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1029 {
        public int TwoCitySchedCost(int[][] costs) {
            return TwoCitySchedCost(costs, costs.Length / 2);
        }

        private int TwoCitySchedCost(int[][] cost, int n) {
            // the problem can be solved by the application of min-cost-max-flow
            // algorithm but due to specific network topology let's do faster
            var have = new List<int>[2];
            have[0] = new List<int>();
            have[1] = new List<int>();
            var best = 0;
            for (var i = 0; i < cost.Length; ++i) {
                if (cost[i][0] < cost[i][1]) {
                    have[0].Add(i);
                    best += cost[i][0];
                } else {
                    have[1].Add(i);
                    best += cost[i][1];
                }
            }
            for (var i = 0; i < 2; ++i) {
                if (have[i].Count > n) {
                    have[i].Sort((a, b) => {
                        var u = Math.Abs(cost[a][0] - cost[a][1]);
                        var v = Math.Abs(cost[b][0] - cost[b][1]);
                        return u - v;
                    });
                    foreach (var c in have[i].Take(have[i].Count - n)) {
                        best += Math.Abs(cost[c][0] - cost[c][1]);
                    }
                }
            }
            return best;
        }

        /*
        private int TwoCitySchedCost(int[][] costs, int n) {
            var mcmf = new MCMF(costs.Length + 4);
            for (var i = 0; i < costs.Length; ++i) {
                mcmf.add(0, i + 1, 0, 1);
                mcmf.add(i + 1, costs.Length + 1, costs[i][0], 1);
                mcmf.add(i + 1, costs.Length + 2, costs[i][1], 1);
            }
            mcmf.add(costs.Length + 1, costs.Length + 3, 0, n);
            mcmf.add(costs.Length + 2, costs.Length + 3, 0, n);
            var (flow, cost) = mcmf.get(0, costs.Length + 3);
            return cost;
        }

        private class MCMF {
            private class Edge {
                public readonly int src;
                public readonly int dst;
                public readonly int cost;
                public readonly int capa;
                public int flow;
                public Edge back;

                public Edge(int src, int dst, int cost, int capa) {
                    this.src = src;
                    this.dst = dst;
                    this.cost = cost;
                    this.capa = capa;
                    this.flow = 0;
                }

                public int residual() {
                    return capa - flow;
                }
            }

            private readonly Queue<int> queue;
            private readonly List<Edge>[] graph;
            private readonly bool[] mark;
            private readonly int[] dist;

            public MCMF(int n) {
                graph = new List<Edge>[n];
                for (int i = 0; i < n; ++i) {
                    graph[i] = new List<Edge>();
                }
                queue = new Queue<int>();
                mark = new bool[n];
                dist = new int[n];
            }

            public int flow(int src, int dst) {
                foreach (var edge in graph[src]) {
                    if (edge.dst == dst) {
                        return edge.flow;
                    }
                }
                return 0;
            }

            public void add(int source, int target, int cost, int capa) {
                var e1 = new Edge(source, target, +cost, capa);
                var e2 = new Edge(target, source, -cost, 0);
                e1.back = e2;
                e2.back = e1;
                graph[source].Add(e1);
                graph[target].Add(e2);
            }

            public (int flow, int cost) get(int source, int target) {
                var cost = 0;
                var flow = 0;
                var from = new Edge[graph.Length];
                while (augment(source, target, from)) {
                    var by = int.MaxValue;
                    for (var at = target; at != source; at = from[at].src) {
                        by = Math.Min(by, from[at].residual());
                    }
                    for (var at = target; at != source; at = from[at].src) {
                        var edge = from[at];
                        var back = from[at].back;
                        edge.flow += by;
                        back.flow -= by;
                        cost += by * edge.cost;
                    }
                    flow += by;
                }
                return (flow, cost);
            }

            private bool augment(int source, int target, Edge[] from) {
                if (hasNegativeCycle(source)) {
                    throw new Exception("Negative cycle detected");
                }
                for (var i = 0; i < dist.Length; ++i) {
                    dist[i] = int.MaxValue / 2;
                }
                for (dist[source] = 0, queue.Enqueue(source); queue.Count > 0;) {
                    var at = queue.Dequeue();
                    mark[at] = false;
                    foreach (var edge in graph[at]) {
                        if (edge.residual() > 0) {
                            if (dist[edge.dst] > dist[edge.src] + edge.cost) {
                                dist[edge.dst] = dist[edge.src] + edge.cost;
                                from[edge.dst] = edge;
                                if (mark[edge.dst] == false) {
                                    mark[edge.dst] = true;
                                    queue.Enqueue(edge.dst);
                                }
                            }
                        }
                    }
                }
                return dist[target] < int.MaxValue / 2;
            }

            private bool hasNegativeCycle(int source) {
                for (var i = 0; i < dist.Length; ++i) {
                    dist[i] = int.MaxValue / 2;
                }
                dist[source] = 0;
                for (var i = 0; i <= graph.Length; ++i) {
                    for (var u = 0; u < graph.Length; ++u) {
                        foreach (var edge in graph[u]) {
                            if (edge.residual() > 0) {
                                if (dist[edge.dst] > dist[edge.src] + edge.cost) {
                                    dist[edge.dst] = dist[edge.src] + edge.cost;
                                    if (i >= graph.Length) {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
                return false;
            }
        }
        */
    }
}
