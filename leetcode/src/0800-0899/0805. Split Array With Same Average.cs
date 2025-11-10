using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0805 {
        public bool SplitArraySameAverage(int[] nums) {
            if (nums.Length < 2) {
                return false;
            }
            return SplitArraySameAverage(nums, nums.Length);
        }

        private bool SplitArraySameAverage(int[] a, int n) {
            var s = a.Sum();
            if (s == 0) {
                return true;
            }
            var h = new int[s + 1];
            h[0] = 1;
            for (var i = 0; i < n; ++i) {
                for (var t = s - a[i]; t >= 0; --t) {
                    h[t + a[i]] |= h[t] << 1;
                }
            }
            // from s1 * n2 = s2 * n1 we can easily get that s1 * n = s * n1
            for (var s1 = 0; s1 <= s; ++s1) {
                if (s1 * n % s == 0) {
                    var n1 = s1 * n / s;
                    if (n1 > 0 && n1 < n) {
                        if ((h[s1] & (1 << n1)) != 0) {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
