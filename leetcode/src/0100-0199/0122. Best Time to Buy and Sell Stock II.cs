using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0122 {
        public int MaxProfit(int[] prices) {
            var ans = 0;
            for (var i = 1; i < prices.Length;) {
                while (i < prices.Length && prices[i] <= prices[i - 1]) {
                    i = i + 1;
                }
                ans -= prices[i - 1];
                while (i < prices.Length && prices[i] >= prices[i - 1]) {
                    i = i + 1;
                }
                ans += prices[i - 1];
            }
            return ans;
            // var cash = new int[prices.Length];
            // var hold = new int[prices.Length];
            // hold[0] = -prices[0];
            // for (var i = 1; i < prices.Length; ++i) {
            //     hold[i] = Math.Max(hold[i - 1], -prices[i] + cash[i - 1]);
            //     cash[i] = Math.Max(cash[i - 1], +prices[i] + hold[i]);
            // }
            // return cash[prices.Length - 1];
        }
    }
}
