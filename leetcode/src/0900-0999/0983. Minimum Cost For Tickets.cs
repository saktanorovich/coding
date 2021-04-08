using System;
using System.Collections.Generic;
using System.Text;

namespace coding.leetcode {
    public class Solution_0983 {
        public int MincostTickets(int[] days, int[] costs) {
            return MincostTickets(days, costs, new[] { 1, 7, 30 });
        }

        private int MincostTickets(int[] days, int[] costs, int[] offers) {
            var best = new int[days.Length + 1];
            for (var i = 1; i <= days.Length; ++i) {
                best[i] = int.MaxValue;
                for (var j = 1; j <= i; ++j) {
                    for (var k = 0; k < offers.Length; ++k) {
                        var period = days[i - 1] - days[j - 1] + 1;
                        if (period <= offers[k]) {
                            best[i] = Math.Min(best[i], best[j - 1] + costs[k]);
                        }
                    }
                }
            }
            return best[days.Length];
        }
    }
}
