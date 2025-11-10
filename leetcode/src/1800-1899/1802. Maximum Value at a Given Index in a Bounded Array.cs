using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1802 {
        public int MaxValue(int n, int index, int maxSum) {
            int lo = 1, hi = maxSum;
            while (lo < hi) {
                var x = (lo + hi + 1) / 2;
                var s = (long)x;
                s += sum(x - 1, index);
                s += sum(x - 1, n - index - 1);
                if (s > maxSum) {
                    hi = x - 1;
                } else {
                    lo = x;
                }
            }
            return lo;
        }

        private long sum(int x, int n) {
            if (x <= n) {
                return 1L * x * (x + 1) / 2 + 1L * (n - x);
            } else {
                return 1L * n * (n + 1) / 2 + 1L * (x - n) * n;
            }
        }
    }
}
