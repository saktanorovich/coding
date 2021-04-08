using System;
using System.Collections.Generic;
using System.Text;

namespace coding.leetcode {
    public class Solution_0001 {
        public int[] TwoSum(int[] nums, int target) {
            var map = new Dictionary<int, int>();
            for (var i = 0; i < nums.Length; ++i) {
                if (map.TryGetValue(target - nums[i], out var j)) {
                    return new [] { i, j };
                }
                map[nums[i]] = i;
            }
            throw new InvalidOperationException();
        }
    }
}
