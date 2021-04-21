using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0453 {
        public int MinMoves(int[] nums) {
            // if we sort nums the answer will be:
            //   nums[1] - nums[0] +
            //   nums[2] - nums[0] +
            //   nums[3] - nums[0] + ...
            return nums.Sum() - nums.Min() * nums.Length;
        }
    }
}