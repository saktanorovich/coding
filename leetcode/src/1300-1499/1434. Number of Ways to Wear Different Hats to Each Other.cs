using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1434 {
        public int NumberWays(IList<IList<int>> hats) {
            var sets = new List<int>[41];
            for (var h = 1; h <= 40; ++h) {
                sets[h] = new List<int>();
            }
            for (var i = 0; i < hats.Count; ++i) {
                foreach (var h in hats[i]) {
                    sets[h].Add(i);
                }
            }
            return NumberWays(sets, hats.Count);
        }

        public int NumberWays(IList<IList<int>> sets, int n) {
            var dp = new int[1 << n];
            dp[0] = 1;
            for (var h = 1; h <= 40; ++h) {
                for (var set = (1 << n) - 1; set >=0; --set) {
                    foreach (var i in sets[h]) {
                        if ((set & (1 << i)) == 0) {
                            dp[set | (1 << i)] += dp[set];
                            if (dp[set | (1 << i)] >= mod) {
                                dp[set | (1 << i)] -= mod;
                            }
                        }
                    }
                }
            }
            return dp[(1 << n) - 1];
        }

        private const int mod = (int)1e9 + 7;
    }
}