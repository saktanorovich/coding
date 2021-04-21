using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class SquareFree {
        public int getNumber(int n) {
            for (var x = 2; 1L * x * x <= int.MaxValue; ++x) {
                for (var y = 2; y * y <= x; ++y) {
                    if (x % y == 0) {
                        goto next;
                    }
                }
                primes.Add(x); next:;
            }
            int lo = 1, hi = int.MaxValue;
            while (lo < hi) {
                var x = lo + (hi - lo) / 2;
                if (x - not(x, 1, 1, 0) < n)
                    lo = x + 1;
                else
                    hi = x;
            }
            return lo;
        }

        private long not(long x, long mul, int sgn, int k) {
            var result = 0L;
            for (; k < primes.Count; ++k) {
                var div = mul * primes[k] * primes[k];
                if (div <= x) {
                    result += sgn * (x / div);
                    result += not(x, div, -sgn, k + 1);
                }
                else break;
            }
            return result;
        }

        private readonly List<int> primes = new List<int>();
    }
}