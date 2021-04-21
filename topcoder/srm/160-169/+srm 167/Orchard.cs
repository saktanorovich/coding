using System;
using System.Collections.Generic;
using System.Dynamic;

namespace TopCoder.Algorithm {
    public class Orchard {
        public int[] nextTree(string[] orchard) {
            return nextTree(orchard, orchard.Length, orchard[0].Length);
        }

        private static int[] nextTree(string[] orchard, int n, int m) {
            var dist = new int[n][];
            var best = 0;
            for (var i = 0; i < n; ++i) {
                dist[i] = new int[m];
                for (int j = 0; j < m; ++j) {
                    if (orchard[i][j] == '-') {
                        dist[i][j] = bfs(orchard, n, m, i, j);
                        best = Math.Max(best, dist[i][j]);
                    }
                }
            }
            for (var i = 0; i < n; ++i) {
                for (var j = 0; j < m; ++j) {
                    if (dist[i][j] == best) {
                        return new[] { i + 1, j + 1 };
                    }
                }
            }
            return null;
        }

        private static int bfs(string[] orchard, int n, int m, int ix, int jx) {
            var dist = new int[n + 2, m + 2];
            for (var i = 1; i <= n; ++i) {
                for (var j = 1; j <= m; ++j) {
                    dist[i, j] = int.MaxValue;
                    if (orchard[i - 1][j - 1] == 'T') {
                        dist[i, j] = 0;
                    }
                }
            }
            var iqueue = new Queue<int>();
            var jqueue = new Queue<int>();
            for (var i = 0; i <= n + 1; ++i) {
                for (var j = 0; j <= m + 1; ++j) {
                    if (dist[i, j] == 0) {
                        iqueue.Enqueue(i);
                        jqueue.Enqueue(j);
                    }
                }
            }
            while (iqueue.Count > 0 && jqueue.Count > 0) {
                var curri = iqueue.Dequeue();
                var currj = jqueue.Dequeue();
                for (var k = 0; k < 4; ++k) {
                    var nexti = curri + dx[k];
                    var nextj = currj + dy[k];
                    if (0 <= nexti && nexti <= n + 1) {
                        if (0 <= nextj && nextj <= m + 1) {
                            if (dist[nexti, nextj] > dist[curri, currj] + 1) {
                                dist[nexti, nextj] = dist[curri, currj] + 1;
                                iqueue.Enqueue(nexti);
                                jqueue.Enqueue(nextj);

                            }
                        }
                    }
                }
            }
            return dist[ix + 1, jx + 1];
        }

        private static readonly int[] dx = { -1,  0, +1,  0 };
        private static readonly int[] dy = {  0, -1,  0, +1 };
    }
}