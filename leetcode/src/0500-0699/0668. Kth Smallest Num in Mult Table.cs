using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0668 {
        public int FindKthNumber(int m, int n, int k) {
            if (m > n) {
                return FindKthNumber(n, m, k);
            }
            int lo = 1, hi = n * m;
            while (lo < hi) {
                var x = (lo + hi) / 2;
                if (count(x, n, m) < k) {
                    lo = x + 1;
                } else {
                    hi = x;
                }
            }
            return lo;
        }

        // count number of pairs less than or equal to x
        private int count(int x, int n, int m) {
            var res = 0;
            for (var a = 1; a <= n; ++a) {
                res += Math.Min(x / a, m);
            }
            return res;
        }
    }
}
