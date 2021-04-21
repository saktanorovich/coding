using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0440 {
        public int FindKthNumber(int n, int k) {
            var x = 1;
            for (long z = k; z > 1;) {
                var c = count(n, x);
                if (c >= z) {
                    // move into subtree in digits tree
                    x = x * 10;
                    z = z - 1;
                } else {
                    // move into sibling in digits tree
                    x = x + 1;
                    z = z - c;
                }
            }
            return x;
        }

        // count numbers less than or equal to n with given prefix p
        private static long count(int n, long p) {
            var c = 0L;
            var t = 1L;
            while (p <= n) {
                c += Math.Min(p + t - 1, n) - p + 1;
                p *= 10;
                t *= 10;
            }
            return c;
        }
    }
}
