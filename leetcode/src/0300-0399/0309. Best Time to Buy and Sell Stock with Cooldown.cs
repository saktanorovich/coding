using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0309 {
        public int MaxProfit(int[] prices) {
            if (prices.Length == 0) {
                return 0;
            }
            var nothing = new int[prices.Length];
            var haveing = new int[prices.Length];
            var selling = new int[prices.Length];
            haveing[0] = -prices[0];
            for (var i = 1; i < prices.Length; ++i) {
                nothing[i] = Math.Max(nothing[i - 1], selling[i - 1]);
                haveing[i] = Math.Max(haveing[i - 1], nothing[i - 1] - prices[i]);
                selling[i] = haveing[i - 1] + prices[i];
            }
            return Math.Max(nothing[prices.Length - 1], selling[prices.Length - 1]);
        }
    }
}
