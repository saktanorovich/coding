using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1267 {
        public int CountServers(int[][] grid) {
            return CountServers(grid, grid.Length, grid[0].Length);
        }

        private int CountServers(int[][] grid, int n, int m) {
            var rows = new int[n];
            var cols = new int[m];
            for (var i = 0; i < n; ++i) {
                for (var j = 0; j < m; ++j) {
                    rows[i] += grid[i][j];
                    cols[j] += grid[i][j];
                }
            }
            var res = 0;
            for (var i = 0; i < n; ++i) {
                for (var j = 0; j < m; ++j) {
                    if (grid[i][j] == 1) {
                        if (rows[i] > 1 || cols[j] > 1) {
                            res = res + 1;
                        }
                    }
                }
            }
            return res;
        }
    }
}
