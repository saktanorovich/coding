using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1727 {
        public int LargestSubmatrix(int[][] matrix) {
            return LargestSubmatrix(matrix, matrix.Length, matrix[0].Length);
        }
        private int LargestSubmatrix(int[][] matrix, int n, int m) {
            var height = new int[m];
            var buffer = new int[m];
            var result = 0;
            for (var i = 0; i < n; ++i) {
                for (var j = 0; j < m; ++j) {
                    height[j] += matrix[i][j];
                    if (matrix[i][j] == 0) {
                        height[j] = 0;
                    }
                    buffer[j] = height[j];
                }
                Array.Sort(buffer);
                for (var k = 0; k < m; ++k) {
                    result = Math.Max(result, buffer[k] * (m - k));
                }
            }
            return result;
        }
    }
}