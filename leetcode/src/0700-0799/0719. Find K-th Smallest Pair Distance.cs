using System;
using System.Collections.Generic;
using System.Text;

namespace coding.leetcode {
    public class Solution_0719 {
        public int SmallestDistancePair(int[] nums, int k) {
            return SmallestDistancePair(nums, nums.Length, k);
        }

        private int SmallestDistancePair(int[] nums, int n, int k) {
            Array.Sort(nums, 0, n);
            var lo = 0;
            var hi = nums[n - 1] - nums[0];
            while (lo < hi) {
                var x = (lo + hi) / 2;
                if (count(nums, n, x) < k) {
                    lo = x + 1;
                } else {
                    hi = x;
                }
            }
            return lo;
        }

        // count number of pairs less than or equal to x
        private int count(int[] nums, int n, int x) {
            var r = 0;
            for (int i = 0, j = 0; j < n;) {
                if (nums[j] - nums[i] <= x) {
                    r = r + j - i;
                    j = j + 1;
                } else {
                    i = i + 1;
                }
            }
            return r;
        }
    }
}
