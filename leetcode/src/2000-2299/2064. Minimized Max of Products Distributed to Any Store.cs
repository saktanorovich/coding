using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_2064 {
        public int MinimizedMaximum(int n, int[] quantities) {
            int lo = 1, hi = int.MaxValue / 2;
            while (lo < hi) {
                var opt = (lo + hi) / 2;
                var has = n;
                foreach (var qua in quantities) {
                    has -= (qua + opt - 1) / opt;
                    if (has < 0) {
                        break;
                    }
                }
                if (has < 0) {
                    lo = opt + 1;
                } else {
                    hi = opt;
                }
            }
            return lo;
        }
    }
}