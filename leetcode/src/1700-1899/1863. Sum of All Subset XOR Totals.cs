using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1863 {
        public int SubsetXORSum(int[] nums) {
            var xor = new int[1 << nums.Length];
            for (var i = 0; i < nums.Length; ++i) {
                xor[1 << i] = nums[i];
            }
            for (var set = 1; set < 1 << nums.Length; ++set) {
                var subset = set & (set - 1);
                if (subset > 0) {
                    xor[set] = xor[subset] ^ xor[set ^ subset];
                }
            }
            return xor.Sum();
        }
    }
}
