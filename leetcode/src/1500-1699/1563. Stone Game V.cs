using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1563 {
        public int StoneGameV(int[] stoneValue) {
            memo = new int[stoneValue.Length, stoneValue.Length];
            summ = new int[stoneValue.Length, stoneValue.Length];
            for (var i = 0; i < stoneValue.Length; ++i) {
                summ[i, i] = stoneValue[i];
                for (var j = i + 1; j < stoneValue.Length; ++j) {
                    memo[i, j] = -1;
                    summ[i, j] = summ[i, j - 1] + stoneValue[j];
                }
            }
            return play(0, stoneValue.Length - 1);
        }

        private int play(int l, int r) {
            if (l == r || memo[l, r] != -1) {
                return memo[l, r];
            }
            memo[l, r] = 0;
            for (var i = l; i < r; ++i) {
                if (summ[l, i] > summ[i + 1, r]) {
                    memo[l, r] = Math.Max(memo[l, r], play(i + 1, r) + summ[i + 1, r]);
                    continue;
                }
                if (summ[l, i] < summ[i + 1, r]) {
                    memo[l, r] = Math.Max(memo[l, r], play(l, i) + summ[l, i]);
                    continue;
                }
                var best = Math.Max(play(l, i), play(i + 1, r));
                if (memo[l, r] < best + summ[l, i]) {
                    memo[l, r] = best + summ[l, i];
                }
            }
            return memo[l, r];
        }

        private int[,] memo;
        private int[,] summ;
    }
}
