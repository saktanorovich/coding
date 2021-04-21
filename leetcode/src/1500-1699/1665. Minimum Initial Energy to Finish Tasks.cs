using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1665 {
        public int MinimumEffort(int[][] tasks) {
            Array.Sort(tasks, (a, b) => {
                return a[0] - b[0];
            });
            var min = 0;
            var has = 0;
            foreach (var task in tasks) {
                if (has < task[1]) {
                    min = task[1] - has + min;
                    has = task[1];
                }
                has -= task[0];
            }
            return min;
        }
    }
}
