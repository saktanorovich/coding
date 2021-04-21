using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0069 {
        public int MySqrt(int x) {
            var lo = 0;
            var hi = 1 << 16;
            while (lo < hi) {
                var r = (lo + hi + 1) / 2;
                if (1L * r * r > x) {
                    hi = r - 1;
                } else {
                    lo = r;
                }
            }
            return lo;
        }
    }
}
