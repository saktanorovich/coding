using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_2280 {
        public int MinimumLines(int[][] stockPrices) {
            if (stockPrices.Length == 1) { return 0; }
            if (stockPrices.Length == 2) { return 1; }
            Array.Sort(stockPrices, (a, b) => {
                return a[0] - b[0];
            });
            var res = 1;
            for (var i = 1; i < stockPrices.Length - 1; ++i) {
                long x1 = stockPrices[i][0] - stockPrices[i - 1][0];
                long y1 = stockPrices[i][1] - stockPrices[i - 1][1];

                long x2 = stockPrices[i + 1][0] - stockPrices[i][0];
                long y2 = stockPrices[i + 1][1] - stockPrices[i][1];

                if (x1 * y2 != x2 * y1) {
                    res = res + 1;
                }
            }
            return res;
        }
    }
}
