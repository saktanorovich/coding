using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1605 {
        public int[][] RestoreMatrix(int[] rowSum, int[] colSum) {
            return RestoreMatrix(rowSum, rowSum.Length, colSum, colSum.Length);
        }

        private int[][] RestoreMatrix(int[] rowSum, int n, int[] colSum, int m) {
            // the problem can be solved by the application of max-flow algorithm
            // but due to specific network topology let's do faster
            var res = new int[n][];
            for (var i = 0; i < n; ++i) {
                res[i] = new int[m];
            }
            for (var i = 0; i < n; ++i) {
                for (var j = 0; j < m; ++j) {
                    var f = Math.Min(rowSum[i], colSum[j]);
                    res[i][j] += f;
                    rowSum[i] -= f;
                    colSum[j] -= f;
                }
            }
            return res;
        }
    }
}
