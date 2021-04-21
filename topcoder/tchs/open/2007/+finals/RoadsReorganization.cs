using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class RoadsReorganization {
        public int minDaysCount(string[] kingdom) {
            memo = new int[kingdom.Length, 2];
            for (var i = 0; i < kingdom.Length; ++i) {
                memo[i, 0] = -1;
                memo[i, 1] = -1;
            }
            // if we remove k roads then the total number of roads to rebuild is 2*k + 1
            // which is equivalent to have (k+1) disjoint paths
            return 2 * dfs(kingdom, 0, -1, 0) - 1;
        }

        // returns a minimum number of disjoint paths
        private int dfs(string[] g, int s, int p, int on) {
            Func<int, bool> hasEdge = x => {
                return g[s][x] == '1' && x != p;
            };
            if (memo[s, on] == -1) {
                var disjoint = 1 - on;
                for (var n = 0; n < g.Length; ++n) {
                    if (hasEdge(n)) {
                        disjoint += dfs(g, n, s, 0);
                    }
                }
                memo[s, on] = disjoint;
                for (var n = 0; n < g.Length; ++n) {
                    if (hasEdge(n)) {
                        var value = disjoint;
                        value -= dfs(g, n, s, 0);
                        value += dfs(g, n, s, 1);
                        if (memo[s, on] > value) {
                            memo[s, on] = value;
                        }
                    }
                }
                if (on == 0) {
                    for (var u = 0; u < g.Length; ++u) {
                        if (hasEdge(u)) {
                            for (var v = u + 1; v < g.Length; ++v) {
                                if (hasEdge(v)) {
                                    var value = disjoint;
                                    value -= dfs(g, u, s, 0);
                                    value -= dfs(g, v, s, 0);
                                    value += dfs(g, u, s, 1);
                                    value += dfs(g, v, s, 1);
                                    if (memo[s, on] > value) {
                                        memo[s, on] = value;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return memo[s, on];
        }

        private int[,] memo;
    }
}
