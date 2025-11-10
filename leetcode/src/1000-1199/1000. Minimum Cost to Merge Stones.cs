using System;
using System.Collections.Generic;
using System.Text;

namespace coding.leetcode {
    public class Solution_1000 {
        public int MergeStones(int[] stones, int K) {
            return MergeStones(stones, stones.Length, K);
        }

        private int MergeStones(int[] stones, int N, int K) {
            if (okay(1, N, K) == false) {
                return -1;
            }
            for (var i = 1; i < N; ++i) {
                stones[i] += stones[i - 1];
            }
            var best = new int[N, N];
            for (var L = K; L <= N; ++L) {
                for (var i = 0; i + L <= N; ++i) {
                    var j = i + L - 1;
                    best[i, j] = int.MaxValue;
                    // let's iterate over all okay prefixes like [i, i + t(K-1)]
                    for (var x = i; x < j; x += K - 1) {
                        best[i, j] = Math.Min(best[i, j], best[i, x] + best[x + 1, j]);
                    }
                    if (okay(i, j, K)) {
                        best[i, j] += suma(stones, i, j);
                    }
                }
            }
            return best[0, N - 1];
        }

        private int suma(int[] stones, int i, int j) {
            if (i > 0) {
                return stones[j] - stones[i - 1];
            } else {
                return stones[j];
            }
        }

        private bool okay(int i, int j, int K) {
            return (j - i) % (K - 1) == 0;
        }
    }
}
