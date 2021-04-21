using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1595 {
        public int ConnectTwoGroups(IList<IList<int>> cost) {
            if (cost.Count < 1) {
                return small(cost, cost.Count, cost[0].Count);
            } else {
                return large(cost, cost.Count, cost[0].Count);
            }
        }

        private int small(IList<IList<int>> cost, int n1, int n2) {
            var best = new int[n1 + 1, 1 << n2];
            for (var i = 0; i <= n1; ++i) {
                for (var k = 0; k < 1 << n2; ++k) {
                    best[i, k] = int.MaxValue / 2;
                }
            }
            best[0, 0] = 0;
            for (var i = 1; i <= n1; ++i) {
                for (var k = 0; k < 1 << n2; ++k) {
                    for (var j = 0; j < n2; ++j) {
                        best[i, k | (1 << j)] = Math.Min(best[i, k | (1 << j)], cost[i - 1][j] + best[i - 1, k]);
                        best[i, k | (1 << j)] = Math.Min(best[i, k | (1 << j)], cost[i - 1][j] + best[i, k]);
                    }
                }
            }
            return best[n1, (1 << n2) - 1];
        }

        private int large(IList<IList<int>> cost, int n1, int n2) {
            // Assume we have a matching M. If some vertices are not
            // covered we can add them to the solution by adding adj
            // edges with minimum cost.
            // Let's μ(v) be the minimum edge adjacent to v. In this
            // case solution can be written as
            //   sum{μ(x), x in V} - sum{c(x, y) - μ(x) - μ(y), c(x, y) in M}
            // for some matching M.
            var u1 = new int[n1];
            var u2 = new int[n2];
            for (var i = 0; i < n1; ++i) u1[i] = int.MaxValue;
            for (var i = 0; i < n2; ++i) u2[i] = int.MaxValue;
            for (var i = 0; i < n1; ++i) {
                for (var j = 0; j < n2; ++j) {
                    u1[i] = Math.Min(u1[i], cost[i][j]);
                    u2[j] = Math.Min(u2[j], cost[i][j]);
                }
            }
            var g = new int[n2][];
            for (var j = 0; j < n2; ++j) {
                g[j] = new int[n1];
                for (var i = 0; i < n1; ++i) {
                    var weight = -1 * (cost[i][j] - u1[i] - u2[j]);
                    if (weight >= 0) {
                        g[j][i] = weight;
                    }
                }
            }
            return u1.Sum() + u2.Sum() - new Hungarian(g, n2, n1).match();
        }

        private class Hungarian {
            private readonly int[][] g;
            private readonly int[] mat1;
            private readonly int[] mat2;
            private readonly int[] pot1;
            private readonly int[] pot2;
            private readonly int[] use1;
            private readonly int[] use2;
            private readonly int n1;
            private readonly int n2;

            public Hungarian(int[][] g, int n1, int n2) {
                this.mat1 = new int[n1];
                this.mat2 = new int[n2];
                this.pot1 = new int[n1];
                this.pot2 = new int[n2];
                this.use1 = new int[n1];
                this.use2 = new int[n2];
                this.n1 = n1;
                this.n2 = n2;
                this.g = g;
            }

            public int match() {
                for (var i = 0; i < n1; ++i) mat1[i] = -1;
                for (var i = 0; i < n2; ++i) mat2[i] = -1;
                for (var i = 0; i < n1; ++i) {
                    pot1[i] = g[i].Max();
                }
                for (var i = 0; i < n1; ++i) {
                    if (mat1[i] < 0) {
                        while (!augment(i)) {
                            relabel();
                        }
                    }
                }
                return pot1.Sum() + pot2.Sum();
            }

            private void relabel() {
                var d = int.MaxValue;
                for (var i = 0; i < n1; ++i) {
                    if (use1[i] != 0) {
                        for (var j = 0; j < n2; ++j) {
                            if (use2[j] == 0) {
                                d = Math.Min(d, pot1[i] + pot2[j] - g[i][j]);
                            }
                        }
                    }
                }
                for (var i = 0; i < n1; ++i) if (use1[i] != 0) pot1[i] -= d;
                for (var i = 0; i < n2; ++i) if (use2[i] != 0) pot2[i] += d;
            }

            private bool augment(int k) {
                for (var i = 0; i < n1; ++i) use1[i] = 0;
                for (var i = 0; i < n2; ++i) use2[i] = 0;
                return kuhn(k);
            }

            private bool kuhn(int i) {
                use1[i] = 1;
                for (var j = 0; j < n2; ++j) {
                    if (use2[j] == 0 && pot1[i] + pot2[j] == g[i][j]) {
                        use2[j] = 1;
                        if (mat2[j] == -1 || kuhn(mat2[j])) {
                            mat1[i] = j;
                            mat2[j] = i;
                            return true;
                        }
                    }
                }
                return false;
            }
        }
    }
}
