using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0416 {
        public bool CanPartition(int[] nums) {
            if (nums == null || nums.Length < 2) {
                return false;
            }
            var sum = nums.Sum();
            if (sum % 2 == 0) {
                return Can(nums, sum / 2);
            } else {
                return false;
            }
        }

        private bool Can(int[] nums, int target) {
            var f = new bool[target + 1];
            f[0] = true;
            foreach (var x in nums) {
                for (var s = target - x; s >= 0; --s) {
                    f[s + x] |= f[s];
                }
            }
            return f[target];
        }
    }
}
