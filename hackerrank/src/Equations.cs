using System;

namespace interview.hackerrank {
    public class Equations {
        private readonly long modulo = 1000007;

        /* The equation 1/x + 1/y = 1/n! is equivalent to (n!-x) * (n!-y) = n!^2. */
        public int count(int n) {
            var factors = new long[n + 1];
            for (var k = 2; k <= n; ++k) {
                var x = k;
                for (var p = 2; p * p <= x; ++p) {
                    while (x % p == 0) {
                        ++factors[p];
                        x = x / p;
                    }
                }
                if (x > 1) {
                    ++factors[x];
                }
            }
            var numOfFactors = 1L;
            for (var k = 2; k <= n; ++k) {
                numOfFactors *= (2 * factors[k] + 1);
                numOfFactors %= modulo;
            }
            return (int)numOfFactors;
        }
    }
}
