using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class ComplexIntegers {
        public string[] classify(int[] realPart, int[] imaginaryPart) {
            var result = new string[realPart.Length];
            for (var i = 0; i < realPart.Length; ++i) {
                result[i] = classify(realPart[i], imaginaryPart[i]);
            }
            return result;
        }

        private static string classify(int re, int im) {
            if (norm(re, im) == 0) return "zero";
            if (norm(re, im) == 1) return "unit";
            if (re != 0 && im != 0) {
                if (isPrime(norm(re, im))) {
                    return "prime";
                }
            }
            else {
                if (isPrime(re) && Math.Abs(re) % 4 == 3) return "prime";
                if (isPrime(im) && Math.Abs(im) % 4 == 3) return "prime";
            }
            return "composite";
        }

        private static bool isPrime(int p) {
            p = Math.Abs(p);
            for (var x = 2; x * x <= p; ++x) {
                if (p % x == 0) {
                    return false;
                }
            }
            return true;
        }

        private static int norm(int re, int im) {
            return re * re + im * im;
        }
    }
}