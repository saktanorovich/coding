using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1799 {
        public int MaxScore(int[] nums) {
            return MaxScore(nums, nums.Length / 2);
        }

        private static int MaxScore(int[] a, int n) {
            var gcd = new int[2 * n, 2 * n];
            for (var i = 0; i < 2 * n; ++i) {
                for (var j = i + 1; j < 2 * n; ++j) {
                    gcd[i, j] = MathUtils.GCD(a[i], a[j]);
                }
            }
            return MaxScore(gcd, a, 1 << (2 * n), n);
        }

        private static int MaxScore(int[,] gcd, int[] a, int u, int n) {
            var bits = MathUtils.Bits(u);
            var best = new int[u];
            for (var curr = 0; curr < u; ++curr) {
                if (MathUtils.Odd(bits[curr]) == false) {
                    var level = n - bits[curr] / 2;
                    for (var i = 0; i < 2 * n; ++i) {
                        for (var j = i + 1; j < 2 * n; ++j) {
                            if (MathUtils.Has(curr, i) == false && MathUtils.Has(curr, j) == false) {
                                var next = MathUtils.Include(curr, i, j);
                                var cost = best[curr] + level * gcd[i, j];
                                if (best[next] < cost) {
                                    best[next] = cost;
                                }
                            }
                        }
                    }
                }
            }
            return best[u - 1];
        }

        private static class MathUtils {
            public static bool Odd(int set) {
                return (set & 1) == 1;
            }

            public static bool Has(int set, int x) {
                return (set & (1 << x)) != 0;
            }

            public static int[] Bits(int u) {
                var bits = new int [u];
                for (var i = 0; i < u; ++i) {
                    bits[i] = bits[i >> 1] + (i & 1);
                }
                return bits;
            }

            public static int Include(int set, params int[] index) {
                foreach (var i in index) {
                    set |= 1 << i;
                }
                return set;
            }

            public static int GCD(int x, int y) {
                while (x != 0 && y != 0) {
                    if (x > y) {
                        x %= y;
                    } else {
                        y %= x;
                    }
                }
                return x + y;
            }
        }
    }
}
