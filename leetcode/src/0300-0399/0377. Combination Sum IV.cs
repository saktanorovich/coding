using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0377 {
        public int CombinationSum4(int[] nums, int target) {
            var count = new int[target + 1];
            count[0] = 1;
            for (var s = 1; s <= target; ++s) {
                foreach (var x in nums) {
                    if (s >= x) {
                        count[s] += count[s - x];
                    }
                }
            }
            return count[target];
        }
    }
}
