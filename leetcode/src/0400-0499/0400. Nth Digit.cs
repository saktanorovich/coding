using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0400 {
        public int FindNthDigit(int n) {
            var p10 = new long[10];
            p10[0] = 1;
            for (var i = 1; i < 10; ++i) {
                p10[i] = 10 * p10[i - 1];
            }
            for (var k = 1; k < 10; ++k) {
                var c = k * (p10[k] - p10[k - 1]);
                if (c < n) {
                    n -= (int)c;
                } else {
                    return get(p10[k - 1], k, n - 1);
                }
            }
            throw new InvalidOperationException();
        }

        private int get(long z, int k, int n) {
            var x = z + n / k;
            var s = x.ToString();
            return s[n % k] - '0';
        }
    }
}
