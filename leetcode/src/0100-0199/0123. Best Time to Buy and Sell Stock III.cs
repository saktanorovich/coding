using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0123 {
        public int MaxProfit(int[] prices) {
            var cash = new int[prices.Length, 2];
            var hold = new int[prices.Length, 2];
            hold[0, 0] = -prices[0];
            hold[0, 1] = -prices[0];
            for (var i = 1; i < prices.Length; ++i) {
                hold[i, 0] = Math.Max(hold[i - 1, 0], -prices[i]);
                cash[i, 0] = Math.Max(cash[i - 1, 0], +prices[i] + hold[i, 0]);
                hold[i, 1] = Math.Max(hold[i - 1, 1], -prices[i] + cash[i, 0]);
                cash[i, 1] = Math.Max(cash[i - 1, 1], +prices[i] + hold[i, 1]);
            }
            return cash[prices.Length - 1, 1];
        }
    }
}
