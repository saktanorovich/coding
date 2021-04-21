using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1288 {
        public int RemoveCoveredIntervals(int[][] intervals) {
            Array.Sort(intervals, (a, b) => {
                if (a[0] != b[0]) {
                    return a[0] - b[0];
                } else {
                    return a[1] - b[1];
                }
            });
            var prev = intervals[0];
            var answ = 1;
            for (var i = 1; i < intervals.Length; ++i) {
                var curr = intervals[i];
                if (prev[0] <= curr[0] && curr[1] <= prev[1]) {
                    continue;
                }
                prev = curr;
                answ++;
            }
            return answ;
        }
    }
}
