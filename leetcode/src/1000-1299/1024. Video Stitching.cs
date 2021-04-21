using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1024 {
        public int VideoStitching(int[][] clips, int T) {
            Array.Sort(clips, (a, b) => {
                return a[1] - b[1];
            });
            var best = new int[T + 1];
            for (var t = 0; t <= T; ++t) {
                best[t] = int.MaxValue / 2;
            }
            best[0] = 0;
            foreach (var clip in clips) {
                var l = clip[0];
                var r = clip[1];
                if (r > T) {
                    r = T;
                }
                for (var t = l; t <= r; ++t) {
                    if (best[r] > best[t] + 1) {
                        best[r] = best[t] + 1;
                    }
                }
            }
            return best[T] < int.MaxValue / 2 ? best[T] : -1;
        }
    }
}
