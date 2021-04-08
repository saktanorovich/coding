using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0410 {
        public int SplitArray(int[] nums, int m) {
            return SplitArray(nums, nums.Length, m);
        }

        private int SplitArray(int[] a, int n, int m) {
            for (var i = 1; i < n; ++i) {
                a[i] += a[i - 1];
            }
            var f = new int[n, m + 1];
            for (var i = 0; i < n; ++i) {
                f[i, 1] = sum(a, 0, i);
            }
            for (var i = 0; i < n; ++i) {
                for (var k = 2; k <= m; ++k) {
                    f[i, k] = int.MaxValue;
                }
            }
            for (var i = 0; i < n; ++i) {
                for (var k = 2; k <= m; ++k) {
                    for (var j = i - 1; j >= 0; --j) {
                        f[i, k] = Math.Min(f[i, k], Math.Max(sum(a, j + 1, i), f[j, k - 1]));
                    }
                }
            }
            return f[n - 1, m];
        }

        private int sum(int[] a, int i, int j) {
            if (i > 0) {
                return a[j] - a[i - 1];
            } else {
                return a[j];
            }
        }
    }
}
