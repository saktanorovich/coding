using System;
using System.Collections.Generic;

namespace coding.interview {
    /**
     * A palindromic number reads the same both ways. The largest palindrome
     * made from the product of two 2-digit numbers is 9009 = 91*99. Find
     * the largest palindrome made from the product of two 3-digit numbers.
     */
    public class LargestPalindrome {
        public int GetLargestPalindrome(int digits) {
            if (0 < digits && digits < 5) {
                return GetLargestPalindromeImpl(digits);
            }
            throw new ArgumentOutOfRangeException("digits");
        }

        private int GetLargestPalindromeImpl(int digits) {
            var result = 0;
            var min = Pow10[digits - 1];
            var max = Pow10[digits] - 1;
            for (var a = max; a >= min; --a) {
                if (a * max < result) {
                    break;
                }
                for (var b = max; b >= a; --b) {
                    var x = a * b;
                    if (IsPalindrome(x)) {
                        result = Math.Max(result, x);
                    }
                }
            }
            return result;
        }

        private static bool IsPalindrome(int x) {
            var digits = Digitize(x);
            for (int i = 0, j = digits.Count - 1; i < j; ++i, --j) {
                if (digits[i] != digits[j])
                    return false;
            }
            return true;
        }

        private static IList<int> Digitize(int x) {
            var result = new List<int>();
            for (; x > 0; x /= 10) {
                result.Add(x % 10);
            }
            return result;
        }

        private static readonly int[] Pow10 = {
            1, 10, 100, 1000, 10000, 100000,
        };
    }
}