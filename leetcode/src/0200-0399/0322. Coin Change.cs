using System;
using System.Collections.Generic;
using System.Text;

namespace coding.leetcode {
    public class Solution_0322 {
        public int CoinChange(int[] coins, int amount) {
            if (coins == null || coins.Length == 0) {
                return -1;
            }
            var best = new int[amount + 1];
            for (var s = 1; s <= amount; ++s) {
                best[s] = int.MaxValue;
            }
            for (var s = 1; s <= amount; ++s) {
                foreach (var c in coins) {
                    if (s - c >= 0) {
                        if (best[s - c] < int.MaxValue) {
                            if (best[s] > best[s - c] + 1) {
                                best[s] = best[s - c] + 1;
                            }
                        }
                    }
                }
            }
            return best[amount] < int.MaxValue ? best[amount] : -1;
        }
    }
}
