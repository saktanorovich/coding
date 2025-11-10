using System;

namespace TopCoder.Algorithm {
    public class RecursiveGraph {
        public int shortestPath(string[] edges, char start, char end) {
            return shortestPath(parse(edges), start - 'A', end - 'A');
        }

        private static int shortestPath(int[,] graph, int a, int b) {
            var shortest = shortestPath(graph, 50);
            if (shortest[a, b] < (int)1e9) {
                return shortest[a, b];
            }
            return -1;
        }

        private static int[,] shortestPath(int[,] graph, int depth) {
            if (depth > 0) {
                var shortest = shortestPath(reduce(graph), depth - 1);
                for (var l = 1; l < 10; ++l)
                    for (var i = 0; i < 10; ++i)
                        for (var j = 0; j < 10; ++j) {
                            graph[l * 10 + i, l * 10 + j] = shortest[i, j];
                            graph[l * 10 + j, l * 10 + i] = shortest[j, i];
                        }
            }
            return floyd(graph);
        }

        private static int[,] reduce(int[,] graph) {
            var res = (int[,])graph.Clone();
            for (var i = 0; i < 100; ++i)
                for (var j = 0; j < 100; ++j) {
                    if (graph[i, j] < (int)1e9) {
                        res[i, j] = graph[i, j] / 2;
                    }
                }
            return res;
        }

        private static int[,] floyd(int[,] graph) {
            var res = (int[,])graph.Clone();
            for (var k = 0; k < 100; ++k)
                for (var i = 0; i < 100; ++i)
                    for (var j = 0; j < 100; ++j) {
                        res[i, j] = Math.Min(res[i, j], res[i, k] + res[k, j]);
                    }
            return res;
        }

        private static int[,] parse(string[] edges) {
            var graph = new int[100, 100];
            for (var i = 0; i < 100; ++i) {
                for (var j = 0; j < 100; ++j) {
                    graph[i, j] = (int)1e9;
                    graph[j, i] = (int)1e9;
                }
            }
            foreach (var edge in edges) {
                var data = edge.Split(' ');
                var a = parse(data[0]);
                var b = parse(data[1]);
                graph[a, b] = int.Parse(data[2]);
                graph[b, a] = int.Parse(data[2]);
            }
            return graph;
        }

        private static int parse(string v) {
            if (v.Length > 1) {
                return 10 * int.Parse(v.Substring(1)) + (v[0] - 'A');
            }
            return v[0] - 'A';
        }
    }
}