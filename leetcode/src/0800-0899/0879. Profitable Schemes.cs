using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0879 {
        public int ProfitableSchemes(int G, int P, int[] group, int[] profit) {
            var f = new int[P + 1, G + 1];
            f[0, 0] = 1;
            for (var i = 0; i < group.Length; ++i) {
                for (var p = P; p >= 0; --p) {
                    for (var g = G; g >= 0; --g) {
                        if (f[p, g] > 0) {
                            if (g + group[i] <= G) {
                                var q = Math.Min(P, p + profit[i]);
                                f[q, g + group[i]] += f[p, g];
                                f[q, g + group[i]] %= mod;
                            }
                        }
                    }
                }
            }
            var res = 0;
            for (var g = 0; g <= G; ++g) {
                res += f[P, g];
                res %= mod;
            }
            return res;
        }

        private static readonly int mod = (int)1e9 + 7;
    }
}
