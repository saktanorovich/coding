using System;
using System.Collections.Generic;
using System.Text;

namespace coding.leetcode {
    public class Solution_0034 {
        public int[] SearchRange(int[] nums, int target) {
            if (nums == null || nums.Length == 0) {
                return new[] { -1, -1 };
            }
            return new[] {
                lSearch(nums, target),
                rSearch(nums, target),
            };
        }

        private int lSearch(int[] nums, int target) {
            int lo = 0, hi = nums.Length - 1;
            while (lo < hi) {
                var x = (lo + hi) / 2;
                if (nums[x] < target) {
                    lo = x + 1;
                } else {
                    hi = x;
                }
            }
            return nums[lo] == target ? lo : -1;
        }

        private int rSearch(int[] nums, int target) {
            int lo = 0, hi = nums.Length - 1;
            while (lo < hi) {
                var x = (lo + hi + 1) / 2;
                if (nums[x] > target) {
                    hi = x - 1;
                } else {
                    lo = x ;
                }
            }
            return nums[hi] == target ? hi : -1;
        }
    }
}