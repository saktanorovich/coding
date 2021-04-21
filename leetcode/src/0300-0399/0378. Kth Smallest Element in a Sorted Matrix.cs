using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0378 {
        public int KthSmallest(int[][] matrix, int k) {
            return KthSmallest(matrix, matrix.Length, matrix[0].Length, k);
        }

        private int KthSmallest(int[][] matrix, int n, int m, int k) {
            int lo = matrix[0][0], hi = matrix[n - 1][m - 1];
            while (lo < hi) {
                var x = (lo + hi) / 2;
                if (count(matrix, n, m, x) < k) {
                    lo = x + 1;
                } else {
                    hi = x;
                }
            }
            return lo;
        }

        // count number of elements less than or equal to x
        private int count(int[][] matrix, int n, int m, int x) {
            var r = 0;
            var i = 0;
            var j = m - 1;
            while (0 <= j && i < n) {
                if (matrix[i][j] > x) {
                    j -= 1;
                } else {
                    r += j + 1;
                    i += 1;
                }
            }
            return r;
        }
    }
}
