using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1819 {
        public int CountDifferentSubsequenceGCDs(int[] nums) {
            return CountDifferentSubsequenceGCDs(nums, nums.Max());
        }

        private int CountDifferentSubsequenceGCDs(int[] nums, int M) {
            var has = new bool[M + 1];
            foreach (var x in nums) {
                has[x] = true;
            }
            var res = 0;
            for (var gcd = 1; gcd <= M; ++gcd) {
                // check if we can build a subsequence whose GCD is gcd
                var div = 0;
                for (var x = gcd; x <= M; x += gcd) {
                    if (has[x]) {
                        div = GCD(div, x);
                    }
                }
                if (gcd == div) res++;
            }
            return res;
        }

        private static int GCD(int a, int b) {
            while (a != 0 && b != 0) {
                if (a > b) {
                    a %= b;
                } else {
                    b %= a;
                }
            }
            return a + b;
        }
    }
}
