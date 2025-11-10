using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0128 {
        public int LongestConsecutive(int[] nums) {
            return LongestConsecutive(new HashSet<int>(nums));
        }

        private int LongestConsecutive(HashSet<int> nums) {
            var best = 0;
            foreach (var x in nums) {
                if (nums.Contains(x - 1) == false) {
                    var have = 0;
                    for (var y = x; nums.Contains(y); ++y) {
                        have = have + 1;
                    }
                    best = Math.Max(best, have);
                }
            }
            return best;
        }
    }
}
