using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0329 {
        public int LongestIncreasingPath(int[][] matrix) {
            return LongestIncreasingPath(matrix, matrix.Length, matrix[0].Length);
        }
        
        private int LongestIncreasingPath(int[][] matrix, int n, int m) {
            var memo = new int[n, m];
            for (var i = 0; i < n; ++i) {
                for (var j = 0; j < m; ++j) {
                    memo[i, j] = -1;
                }
            }
            var best = 1;
            for (var i = 0; i < n; ++i) {
                for (var j = 0; j < m; ++j) {
                    best = Math.Max(best, dfs(matrix, n, m, i, j, memo));
                }
            }
            return best;
        }
        
        private int dfs(int[][] matr, int n, int m, int x, int y, int[,] memo) {
            if (memo[x, y] == -1) {
                memo[x, y] = 1;
                for (var k = 0; k < 4; ++k) {
                    var u = x + dx[k];
                    var v = y + dy[k];
                    if (0 <= u && u < n && 0 <= v && v < m) {
                        if (matr[u][v] > matr[x][y]) {
                            memo[x, y] = Math.Max(memo[x, y], dfs(matr, n, m, u, v, memo) + 1);
                        }
                    }
                }
            }
            return memo[x, y];
        }
        
        private static readonly int[] dx = { -1, 0, +1, 0 };
        private static readonly int[] dy = { 0, -1, 0, +1 };
    }
}
