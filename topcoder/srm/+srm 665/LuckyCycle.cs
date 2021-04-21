using System;

namespace TopCoder.Algorithm {
    public class LuckyCycle {
        public int[] getEdge(int[] edge1, int[] edge2, int[] weight) {
            var graph = new int[weight.Length + 1, weight.Length + 1];
            for (var i = 0; i < weight.Length; ++i) {
                graph[edge1[i] - 1, edge2[i] - 1] = weight[i];
                graph[edge2[i] - 1, edge1[i] - 1] = weight[i];
            }
            return getEdge(graph, weight.Length + 1);
        }

        private static int[] getEdge(int[,] graph, int n) {
            for (var a = 0; a < n; ++a) {
                for (var b = a + 1; b < n; ++b) {
                    if (graph[a, b] == 0) {
                        for (var w = 0; w < 2; ++w) {
                            var cnt = new[] { 1 - w, w };
                            dfs(graph, n, a, b, -1, cnt);
                            if (cnt[0] == cnt[1]) {
                                return new[] { a + 1, b + 1, 3 * w + 4 };
                            }
                        }
                    }
                }
            }
            return new int[0];
        }

        private static bool dfs(int[,] graph, int n, int src, int dst, int prv, int[] cnt) {
            if (src == dst) {
                return true;
            }
            for (var nxt = 0; nxt < n; ++nxt) {
                if (graph[src, nxt] > 0) {
                    if (nxt != prv) {
                        ++cnt[graph[src, nxt] / 5];
                        if (dfs(graph, n, nxt, dst, src, cnt)) {
                            return true;
                        }
                        --cnt[graph[src, nxt] / 5];
                    }
                }
            }
            return false;
        }
    }
}