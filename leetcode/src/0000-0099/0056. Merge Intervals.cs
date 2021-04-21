using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0056 {
        public int[][] Merge(int[][] intervals) {
            if (intervals == null || intervals.Length == 0) {
                return new int[0][];
            }
            Array.Sort(intervals, (a, b) => {
                if (a[0] != b[0]) {
                    return a[0] - b[0];
                } else {
                    return a[1] - b[1];
                }
            });
            var res = new List<int[]>();
            var min = intervals[0][0];
            var max = intervals[0][1];
            for (var i = 1; i < intervals.Length; ++i) {
                if (max < intervals[i][0]) {
                    res.Add(new[] { min, max });
                    min = intervals[i][0];
                    max = intervals[i][1];
                } else {
                    max = Math.Max(max, intervals[i][1]);
                }
            }
            res.Add(new[] { min, max });
            return res.ToArray();
        }
    }
}
