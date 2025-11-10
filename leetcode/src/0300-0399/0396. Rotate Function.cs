using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0396 {
        public int MaxRotateFunction(int[] nums) {
            var sum = 0L;
            var sub = 0L;
            for (var i = 0; i < nums.Length; ++i) {
                sum += +nums[i] * i;
                sub += -nums[i];
            }
            var max = sum;
            for (var i = 1; i < nums.Length; ++i) {
                sum += sub + 1L * nums[i - 1] * nums.Length;
                if (max < sum) {
                    max = sum;
                }
            }
            return (int)max;
        }
    }
}
