using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_2572 {
        public int SquareFreeSubsets(int[] nums) {
            var freq = new int[max + 1];
            var mask = new int[max + 1];
            foreach (var num in nums) {
                if (good(num)) {
                    freq[num] ++;
                    mask[num] = make(num);
                }
            }
            var summ = new long[1 << primes.Length];
            summ[0] = 1;
            for (var num = 2; num <= max; ++num) {
                for (var set = 0; set < 1 << primes.Length; ++set) {
                    if ((set & mask[num]) == 0) {
                        summ[set | mask[num]] += summ[set] * freq[num];
                        summ[set | mask[num]] %= mod;
                    }
                }
            }
            var ones = pow(2, freq[1]);
            var answ = 0L;
            answ += summ.Sum() - 1;
            answ += mod;
            answ %= mod;
            if (ones > 1) {
                answ *= ones;
                answ %= mod;
                answ += ones - 1;
                answ += mod;
                answ %= mod;
            }
            return (int)answ;
        }

        private static bool good(int num) {
            foreach (var sqr in powers) {
                if (num % sqr == 0) {
                    return false;
                }
            }
            return true;
        }

        private static long pow(long x, int k) {
            if (k == 0) {
                return 1;
            } else if (k % 2 == 0) {
                return pow(x * x % mod, k / 2);
            } else {
                return x * pow(x, k - 1) % mod;
            }
        }

        private static int make(int num) {
            var mask = 0;
            for (var i = 0; i < primes.Length; ++i) {
                if (num % primes[i] == 0) {
                    mask |= 1 << i;
                }
            }
            return mask;
        }

        private static readonly long mod = (long)1e9 + 7;
        private static readonly int max = 30;
        private static readonly int[] powers = { 4, 9, 25 };
        private static readonly int[] primes = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29 };
    }
}