using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0861 {
        public int MatrixScore(int[][] A) {
            return MatrixScore(A, A.Length, A[0].Length);
        }

        private int MatrixScore(int[][] A, int n, int m) {
            var res = (1 << (m - 1)) * n;
            for (var i = 0; i < n; ++i) {
                if (A[i][0] == 0)  {
                    for (var j = 0; j < m; ++j) {
                        A[i][j] ^= 1;
                    }
                }
            }
            for (var j = 1; j < m; ++j) {
                var z = 0;
                for (var i = 0; i < n; ++i) {
                    if (A[i][j] == 0) {
                        z++;
                    }
                }
                if (z > n - z) {
                    res += (1 << (m - j - 1)) * (z);
                } else {
                    res += (1 << (m - j - 1)) * (n - z);
                }
            }
            return res;
        }
    }
}
