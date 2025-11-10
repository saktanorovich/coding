using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0714 {
        public int MaxProfit(int[] prices, int fee) {
            var cash = new int[prices.Length];
            var hold = new int[prices.Length];
            hold[0] = -prices[0];
            for (var i = 1; i < prices.Length; ++i) {
                hold[i] = Math.Max(hold[i - 1], cash[i - 1] - prices[i]);
                cash[i] = cash[i - 1];
                if (cash[i] < hold[i] + prices[i] - fee) {
                    cash[i] = hold[i] + prices[i] - fee;
                }
            }
            return cash[prices.Length - 1];
        }
    }
}
