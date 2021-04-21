using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1284 {
        public int MinFlips(int[][] mat) {
            return MinFlips(mat, mat.Length, mat[0].Length);
        }

        private int MinFlips(int[][] mat, int n, int m) {
            var res = 100;
            for (var set = 0; set < 1 << (n * m); ++set) {
                if (okay(mat, n, m, set)) {
                    res = Math.Min(res, bits(set));
                }
            }
            return res < 100 ? res : -1;
        }

        private bool okay(int[][] mat, int n, int m, int set) {
            var c = new int[n, m];
            for (var x = 0; x < n; ++x) {
                for (var y = 0; y < m; ++y) {
                    c[x, y] = mat[x][y];
                }
            }
            for (var i = 0; i < n * m; ++i) {
                if ((set & (1 << i)) != 0) {
                    var x = i / m;
                    var y = i % m;
                    c[x, y] ^= 1;
                    for (var k = 0; k < 4; ++k) {
                        var a = x + dx[k];
                        var b = y + dy[k];
                        if (0 <= a && a < n && 0 <= b && b < m) {
                            c[a, b] ^= 1;
                        }
                    }
                }
            }
            for (var x = 0; x < n; ++x) {
                for (var y = 0; y < m; ++y) {
                    if (c[x, y] != 0) {
                        return false;
                    }
                }
            }
            return true;
        }

        private int bits(int set) {
            if (set > 0) {
                return 1 + bits(set & (set - 1));
            }
            return 0;
        }

        private static readonly int[] dx = { -1, 0, +1, 0 };
        private static readonly int[] dy = { 0, -1, 0, +1 };
    }
}
