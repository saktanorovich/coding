using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_2187 {
        public long MinimumTime(int[] time, int totalTrips) {
            Array.Sort(time);
            long lo = 0, hi = long.MaxValue / 2;
            while (lo < hi) {
                var spend = (lo + hi) / 2;
                var count = 0L;
                foreach (var bus in time) {
                    count += spend / bus;
                    if (count >= totalTrips) {
                        break;
                    }
                }
                if (count < totalTrips) {
                    lo = spend + 1;
                } else {
                    hi = spend;
                }
            }
            return lo;
        }
    }
}