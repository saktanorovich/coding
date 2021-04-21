using System;
using System.Collections.Generic;
using System.Text;

namespace coding.leetcode {
    public class Solution_0743 {
        public int NetworkDelayTime(int[][] times, int N, int K) {
            var g = new int[N, N];
            for (var i = 0; i < N; ++i) {
                g[i, i] = 0;
                for (var j = i + 1; j < N; ++j) {
                    g[i, j] = int.MaxValue / 2;
                    g[j, i] = int.MaxValue / 2;
                }
            }
            foreach (var e in times) {
                var a = e[0] - 1;
                var b = e[1] - 1;
                var t = e[2];
                g[a, b] = t;
            }
            for (var k = 0; k < N; ++k) {
                for (var i = 0; i < N; ++i) {
                    for (var j = 0; j < N; ++j) {
                        g[i, j] = Math.Min(g[i, j], g[i, k] + g[k, j]);
                    }
                }
            }
            var min = 1;
            for (var i = 0; i < N; ++i) {
                if (g[K - 1, i] < int.MaxValue / 2) {
                    min = Math.Max(min, g[K - 1, i]);
                } else {
                    return -1;
                }
            }
            return min;
        }
    }
}
