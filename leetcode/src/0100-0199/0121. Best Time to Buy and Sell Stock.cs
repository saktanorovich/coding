using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0121 {
        public int MaxProfit(int[] prices) {
            var min = int.MaxValue;
            var ans = 0;
            foreach (var price in prices) {
                if (price > min) {
                    ans = Math.Max(ans, price - min);
                }
                min = Math.Min(min, price);
            }
            return ans;
            // var cash = new int[prices.Length];
            // var hold = new int[prices.Length];
            // hold[0] = -prices[0];
            // for (var i = 1; i < prices.Length; ++i) {
            //     hold[i] = Math.Max(hold[i - 1], -prices[i]);
            //     cash[i] = Math.Max(cash[i - 1], +prices[i] + hold[i]);
            // }
            // return cash[prices.Length - 1];
        }
    }
}
