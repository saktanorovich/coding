using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class Divisibility {
        public int numDivisible(int L, int R, int[] a) {
            var result = 0L;
            for (var set = 1; set < 1 << a.Length; ++set) {
                var mul = 1L;
                var sgn = -1;
                for (var i = 0; i < a.Length; ++i) {
                    if ((set & (1 << i)) != 0) {
                        mul *= a[i] / gcd(mul, a[i]);
                        if (mul > R) {
                            goto next;
                        }
                        sgn = -sgn;
                    }
                }
                result += sgn * (R / mul - (L - 1) / mul);
                next:;
            }
            return (int)result;
        }

        private static long gcd(long a, long b) {
            while (a != 0 && b != 0) {
                if (a > b) {
                    a %= b;
                }
                else {
                    b %= a;
                }
            }
            return a + b;
        }
    }
}
