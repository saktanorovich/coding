using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0228 {
        public IList<string> SummaryRanges(int[] nums) {
            var res = new List<string>();
            var lo = 0;
            var hi = 0;
            for (; lo < nums.Length; lo = hi) {
                for (hi = lo + 1; hi < nums.Length; ++hi) {
                    if (nums[hi - 1] + 1 < nums[hi]) {
                        break;
                    }
                }
                res.Add(make(nums[lo], nums[hi - 1]));
            }
            return res;
        }

        private string make(int x, int y) {
            if (x < y) {
                return $"{x}->{y}";
            } else {
                return $"{x}";
            }
        }
    }
}
