using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0878 {
        public int NthMagicalNumber(int n, int a, int b) {
            var lcm = a * b / gcd(a, b);
            var lo = 0L;
            var hi = 1L * n * lcm;
            while (lo < hi) {
                var x = (lo + hi) / 2;
                if (count(x, a, b, lcm) < n) {
                    lo = x + 1;
                } else {
                    hi = x;
                }
            }
            return (int)(lo % mod);
        }

        private long count(long x, int a, int b, int lcm) {
            return x / a + x / b - x / lcm;
        }

        private int gcd(int a, int b) {
            while (a != 0 && b != 0) {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }
            return a + b;
        }

        private const int mod = (int)1e9 + 7;
    }
}
