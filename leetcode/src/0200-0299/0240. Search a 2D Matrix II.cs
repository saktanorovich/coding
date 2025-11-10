using System;
using System.Collections.Generic;

namespace coding.leetcode {
    public class Solution_0240 {
        public bool SearchMatrix(int[,] matrix, int target) {
            var n = matrix.GetLength(0);
            var m = matrix.GetLength(1);
            if (n * m < 1) {
                return new Small(matrix, n, m).get(target);
            } else {
                return new Large(matrix, n, m).get(target);
            }
        }

        private class Small {
            private readonly int[,] matrix;
            private readonly int n;
            private readonly int m;

            public Small(int[,] matrix, int n, int m) {
                this.matrix = matrix;
                this.n = n;
                this.m = m;
            }

            public bool get(int target) {
                var i = 0;
                var j = m - 1;
                while (0 <= j && i < n) {
                    if (matrix[i, j] == target) {
                        return true;
                    }
                    if (matrix[i, j] < target) {
                        ++i;
                    } else {
                        --j;
                    }
                }
                return false;
            }
        }

        private class Large {
            private readonly int[,] matrix;
            private readonly int n;
            private readonly int m;

            public Large(int[,] matrix, int n, int m) {
                this.matrix = matrix;
                this.n = n;
                this.m = m;
            }

            public bool get(int target) {
                return get(target, 0, n - 1, 0, m - 1);
            }

            private bool get(int target, int i1, int i2, int j1, int j2) {
                if (i1 > i2) {
                    return false;
                }
                if (j1 > j2) {
                    return false;
                }
                if (matrix[i1, j1] > target) {
                    return false;
                }
                if (matrix[i2, j2] < target) {
                    return false;
                }
                var ii = (i1 + i2) / 2;
                var jj = (j1 + j2) / 2;
                if (matrix[ii, jj] == target) {
                    return true;
                }
                if (matrix[ii, jj] > target) {
                    return get(target, i1, ii - 1, j1, j2)
                        || get(target, ii, i2, j1, jj - 1);
                } else {
                    return get(target, i1, i2, jj + 1, j2)
                        || get(target, ii + 1, i2, j1, jj);
                }
            }
        }
    }
}