using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0279 {
        public int NumSquares(int n) {
            var memo = new int[n + 1];
            for (var x = 1; x * x <= n; ++x) {
                memo[x * x] = 1;
            }
            return NumSquares(memo, n);
        }

        private int NumSquares(int[] memo, int n) {
            if (memo[n] == 0) {
                memo[n] = n;
                for (var x = 1; x * x < n; ++x) {
                    memo[n] = Math.Min(memo[n], NumSquares(memo, n - x * x) + 1);
                }
            }
            return memo[n];
        }
    }
}
