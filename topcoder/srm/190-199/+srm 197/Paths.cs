using System;

namespace TopCoder.Algorithm {
    public class Paths {
        public int secondBest(string[] graph, int from, int to) {
            return secondBest(Array.ConvertAll(graph, adj => {
                var result = new int[adj.Length];
                for (var i = 0; i < adj.Length; ++i) {
                    result[i] = oo;
                    if (adj[i] != 'X') {
                        result[i] = int.Parse(adj[i].ToString());
                    }
                }
                return result;
            }), graph.Length, from, to);
        }

        private static int secondBest(int[][] graph, int n, int from, int to) {
            var best = floyd_warshall(graph, n);
            var next = +oo;
            for (var a = 0; a < n; ++a)
                for (var b = 0; b < n; ++b) {
                    if (graph[a][b] < +oo) {
                        var eval = best[from, a] + graph[a][b] + best[b, to];
                        if (best[from, to] < eval) {
                            next = Math.Min(next, eval);
                        }
                    }
                }
            if (next < +oo)
                return next;
            return -1;
        }

        private static int[,] floyd_warshall(int[][] graph, int n) {
            var min = new int[n, n];
            for (var i = 0; i < n; ++i) {
                for (var j = i; j < n; ++j) {
                    min[i, j] = graph[i][j];
                    min[j, i] = graph[j][i];
                }
                min[i, i] = 0;
            }
            for (var k = 0; k < n; ++k)
                for (var i = 0; i < n; ++i)
                    for (var j = 0; j < n; ++j) {
                        min[i, j] = Math.Min(min[i, j], min[i, k] + min[k, j]);
                    }
            return min;
        }

        private static readonly int oo = +1000000000;
    }
}