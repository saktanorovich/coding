using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0041 {
        public int FirstMissingPositive(int[] nums) {
            return FirstMissingPositive(nums, nums.Length);
        }

        private int FirstMissingPositive(int[] nums, int n) {
            // the answer is always belong to [1, n + 1]
            for (var i = 0; i < nums.Length; ++i) {
                if (1 <= nums[i] && nums[i] <= n) {
                    while (nums[i] - 1 != i) {
                        var x = i;
                        var y = nums[i] - 1;
                        if (nums[x] == nums[y]) {
                            nums[x] = 0;
                            break;
                        }
                        var t = nums[x];
                        nums[x] = nums[y];
                        nums[y] = t;
                        if (nums[i] < 1) break;
                        if (nums[i] > n) break;
                    }
                }
                if (nums[i] < 1) nums[i] = 0;
                if (nums[i] > n) nums[i] = 0;
            }
            for (var i = 0; i < nums.Length; ++i) {
                if (nums[i] == 0) {
                    return i + 1;
                }
            }
            return n + 1;
        }
    }
}
