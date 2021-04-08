using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0887 {
        public int SuperEggDrop(int K, int N) {
            if (N < 1) {
                return small(K, N);
            } else {
                return large(K, N);
            }
        }

        private int large(int K, int N) {
            var F = new int[N + 1, K + 1];
            for (var n = 1; n <= N; ++n) {
                F[n, 0] = +oo;
            }
            for (var n = 1; n <= N; ++n) {
                for (var k = 1; k <= K; ++k) {
                    int lo = 1, hi = n;
                    while (hi - lo > 1) {
                        var x = (lo + hi) / 2;
                        if (F[x - 1, k - 1] < F[n - x, k]) {
                            lo = x;
                            continue;
                        }
                        if (F[x - 1, k - 1] > F[n - x, k]) {
                            hi = x;
                            continue;
                        }
                        lo = x;
                        hi = x;
                    }
                    var fLo = Math.Max(F[lo - 1, k - 1], F[n - lo, k]);
                    var fHi = Math.Max(F[hi - 1, k - 1], F[n - hi, k]);
                    F[n, k] = Math.Min(fLo, fHi) + 1;
                }
            }
            return F[N, K];
        }

        private int small(int K, int N) {
            var F = new int[N + 1, K + 1];
            for (var n = 1; n <= N; ++n) {
                F[n, 0] = +oo;
            }
            for (var n = 1; n <= N; ++n) {
                for (var k = 1; k <= K; ++k) {
                    F[n, k] = +oo;
                    for (var x = 1; x <= n; ++x) {
                        F[n, k] = Math.Min(F[n, k], Math.Max(F[x - 1, k - 1], F[n - x, k]) + 1);
                    }
                }
            }
            return F[N, K];
        }

        private static readonly int oo = 1000000;
    }
}
