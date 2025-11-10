using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0546 {
        public int RemoveBoxes(int[] boxes) {
            if (boxes == null || boxes.Length == 0) {
                return 0;
            }
            return RemoveBoxes(boxes, boxes.Length);
        }

        private int RemoveBoxes(int[] a, int n) {
            // dp[i, j, k] is an optimal score we can reach on subarray [i, j]
            // by removing some of the boxes and getting k boxes of color a[j]
            var dp = new int[n, n, n + 1];
            for (var i = 0; i < n; ++i) {
                for (var k = 0; k <= n; ++k) {
                    dp[i, i, k] = k * k;
                }
            }
            for (var l = 1; l < n; ++l) {
                for (var j = l; j < n; ++j) {
                    var i = j - l;
                    for (var k = 1; k <= l; ++k) {
                        for (var x = i; x < j; ++x) {
                            if (a[x] == a[j]) {
                                dp[i, j, k] = Math.Max(dp[i, j, k], dp[i, x, k - 1] + dp[x, j, 0]);
                            }
                        }
                    }
                }
            }
            return dp[0, n - 1, 0];
        }
    }
}
