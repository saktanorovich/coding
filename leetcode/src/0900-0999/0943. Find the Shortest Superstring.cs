using System;
using System.Collections.Generic;
using System.Text;

namespace coding.leetcode {
    public class Solution_0943 {
        public string ShortestSuperstring(string[] A) {
            return ShortestSuperstring(A, A.Length);
        }

        private string ShortestSuperstring(string[] a, int n) {
            var g = new int[n, n];
            for (var i = 0; i < n; ++i) {
                for (var j = 0; j < n; ++j) {
                    if (i == j) {
                        continue;
                    }
                    for (var k = Math.Min(a[i].Length, a[j].Length); k > 0; --k) {
                        if (sub(a[j], k) == sub(a[i], -k)) {
                            g[i, j] = k;
                            break;
                        }
                    }
                }
            }
            var f = new string[1 << n, n];
            for (var i = 0; i < n; ++i) {
                f[1 << i, i] = a[i];
            }
            for (var s = 1; s < (1 << n) - 1; ++s) {
                for (var x = 0; x < n; ++x) {
                    if (has(s, x)) {
                        for (var y = 0; y < n; ++y) {
                            if (has(s, y) == false) {
                                f[s | (1 << y), y] = min(f[s | (1 << y), y], f[s, x] + a[y].Substring(g[x, y]));
                            }
                        }
                    }
                }
            }
            var best = (string)null;
            for (var i = 0; i < n; ++i) {
                best = min(best, f[(1 << n) - 1, i]);
            }
            return best;
        }

        private string sub(string a, int k) {
            if (k < 0) {
                return a.Substring(a.Length + k);
            } else {
                return a.Substring(0, k);
            }
        }

        private string min(string a, string b) {
            if (a?.Length < b.Length) {
                return a;
            } else {
                return b;
            }
        }

        private bool has(int set, int x) {
            return (set & (1 << x)) != 0;
        }
    }
}
