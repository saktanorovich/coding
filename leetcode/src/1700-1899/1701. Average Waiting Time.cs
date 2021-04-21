using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1701 {
        public double AverageWaitingTime(int[][] customers) {
            var summa = 0L;
            var timer = 0L;
            foreach (var customer in customers) {
                var arriveAt = customer[0];
                var cookTime = customer[1];
                if (timer < arriveAt) {
                    timer = arriveAt + cookTime;
                } else {
                    timer += cookTime;
                }
                summa += timer - arriveAt;
            }
            return 1.0 * summa / customers.Length;
        }
    }
}
