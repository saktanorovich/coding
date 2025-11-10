using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0465 {
        public int MinTransfers(IList<int[]> transactions) {
            // if (transactions.Count < 1) {
            //     return new Small(transactions).solve();
            // } else {
            //     return new Large(transactions).solve();
            // }
            var small = new Small(transactions).solve();
            var large = new Large(transactions).solve();
            if (large != small) {
                Console.WriteLine($"small = {small}, large = {large}");
                return -1;
            } else {
                return large;
            }
        }

        private class Small {
            private readonly int N = 12;
            private readonly IList<int[]> trans;

            public Small(IList<int[]> trans) {
                this.trans = trans;
            }

            public int solve() {
                var debt = new int[N];
                foreach (var t in trans) {
                    debt[t[0]] += t[2];
                    debt[t[1]] -= t[2];
                }
                return solve(debt.Where(d => d != 0).ToArray());
            }

            private int solve(int[] debt) {
                var best = new int[1 << debt.Length];
                for (var set = 1; set < 1 << debt.Length; ++set) {
                    best[set] = int.MaxValue / 2;
                }
                for (var set = 1; set < 1 << debt.Length; ++set) {
                    var sum = 0;
                    for (var i = 0; i < debt.Length; ++i) {
                        if (MathUtils.Contains(set, i)) {
                            sum += debt[i];
                        }
                    }
                    if (sum == 0) {
                        best[set] = MathUtils.Bits(set) - 1;
                        foreach (var subset in MathUtils.Subsets(set)) {
                            var eval = best[subset] + best[set ^ subset];
                            if (best[set] > eval) {
                                best[set] = eval;
                            }
                        }
                    }
                }
                return best[(1 << debt.Length) - 1];
            }

            private static class MathUtils {
                public static bool Contains(int set, int x) {
                    return (set & (1 << x)) != 0;
                }

                public static int Bits(int set) {
                    if (set > 0) {
                        return 1 + Bits(set & (set - 1));
                    }
                    return 0;
                }

                public static IEnumerable<int> Subsets(int set) {
                    for (var subset = set; subset > 0; subset = set & (subset - 1)) {
                        yield return subset;
                    }
                }
            }
        }

        private class Large {
            private readonly int N = 12;
            private readonly IList<int[]> trans;

            public Large(IList<int[]> trans) {
                this.trans = trans;
            }

            public int solve() {
                var balance = new int[N];
                foreach (var t in trans) {
                    balance[t[0]] += t[2];
                    balance[t[1]] -= t[2];
                }
                var src = N;
                var dst = N + 1;
                var flow = new int[N + 2, N + 2];
                var capa = new int[N + 2, N + 2];
                for (var i = 0; i < N; ++i) {
                    if (balance[i] > 0) {
                        capa[i, dst] = +balance[i];
                    } else {
                        capa[src, i] = -balance[i];
                    }
                }
                for (var i = 0; i < N; ++i) {
                    if (balance[i] < 0) {
                        for (var j = 0; j < N; ++j) {
                            if (balance[j] > 0) {
                                capa[i, j] = int.MaxValue / 2;
                            }
                        }
                    }
                }
                var prev = new int[N + 2];
                while (augment(flow, capa, src, dst, prev)) {
                    var push = int.MaxValue / 2;
                    for (var i = dst; i != src;) {
                        var have = capa[prev[i], i] - flow[prev[i], i];
                        if (push > have) {
                            push = have;
                        }
                        i = prev[i];
                    }
                    for (var i = dst; i != src;) {
                        flow[prev[i], i] += push;
                        flow[i, prev[i]] -= push;
                        i = prev[i];
                    }
                }
                var res = 0;
                for (var i = 0; i < N + 2; ++i) {
                    for (var j = 0; j < N + 2; ++j) {
                        //if (flow[man1, man2] > 0) {
                        if (capa[i, j] > 0) {
                            //Console.WriteLine($"{i} -> {j} : c = {capa[i, j]}, f = {flow[i, j]}");
                            //res = res + 1;
                        }
                    }
                }
                var done = new bool[N + 2];
                for (var i = 0; i < N; ++i) {
                    if (capa[src, i] > 0) {
                        dfs(flow, capa, i, done);
                    }
                }
                //dfs(flow, capa, src, done);
                for (var i = 0; i < N + 2; ++i) {
                    for (var j = 0; j < N + 2; ++j) {
                        // we have an edge from S to T set
                        if (done[i] && !done[j]) {
                            // every edge in the (s, T) cut should have non-0 capacity
                            if (capa[i, j] > 0) {
                                res = res + 1;
                            }
                        }
                        // if (done[i] && !done[j] && capa[i, j] > 0) {
                        //     res  = res + 1;
                        // }
                        // if (done[i] && !done[j] && capa[i, j] > 0) {
                        //     res = res + 1;
                        // }
                    }
                }
                return res;
            }

            private bool augment(int[,] flow, int[,] capa, int src, int dst, int[] prev) {
                for (var i = 0; i < N + 2; ++i) {
                    prev[i] = -1;
                }
                return dfs(flow, capa, src, dst, prev);
            }

            private bool dfs(int[,] flow, int[,] capa, int src, int dst, int[] prev) {
                if (src == dst) {
                    return true;
                }
                for (var nxt = 0; nxt < N + 2; ++nxt) {
                    if (prev[nxt] == -1) {
                        var by = capa[src, nxt] - flow[src, nxt];
                        if (by > 0) {
                            prev[nxt] = src;
                            if (dfs(flow, capa, nxt, dst, prev)) {
                                return true;
                            }
                        }
                    }
                }
                return false;
            }

            private void dfs(int[,] flow, int[,] capa, int src, bool[] done) {
                done[src] = true;
                for (var nxt = 0; nxt < N + 2; ++nxt) {
                    if (done[nxt] == false) {
                        var rsd = capa[src, nxt] - flow[src, nxt];
                        if (rsd > 0) {
                            dfs(flow, capa, nxt, done);
                        }
                    }
                }
            }
        }
    }
}
