using System;

namespace interview.hackerrank {
    public class NumberOfPathsInAMatrix {
        private readonly long modulo = 1000000007;

        public int count(int[,] a, int m, int n) {
            var memo = new long[m, n];
            memo[0, 0] = a[0, 0];
            for (var i = 0; i < m; ++i) {
                for (var j = 0; j < n; ++j) {
                    if (i + j != 0) {
                        if (i > 0) {
                            memo[i, j] += a[i, j] * memo[i - 1, j];
                            memo[i, j] %= modulo;
                        }
                        if (j > 0) {
                            memo[i, j] += a[i, j] * memo[i, j - 1];
                            memo[i, j] %= modulo;
                        }
                    }
                }
            }
            return (int)memo[m - 1, n - 1];
        }
    }
}
