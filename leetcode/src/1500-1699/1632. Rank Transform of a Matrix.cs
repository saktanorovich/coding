using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1632 {
        public int[][] MatrixRankTransform(int[][] matrix) {
            return doit(matrix, matrix.Length, matrix[0].Length);
        }

        private int[][] doit(int[][] a, int n, int m) {
            var vals = new SortedDictionary<int, List<int[]>>();
            var rank = new int[n][];
            var rmax = new int[n];
            var cmax = new int[m];
            for (var i = 0; i < n; ++i) {
                rank[i] = new int[m];
                for (var j = 0; j < m; ++j) {
                    vals.TryAdd(a[i][j], new List<int[]>());
                    vals[a[i][j]].Add(new int[] { i, j });
                }
            }
            foreach (var k in vals.Keys) {
                var sets = join(vals[k], n, m);
                foreach (var set in sets) {
                    var r = 0;
                    var c = 0;
                    foreach (var p in set) {
                        var i = p[0];
                        var j = p[1];
                        r = Math.Max(r, rmax[i]);
                        c = Math.Max(c, cmax[j]);
                    }
                    foreach (var p in set) {
                        var i = p[0];
                        var j = p[1];
                        rank[i][j] = Math.Max(r, c) + 1;
                        rmax[i] = Math.Max(rmax[i], rank[i][j]);
                        cmax[j] = Math.Max(cmax[j], rank[i][j]);
                    }
                }
            }
            return rank;
        }

        private List<List<int[]>> join(List<int[]> ps, int n, int m) {
            var dsu = new DSU(n + m);
            foreach (var p in ps) {
                var x = p[0];
                var y = p[1];
                dsu.join(x, y + n);
            }
            var res = new Dictionary<int, List<int[]>>();
            foreach (var p in ps) {
                var k = dsu.find(p[0]);
                res.TryAdd(k, new List<int[]>());
                res[k].Add(p);
            }
            return res.Values.ToList();
        }

        private sealed class DSU {
            private readonly int[] leader;
            private readonly Random rand;

            public DSU(int n) {
                leader = new int[n];
                for (var i = 0; i < n; ++i) {
                    leader[i] = i;
                }
                rand = new Random();
            }

            public void join(int a, int b) {
                a = find(a);
                b = find(b);
                if (a != b) {
                    if (rand.Next() % 2 == 0) {
                        leader[a] = b;
                    } else {
                        leader[b] = a;
                    }
                }
            }

            public int find(int a) {
                if (a == leader[a]) {
                    return a;
                }
                return leader[a] = find(leader[a]);
            }
        }
    }
}
