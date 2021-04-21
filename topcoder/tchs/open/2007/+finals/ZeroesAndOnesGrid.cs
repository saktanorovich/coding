using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class ZeroesAndOnesGrid {
        public int minMovesCount(string[] table) {
            return minMovesCount(table, table.Length, table[0].Length);
        }

        private static int minMovesCount(string[] table, int n, int m) {
            var turn = new int[n + 1, m + 1];
            for (var i = 1; i <= n; ++i) {
                for (var j = 1; j <= m; ++j) {
                    if (table[i - 1][j - 1] == '1') {
                        ++turn[i, j];
                        ++turn[i - 1, j];
                        ++turn[i, j - 1];
                        ++turn[i - 1, j - 1];
                    }
                }
            }
            var res = 0;
            for (var i = 1; i <= n; ++i) {
                for (var j = 1; j <= m; ++j) {
                    res += turn[i, j] & 1;
                }
            }
            return res;
        }
    }
}
