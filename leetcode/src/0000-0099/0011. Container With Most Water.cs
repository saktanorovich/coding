using System;
using System.Collections.Generic;
using System.Text;

namespace coding.leetcode {
    public class Solution_0011 {
        public int MaxArea(int[] height) {
            var res = 0;
            var le = 0;
            var ri = height.Length - 1;
            while (le < ri) {
                if (height[le] < height[ri]) {
                    res = Math.Max(res, height[le] * (ri - le));
                } else {
                    res = Math.Max(res, height[ri] * (ri - le));
                }
                if (height[le] < height[ri]) {
                    ++le;
                } else {
                    --ri;
                }
            }
            return res;
        }
    }
}
