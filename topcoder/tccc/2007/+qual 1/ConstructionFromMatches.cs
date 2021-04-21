using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class ConstructionFromMatches {
        public int minimumCost(int[] cost, int[] top, int[] bottom) {
            return minimumCost(top, bottom, cost, top.Length, cost.Length);
        }

        private int minimumCost(int[] top, int[] bot, int[] cost, int m, int n) {
            var best = new int[m + 1, n + 1, n + 1];
            for (var t = 1; t <= n; ++t) {
                for (var b = 1; b <= n; ++b) {
                    best[0, t, b] = cost[t - 1] + cost[b - 1];
                }
            }
            for (var ix = 1; ix <= m; ++ix)
                // current state
                for (var tc = 1; tc <= n; ++tc)
                for (var bc = 1; bc <= n; ++bc)
                {
                    best[ix, tc, bc] = int.MaxValue / 2;
                    for (var up = 1; up <= n; ++up)
                    for (var md = 1; md <= n; ++md)
                    for (var dn = 1; dn <= n; ++dn)
                    {
                        // previous state
                        var tp = top[ix - 1] - tc - up - md;
                        var bp = bot[ix - 1] - bc - dn - md;

                        // relax current state from previous state
                        if (0 < tp && tp <= n && 0 < bp && bp <= n)
                        {
                            best[ix, tc, bc] = Math.Min(best[ix, tc, bc],
                                best[ix - 1, tp, bp] +
                                    cost[tc - 1] +
                                    cost[bc - 1] +
                                    cost[up - 1] +
                                    cost[md - 1] +
                                    cost[dn - 1]);
                        }
                    }
                }
            var result = int.MaxValue / 2;
            for (var t = 1; t <= n; ++t) {
                for (var b = 1; b <= n; ++b) {
                    result = Math.Min(result, best[m, t, b]);
                }
            }
            return result < int.MaxValue / 2 ? result : -1;
        }
    }
}
