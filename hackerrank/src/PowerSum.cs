/**
Find the number of integers from l to r which can be expressed
as a^x + b^y where a, b, x, y >= 0.
*/
using System;

namespace interview.hackerrank {
    public class PowerSum {
        private readonly bool[] isPowerNumber = new bool[(int)5e6 + 1];

        public int count(int l, int r) {
            isPowerNumber[0] = true;
            isPowerNumber[1] = true;
            isPowerNumber[2] = true;
            // x^k + {0|1} = n
            for (var x = 2; x * x <= r; ++x) {
                for (long p = x * x; p <= r; p *= x) {
                    for (var b = 0; b <= 1; ++b) {
                        if (p + b <= r) {
                            isPowerNumber[p + b] = true;
                        }
                    }
                }
            }
            // a^x + b^y = n
            for (var a = 2; a * a <= r; ++a) {
                for (long p = a * a; p <= r; p *= a) {
                    for (var b = a; b * b <= r; ++b) {
                        for (long q = b * b; q <= r; q *= b) {
                            if (p + q <= r) {
                                isPowerNumber[p + q] = true;
                            }
                        }
                    }
                }
            }
            var result = 0;
            for (var x = l; x <= r; ++x) {
                if (isPowerNumber[x]) {
                    ++result;
                }
            }
            return result;
        }
    }
}
