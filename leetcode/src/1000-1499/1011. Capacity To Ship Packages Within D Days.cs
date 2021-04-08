using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1011 {
        public int ShipWithinDays(int[] weights, int D) {
            int lo = 1, hi = weights.Sum();
            while (lo < hi) {
                var x = (lo + hi) / 2;
                var d = 0;
                var s = 0;
                foreach (var w in weights) {
                    if (w > x) {
                        d = D + 1;
                        break;
                    }
                    if (s >= w) {
                        s -= w;
                    } else {
                        s = x - w;
                        d = d + 1;
                    }
                }
                if (d > D) {
                    lo = x + 1;
                } else {
                    hi = x;
                }
            }
            return lo;
        }
    }
}
