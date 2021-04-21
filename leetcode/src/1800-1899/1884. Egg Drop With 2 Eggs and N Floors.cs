using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1884 {
        public int TwoEggDrop(int n) {
            var best = new int[n + 1];
            for (var i = 1; i <= n; ++i) {
                best[i] = i;
                for (var j = i - 1; j > 0; --j) {
                    var drop = 1 + Math.Max(i - j, best[j - 1]);
                    if (best[i] > drop) {
                        best[i] = drop;
                    }
                }
            }
            return best[n];
        }
    }
}