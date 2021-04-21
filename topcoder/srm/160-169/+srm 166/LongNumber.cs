using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class LongNumber {
        public int findDigit(int k) {
            var d1 = getDigit(k, max1, x => x);
            var d2 = getDigit(k, max2, x => x * x);
            if (overflow(k + 1)) {
                return (d1 + d2 + 1) % 10;
            }
            return (d1 + d2) % 10;
        }

        private static bool overflow(long k) {
            var d1 = getDigit(k, max1, x => x);
            var d2 = getDigit(k, max2, x => x * x);
            if (d1 + d2 < 9) {
                return false;
            }
            if (d1 + d2 > 9) {
                return true;
            }
            return overflow(k + 1);
        }

        private static int getDigit(long k, Func<int, long> max, Func<long, long> ind) {
            var totalLength = 0L;
            for (var len = 1; ; ++len) {
                var count = max(len) - max(len - 1);
                if (totalLength + len * count >= k) {
                    long a = 1, b = count;
                    while (a < b) {
                        var x = (a + b) / 2;
                        if (totalLength + len * x < k)
                            a = x + 1;
                        else
                            b = x;
                    }
                    totalLength += len * (a - 1);
                    var digits = digitize(ind(max(len - 1) + a));
                    for (var i = 1; i <= digits.Length; ++i) {
                        if (totalLength + i == k) {
                            return digits[i - 1];
                        }
                    }
                }
                totalLength += len * count;
            }
        }

        private static long max1(int len) {
            return pow(10, len) - 1;
        }

        private static long max2(int len) {
            long a = 1, b = 2 * (long)Math.Sqrt(max1(len));
            while (a < b) {
                var y = (a + b) / 2;
                if (y * y > max1(len))
                    b = y;
                else
                    a = y + 1;
            }
            return a - 1;
        }

        private static int[] digitize(long x) {
            var result = new List<int>();
            for (; x > 0; x /= 10) {
                result.Add((int)(x % 10));
            }
            return Enumerable.Reverse(result).ToArray();
        }

        private static long pow(long a, int k) {
            if (k > 0) {
                if (k % 2 == 0) {
                    return pow(a * a, k / 2);
                }
                return a * pow(a, k - 1);
            }
            return 1;
        }
    }
}