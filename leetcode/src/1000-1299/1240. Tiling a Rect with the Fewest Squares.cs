using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1240 {
        public int TilingRectangle(int n, int m) {
            if (n < m) {
                rec(n, m);
            } else {
                rec(m, n);
            }
            return best;
        }

        private void rec(int n, int m) {
            mark = new bool[n, m];
            best = n * m;
            rec(n, m, n * m, 0);
        }

        private void rec(int n, int m, int a, int have) {
            if (a == 0) {
                best = Math.Min(best, have);
                return;
            }
            for (var x = 0; x < n; ++x) {
                for (var y = 0; y < m; ++y) {
                    if (mark[x, y] == false) {
                        var k = Math.Min(n - x, m - y);
                        for (; k > 0; --k) {
                            if (get(x, y, k)) {
                                if (have + 1 < best) {
                                    set(x, y, k, true);
                                    rec(n, m, a - k * k, have + 1);
                                    set(x, y, k, false);
                                }
                            }
                        }
                        return;
                    }
                }
            }
        }

        private bool get(int x, int y, int k) {
            for (var i = 0; i < k; ++i) {
                for (var j = 0; j < k; ++j) {
                    if (mark[x + i, y + j]) {
                        return false;
                    }
                }
            }
            return true;
        }

        private void set(int x, int y, int k, bool v) {
            for (var i = 0; i < k; ++i) {
                for (var j = 0; j < k; ++j) {
                    mark[x + i, y + j] = v;
                }
            }
        }

        private bool[,] mark;
        private int best;
    }
}
