using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1620 {
        public int[] BestCoordinate(int[][] towers, int radius) {
            var bestx = 0;
            var besty = 0;
            var bestq = 0;
            for (var x = 0; x <= 50; ++x) {
                for (var y = 0; y <= 50; ++y) {
                    var q = 0;
                    foreach (var t in towers) {
                        var d = (t[0] - x) * (t[0] - x) + (t[1] - y) * (t[1] - y);
                        if (d <= radius * radius) {
                            q += (int)(1.0 * t[2] / (1 + Math.Sqrt(d)));
                        }
                    }
                    if (bestq < q) {
                        bestq = q;
                        bestx = x;
                        besty = y;
                    }
                }
            }
            return new[] { bestx, besty };
        }
    }
}
