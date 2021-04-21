using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0062 {
        public int UniquePaths(int m, int n) {
            var count = new int[m, n];
            count[0, 0] = 1;
            for (var i = 0; i < m; ++i) {
                for (var j = 0; j < n; ++j) {
                    if (i > 0) count[i, j] += count[i - 1, j];
                    if (j > 0) count[i, j] += count[i, j - 1];
                }
            }
            return count[m - 1, n - 1];
        }
    }
}
