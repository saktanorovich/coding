using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_2376 {
        public int CountSpecialNumbers(int n) {
            var digits = n.ToString();
            var res = count(digits, 0, 0);
            for (var len = 1; len < digits.Length; ++len) {
                res += 9 * arrange(9, len - 1);
            }
            return res;
        }

        private int count(string digits, int pos, int mask) {
            if (pos == digits.Length) {
                return 1;
            }
            var res = 0;
            for (var d = pos > 0 ? 0 : 1; d <= digits[pos] - '0'; ++d) {
                if ((mask & (1 << d)) == 0) {
                    if (d < digits[pos] - '0') {
                        res += arrange(9 - pos, digits.Length - pos - 1);
                    } else {
                        res += count(digits, pos + 1, mask | (1 << d));
                    }
                }
            }
            return res;
        }

        private int arrange(int n, int k) {
            var res = 1;
            for (var i = n; i > n - k; --i) {
                res *= i;
            }
            return res;
        }
    }
}