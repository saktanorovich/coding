using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0096 {
        public int NumTrees(int n) {
            var memo = new int[n + 1];
            memo[0] = 1;
            for (var k = 1; k <= n; ++k) {
                for (var i = 1; i <= k; ++i) {
                    memo[k] += memo[i - 1] * memo[k - i];
                }
            }
            return memo[n];
        }
    }
}
