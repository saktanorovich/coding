using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0518 {
            public int Change(int amount, int[] coins) {
                var comb = new int[amount + 1];
                comb[0] = 1;
                foreach (var c in coins) {
                    for (var s = 0; s + c <= amount; ++s) {
                        comb[s + c] += comb[s];
                    }
                }
                return comb[amount];
            }
    }
}