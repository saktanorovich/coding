using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0010 {
        public bool IsMatch(string s, string p) {
            var dp = new bool[s.Length + 1, p.Length + 1];
            dp[0, 0] = true;
            for (var j = 1; j <= p.Length; ++j) {
                if (p[j - 1] == '*') {
                    dp[0, j] = dp[0, j - 2];
                }
            }
            for (var i = 1; i <= s.Length; ++i) {
                for (var j = 1; j <= p.Length; ++j) {
                    var x = s[i - 1];
                    var y = p[j - 1];
                    if (y == '*') {
                        var c = p[j - 2];
                        dp[i, j] |= dp[i, j - 2];
                        dp[i, j] |= dp[i - 1, j] && (c == x || c == '.');
                        continue;
                    }
                    if (y == '.') {
                        dp[i, j] |= dp[i - 1, j - 1];
                    } else {
                        dp[i, j] |= dp[i - 1, j - 1] && x == y;
                    }
                }
            }
            return dp[s.Length, p.Length];
        }
    }
}
