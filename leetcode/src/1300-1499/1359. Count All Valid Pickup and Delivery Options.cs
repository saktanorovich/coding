using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1359 {
        public int CountOrders(int n) {
            var f = new long[n + 1];
            f[1] = 1;
            for (var k = 2; k <= n; ++k) {
                for (var i = 1; i < 2 * k; ++i) {
                    f[k] += i;
                    f[k] %= mod;
                }
                f[k] *= f[k - 1];
                f[k] %= mod;
            }
            return (int)f[n];
        }

        private const long mod = (long)1e9 + 7;
    }
}
