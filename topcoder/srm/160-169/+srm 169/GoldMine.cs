using System;
using System.Linq;

namespace TopCoder.Algorithm {
    public class GoldMine {
        public int[] getAllocation(string[] mines, int miners) {
            return getAllocation(Array.ConvertAll(mines, mine => {
                var prob = mine.Split(new[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries);
                return prob.Select(int.Parse).ToArray();
            }), miners);
        }

        private static int[] getAllocation(int[][] mines, int numOfMiners) {
            /* The problem can be reformulated as problem with n functions Fi where goal
             * is to find decomposition of miners such that Fi(mi) -> max. Because original
             * problem statement requires to place maximum number of miners as earlier as possible
             * we reverse mines without affecting the best expected profit.. */
            var functions = Array.ConvertAll(mines, profit);
            functions = functions.Reverse().ToArray();
            var result = getAllocation(functions, numOfMiners);
            return result.Reverse().ToArray();
        }

        private static int[] getAllocation(Func<int, int>[] mines, int numOfMiners) {
            var best = new int[mines.Length + 1, numOfMiners + 1];
            var prev = new int[mines.Length + 1, numOfMiners + 1];
            for (var mine = 0; mine <= mines.Length; ++mine) {
                for (var miners = 0; miners <= numOfMiners; ++miners) {
                    best[mine, miners] = int.MinValue / 2;
                }
            }
            best[0, 0] = 0;
            for (var mine = 1; mine <= mines.Length; ++mine) {
                for (var miners = 0; miners <= numOfMiners; ++miners) {
                    for (var place = 6; place >= 0; --place) {
                        if (miners >= place) {
                            var profit = mines[mine - 1](place);
                            if (best[mine, miners] < best[mine - 1, miners - place] + profit) {
                                best[mine, miners] = best[mine - 1, miners - place] + profit;
                                prev[mine, miners] = place;
                            }
                        }
                    }
                }
            }
            var result = new int[mines.Length];
            for (var mine = mines.Length - 1; mine >= 0; --mine) {
                result[mine] = prev[mine + 1, numOfMiners];
                numOfMiners -= prev[mine + 1, numOfMiners];
            }
            return result;
        }

        private static Func<int, int> profit(int[] mine) {
            return miners => {
                var result = 0;
                for (var i = 0; i < mine.Length; ++i) {
                    result += earn(miners, i) * mine[i];
                }
                return result;
            };
        }

        private static int earn(int miners, int deposits) {
            if (miners >= deposits) {
                return 50 * deposits - 20 * (miners - deposits);
            }
            return 60 * miners;
        }
    }
}