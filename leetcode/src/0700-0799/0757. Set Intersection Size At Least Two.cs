using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0757 {
        public int IntersectionSizeTwo(int[][] intervals) {
            Array.Sort(intervals, (a, b) => {
                if (a[1] != b[1]) {
                    return a[1] - b[1];
                } else {
                    return b[0] - a[0];
                }
            });
            var set = new List<int>();
            set.Add(intervals[0][1] - 1);
            set.Add(intervals[0][1]);
            for (var i = 1; i < intervals.Length; ++i) {
                var a = intervals[i][0];
                var b = intervals[i][1];
                var c = set[set.Count - 2];
                var d = set[set.Count - 1];
                if (a <= c) {
                    continue;
                }
                if (a <= d) {
                    set.Add(b);
                } else {
                    set.Add(b - 1);
                    set.Add(b);
                }
            }
            return set.Count;
        }
    }
}
