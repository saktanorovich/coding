using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0053 {
        public int MaxSubArray(int[] nums) {
            var h = 0;
            var b = int.MinValue;
            foreach (var x in nums) {
                b = Math.Max(b, h + x);
                h = Math.Max(0, h + x);
            }
            return b;
        }
    }
}
