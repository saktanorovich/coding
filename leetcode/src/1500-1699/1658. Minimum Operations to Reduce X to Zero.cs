using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1658 {
        public int MinOperations(int[] nums, int X) {
            var S = nums.Sum();
            if (S == X) {
                return nums.Length;
            }
            var M = new Dictionary<int, int> {
                { 0, 0 }
            };
            var T = 0;
            var B = 0;
            for (var i = 0; i < nums.Length; ++i) {
                T += nums[i];
                if (M.TryGetValue(T - S + X, out var j)) {
                    B = Math.Max(B, i - j + 1);
                }
                M.TryAdd(T, i + 1);
            }
            return B > 0 ? nums.Length - B : -1;
        }
    }
}
