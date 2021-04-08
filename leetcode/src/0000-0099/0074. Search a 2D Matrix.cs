using System;
using System.Collections.Generic;

namespace coding.leetcode {
    public class Solution_0074 {
        public bool SearchMatrix(int[][] matrix, int target) {
            var n = matrix.Length;
            if (n == 0) {
                return false;
            }
            var m = matrix[0].Length;
            if (m == 0) {
                return false;
            }
            var i = 0;
            var j = n * m - 1;
            while (i < j) {
                var x = (i + j) / 2;
                var p = x / m;
                var q = x % m;
                if (matrix[p][q] == target) {
                    return true;
                }
                if (matrix[p][q] < target) {
                    i = x + 1;
                } else {
                    j = x - 1;
                }
            }
            return matrix[i / m][i % m] == target;
        }
    }
}