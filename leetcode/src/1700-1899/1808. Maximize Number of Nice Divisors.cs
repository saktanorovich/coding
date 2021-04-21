using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1808 {
        public int MaxNiceDivisors(int P) {
            if (P < 5) {
                return P;
            }
            var n3 = 0;
            if (P % 3 == 1) {
                n3 = P / 3 - 1;
            } else {
                n3 = P / 3;
            }
            var n2 = (P - 3 * n3) / 2;

            var R = 1L;
            R *= pow(2, n2);
            R %= mod;
            R *= pow(3, n3);
            R %= mod;
            return (int)R;
        }

        private static long pow(long x, int k) {
            if (k == 0) {
                return 1;
            } else if (k % 2 == 0) {
                return pow(x * x % mod, k / 2);
            } else {
                return x * pow(x, k - 1) % mod;
            }
        }

        private const long mod = (long)1e9 + 7;
    }
}
