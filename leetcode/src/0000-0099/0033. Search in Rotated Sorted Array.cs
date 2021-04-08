using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0033 {
        public int Search(int[] nums, int target) {
            if (nums == null || nums.Length == 0) {
                return -1;
            }
            return Search(nums, nums.Length, target);
        }

        private int Search(int[] nums, int n, int target) {
            var xMin = nums[0];
            var xMax = nums[n - 1];
            if (xMin <= xMax) {
                return Search(nums, 0, n - 1, target);
            } else {
                int lo = 0, hi = n - 1;
                while (lo < hi) {
                    var x = (lo + hi) / 2;
                    if (nums[x] < xMax) {
                        hi = x;
                    } else {
                        lo = x + 1;
                    }
                }
                var l = Search(nums, 0, lo - 1, target);
                var r = Search(nums, lo, n - 1, target);
                return Math.Max(l, r);
            }
        }

        private int Search(int[] nums, int lo, int hi, int target) {
            while (lo <= hi) {
                var x = (lo + hi) / 2;
                if (nums[x] == target) {
                    return x;
                }
                if (nums[x] < target) {
                    lo = x + 1;
                } else {
                    hi = x - 1;
                }
            }
            return -1;
        }
    }
}
