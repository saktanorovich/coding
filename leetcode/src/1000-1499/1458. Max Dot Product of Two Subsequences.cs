using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1458 {
        public int MaxDotProduct(int[] nums1, int[] nums2) {
            return MaxDotProduct(nums1, nums1.Length, nums2, nums2.Length);
        }

        private int MaxDotProduct(int[] nums1, int n1, int[] nums2, int n2) {
            var memo = new int?[n1, n2];
            for (var i = 0; i < n1; ++i) {
                for (var j = 0; j < n2; ++j) {
                    memo[i, j] = null;
                }
            }
            return MaxDotProduct(memo, nums1, n1 - 1, nums2, n2 - 1);
        }

        private int MaxDotProduct(int?[,] memo, int[] nums1, int n1, int[] nums2, int n2) {
            if (n1 < 0 || n2 < 0) {
                return int.MinValue / 2;
            }
            if (memo[n1, n2].HasValue) {
                return memo[n1, n2].Value;
            }
            var t1 = MaxDotProduct(memo, nums1, n1, nums2, n2 - 1);
            var t2 = MaxDotProduct(memo, nums1, n1 - 1, nums2, n2);
            var t3 = MaxDotProduct(memo, nums1, n1 - 1, nums2, n2 - 1);
            memo[n1, n2] = new[] { t1, t2, t3, Math.Max(t3, 0) + nums1[n1] * nums2[n2] }.Max();
            return memo[n1, n2].Value;
        }
    }
}
