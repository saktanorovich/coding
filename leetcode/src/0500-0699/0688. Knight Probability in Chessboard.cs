using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0688 {
        public double KnightProbability(int N, int K, int r, int c) {
            var prob = new double[N, N];
            prob[r, c] = 1;
            while (K-- > 0) {
                var help = new double[N, N];
                for (var x = 0; x < N; ++x) {
                    for (var y = 0; y < N; ++y) {
                        for (var t = 0; t < 8; ++t) {
                            var nx = x + dx[t];
                            var ny = y + dy[t];
                            if (0 <= nx && nx < N && 0 <= ny && ny < N) {
                                help[nx, ny] += prob[x, y] / 8;
                            }
                        }
                    }
                }
                prob = help;
            }
            var res = 0.0;
            for (var x = 0; x < N; ++x) {
                for (var y = 0; y < N; ++y) {
                    res += prob[x, y];
                }
            }
            return res;
        }

        private static readonly int[] dx = { -1, -2, -2, -1, +1, +2, +2, +1 };
        private static readonly int[] dy = { -2, -1, +1, +2, +2, +1, -1, -2 };
    }
}
