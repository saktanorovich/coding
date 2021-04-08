using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1478 {
        public int MinDistance(int[] houses, int k) {
            return MinDistance(houses, houses.Length, k);
        }

        private int MinDistance(int[] h, int H, int K) {
            Array.Sort(h);
            var d = new int[H + 1, H + 1];
            for (var i = 1; i <= H; ++i) {
                for (var j = i + 1; j <= H; ++j) {
                    var x = (i + j) / 2;
                    for (var p = i; p <= j; ++p) {
                        d[i, j] += Math.Abs(h[p - 1] - h[x - 1]);
                    }
                }
            }
            var f = new int[H + 1, K + 1];
            for (var i = 0; i <= H; ++i) {
                for (var k = 0; k <= K; ++k) {
                    f[i, k] = int.MaxValue / 2;
                }
            }
            f[0, 0] = 0;
            for (var i = 1; i <= H; ++i) {
                for (var k = 1; k <= K; ++k) {
                    for (var j = 0; j < i; ++j) {
                        f[i, k] = Math.Min(f[i, k], f[j, k - 1] + d[j + 1, i]);
                    }
                }
            }
            return f[H, K];
        }
    }
}
