using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0188 {
        public int MaxProfit(int k, int[] prices) {
            if (prices.Length == 0 || k == 0) {
                return 0;
            }
            var cash = new int[prices.Length, k];
            var hold = new int[prices.Length, k];
            for (var j = 0; j < k; ++j) {
                hold[0, j] = -prices[0];
            }
            for (var i = 1; i < prices.Length; ++i) {
                hold[i, 0] = Math.Max(hold[i - 1, 0], -prices[i]);
                cash[i, 0] = Math.Max(cash[i - 1, 0], +prices[i] + hold[i, 0]);
                for (var j = 1; j < k; ++j) {
                    hold[i, j] = Math.Max(hold[i - 1, j], -prices[i] + cash[i, j - 1]);
                    cash[i, j] = Math.Max(cash[i - 1, j], +prices[i] + hold[i, j]);
                }
            }
            return cash[prices.Length - 1, k - 1];
        }
    }
}
