using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class NCMultiplication {
        public long findFactors(int[] digits) {
            return findFactors(digits, fromDigits(digits));
        }

        private static long findFactors(int[] digits, long number) {
            for (var b = (long)Math.Sqrt(number) + 1; b > 0; --b) {
                if (b * b <= number && number % b == 0) {
                    var a = number / b;
                    if (valid(digits, a, b)) {
                        return a;
                    }
                }
            }
            return -1;
        }

        private static bool valid(int[] digits, long a, long b) {
            var x = digitize(a);
            var y = digitize(b);
            var result = new int[x.Length + y.Length - 1];
            if (result.Length == digits.Length) {
                for (var i = 0; i < x.Length; ++i) {
                    for (var j = 0; j < y.Length; ++j) {
                        result[i + j] += x[i] * y[j];
                    }
                }
                for (var i = 0; i < result.Length; ++i) {
                    if (result[i] != digits[digits.Length - 1 - i]) {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        private static int[] digitize(long x) {
            var result = new List<int>();
            for (; x > 0; x /= 10) {
                result.Add((int)(x % 10));
            }
            return result.ToArray();
        }

        private static long fromDigits(int[] digits) {
            var result = 0L;
            foreach (var d in digits) {
                result = result * 10 + d;
            }
            return result;
        }
    }
}