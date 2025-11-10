using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1723 {
        public int MinimumTimeRequired(int[] jobs, int k) {
            var best = new int[1 << jobs.Length, k + 1];
            var time = new int[1 << jobs.Length];
            for (var i = 0; i < jobs.Length; ++i) {
                time[1 << i] = jobs[i];
            }
            for (var set = 1; set < 1 << jobs.Length; ++set) {
                time[set] = time[set & (set - 1)] + time[set & -set];
            }
            for (var set = 1; set < 1 << jobs.Length; ++set) {
                best[set, 1] = time[set];
                for (var t = 2; t <= k; ++t) {
                    best[set, t] = int.MaxValue;
                    for (var subset = set; subset > 0; subset = (subset - 1) & set) {
                        best[set, t] = Math.Min(best[set, t], Math.Max(time[subset], best[set ^ subset, t - 1]));
                    }
                }
            }
            return best[(1 << jobs.Length) - 1, k];
        }
    }
}
