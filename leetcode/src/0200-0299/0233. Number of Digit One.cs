using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0233 {
        public int CountDigitOne(int n) {
            this.digits = digitize(n);
            this.memo = new long[digits.Count, 2, 11];
            for (var pos = 0; pos < digits.Count; ++pos) {
                for (var carry = 0; carry < 2; ++carry) {
                    for (var ones = 0; ones <= 10; ++ones) {
                        memo[pos, carry, ones] = -1;
                    }
                }
            }
            return (int)doit(0, 1, 0);
        }

        private long doit(int pos, int carry, int ones) {
            if (pos == digits.Count) {
                return ones;
            }
            if (memo[pos, carry, ones] != -1) {
                return memo[pos, carry, ones];
            }
            var count = 0L;
            if (carry == 0) {
                for (var d = 0; d <= 9; ++d) {
                    count += doit(pos + 1, 0, ones + (d == 1 ? 1 : 0));
                }
            } else {
                for (var d = 0; d <= digits[pos]; ++d) {
                    if (d < digits[pos]) {
                        count += doit(pos + 1, 0, ones + (d == 1 ? 1 : 0));
                    } else {
                        count += doit(pos + 1, 1, ones + (d == 1 ? 1 : 0));
                    }
                }
            }
            return memo[pos, carry, ones] = count;
        }

        private List<int> digits;
        private long[,,] memo;

        private static List<int> digitize(int n) {
            var digits = new List<int>();
            while (n > 0) {
                digits.Add(n % 10);
                n /= 10;
            }
            digits.Reverse();
            return digits.ToList();
        }
    }
}
