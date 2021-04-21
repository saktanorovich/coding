using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_2218 {
        public int MaxValueOfCoins(IList<IList<int>> piles, int k) {
            return MaxValueOfCoins(
                piles.ToArray().
                    Select(p => p.ToArray()).ToArray(),
                        piles.Count, k);
        }

        private int MaxValueOfCoins(int[][] piles, int n, int k) {
            for (var indx = 0; indx < n; ++indx) {
                var pile = piles[indx];
                for (var take = 1; take < pile.Length; ++take) {
                    pile[take] += pile[take - 1];
                }
            }
            var best = new int[n, k + 1];
            for (var take = 1; take <= Math.Min(k, piles[0].Length); ++take) {
                best[0, take] = piles[0][take - 1];
            }
            for (var pile = 1; pile < n; ++pile) {
                for (var have = 1; have <= k; ++have) {
                    best[pile, have] = best[pile - 1, have];
                    for (var take = 1; take <= Math.Min(have, piles[pile].Length); ++take) {
                        if (best[pile, have] < best[pile - 1, have - take] + piles[pile][take - 1]) {
                            best[pile, have] = best[pile - 1, have - take] + piles[pile][take - 1];
                        }
                    }
                }
            }
            return best[n - 1, k];
        }
    }
}