using System;
using System.Collections.Generic;
using System.Text;

namespace coding.leetcode {
    public class Solution_0629 {
        public int KInversePairs(int n, int k) {
            if (n * k < 1) {
                return new Small().KInversePairs(n, k);
            } else {
                return new Large().KInversePairs(n, k);
            }
        }

        private static readonly int MOD = (int)1e9 + 7;

        private class Small {
            public int KInversePairs(int n, int k) {
                var f = new int[n + 1, k + 1];
                f[0, 0] = 1;
                for (var i = 1; i <= n; ++i) {
                    for (var j = 0; j <= k; ++j) {
                        for (var t = 0; t < i; ++t) {
                            if (j >= t) {
                                f[i, j] += f[i - 1, j - t];
                                f[i, j] %= MOD;
                            } else {
                                break;
                            }
                        }
                    }
                }
                return f[n, k];
            }
        }

        private class Large {
            public int KInversePairs(int n, int k) {
                var f = new int[n + 1, k + 1];
                f[0, 0] = 1;
                for (var i = 1; i <= n; ++i) {
                    f[i, 0] = 1;
                    for (var j = 1; j <= k; ++j) {
                        f[i, j] += f[i - 1, j];
                        f[i, j] %= MOD;
                        f[i, j] += f[i, j - 1];
                        f[i, j] %= MOD;
                        if (j >= i) {
                            f[i, j] -= f[i - 1, j - i];
                            f[i, j] += MOD;
                            f[i, j] %= MOD;
                        }
                    }
                }
                return f[n, k];
            }
        }
    }
}
