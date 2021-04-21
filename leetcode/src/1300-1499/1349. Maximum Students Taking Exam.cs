using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1349 {
        public int MaxStudents(char[][] seats) {
            return MaxStudents(seats, seats.Length, seats[0].Length);
        }

        private int MaxStudents(char[][] seats, int n, int m) {
            var mask = new int[n + 1];
            for (var i = 1; i <= n; ++i) {
                for (var j = 0; j < m; ++j) {
                    mask[i] = (mask[i] << 1) + (seats[i - 1][j] == '.' ? 1 : 0);
                }
            }
            var bits = new int[1 << m];
            for (var i = 1; i < 1 << m; ++i) {
                bits[i] = bits[i >> 1] + (i & 1);
            }
            var best = new int[n + 1, 1 << m];
            best[0, 0] = 0;
            for (var i = 1; i <= n; ++i) {
                for (var curr = 0; curr < 1 << m; ++curr) {
                    if ((mask[i] & curr) == curr && okay(curr, curr)) {
                        for (var last = 0; last < 1 << m; ++last) {
                            if ((mask[i - 1] & last) == last && okay(last, last)) {
                                if (okay(curr, last) && okay(last, curr)) {
                                    best[i, curr] = Math.Max(best[i, curr], best[i - 1, last] + bits[curr]);
                                }
                            }
                        }
                    }
                }
            }
            var answ = 0;
            for (var last = 0; last < 1 << m; ++last) {
                answ = Math.Max(answ, best[n, last]);
            }
            return answ;
        }

        private bool okay(int set1, int set2) {
            return (set1 & (set2 >> 1)) == 0;
        }
    }
}
