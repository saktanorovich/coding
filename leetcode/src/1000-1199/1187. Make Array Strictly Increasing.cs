using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1187 {
        public int MakeArrayIncreasing(int[] a, int[] b) {
            var bs = new List<int>();
            bs.Add(int.MaxValue);
            foreach (var x in new SortedSet<int>(b)) {
                bs.Add(x);
            }
            return make(a, bs.ToArray());
        }

        private int make(int[] a, int[] b) {
            var n = a.Length - 1;
            var m = b.Length - 1;
            var dp = new int[n + 1, m + 1, 2];
            for (var j = 0; j <= m; ++j) {
                for (var i = 1; i <= n; ++i) {
                    dp[i, j, 0] = int.MaxValue / 2;
                    dp[i, j, 1] = int.MaxValue / 2;
                }
                dp[0, j, 0] = 0;
                dp[0, j, 1] = 1;
            }
            for (var i = 1; i <= n; ++i) {
                for (var j = 1; j <= m; ++j) {
                    if (a[i] > a[i - 1]) {
                        dp[i, j, 0] = Math.Min(dp[i, j, 0], dp[i - 1, j, 0]);
                    }
                    if (b[j] > a[i - 1]) {
                        dp[i, j, 1] = Math.Min(dp[i, j, 1], dp[i - 1, j - 1, 0] + 1);
                    }
                    if (b[j] > b[j - 1]) {
                        dp[i, j, 1] = Math.Min(dp[i, j, 1], dp[i - 1, j - 1, 1] + 1);
                    }
                }
                for (var k = m; k > 0; --k) {
                    if (a[i] > b[k]) {
                        for (var j = k; j <= m; ++j) {
                            dp[i, j, 0] = Math.Min(dp[i, j, 0], dp[i - 1, k, 1]);
                        }
                        break;
                    }
                }
            }
            var res = int.MaxValue / 2;
            for (var k = 1; k <= m; ++k) {
                res = Math.Min(res, dp[n, k, 0]);
                res = Math.Min(res, dp[n, k, 1]);
            }
            if (res < int.MaxValue / 2) {
                return res;
            } else {
                return -1;
            }
        }
    }
}
