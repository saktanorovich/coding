using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1467 {
        public double GetProbability(int[] balls) {
            return GetProbability(balls, balls.Length, balls.Sum() / 2);
        }

        private double GetProbability(int[] balls, int K, int N) {
            // Let's analyse only unique permutations of balls because any
            // ball b with freq nb > 1 will add nb! equal permutations. So
            // the answer to the problem is V/T where
            //   V is a number of valid permutations,
            //   T is a number of total permutations.
            // It is enough to consider only combinations of the balls because
            // problem statement asks to analyze balls colors and does not ask
            // about their ordering. In that case we have to consider balls
            // with the same color as different balls.
            var C = new long[2 * N + 1, 2 * N + 1];
            for (int i = 0; i <= 2 * N; ++i) {
                C[i, 0] = 1;
                for (int j = 1; j <= i; ++j) {
                    C[i, j] = C[i - 1, j] + C[i - 1, j - 1];
                }
            }
            // Let's denote by F[t1, t2, d1, d2] the number of ways to put
            //   t1 balls into the box1,
            //   t2 balls into the box2
            // having
            //  d1 distinct balls in the box1,
            //  d2 distinct balls in the box2.
            var F = new long[K + 1, N + 1, N + 1, K + 1, K + 1];
            F[0, 0, 0, 0, 0] = 1;
            for (var i = 1; i <= K; ++i) {
                var b = balls[i - 1];
                for (var t1 = 0; t1 <= N; ++t1) {
                    for (var t2 = 0; t2 <= N; ++t2) {
                        for (var d1 = 0; d1 <= K; ++d1) {
                            for (var d2 = 0; d2 <= K; ++d2) {
                                for (var t = 0; t <= b; ++t) {
                                    var a1 = t;
                                    var a2 = b - t;
                                    var r1 = a1 > 0 ? 1 : 0;
                                    var r2 = a2 > 0 ? 1 : 0;
                                    if (t1 + a1 <= N && t2 + a2 <= N && d1 + r1 <= K && d2 + r2 <= K) {
                                        F[i, t1 + a1, t2 + a2, d1 + r1, d2 + r2] += C[b, t] * F[i - 1, t1, t2, d1, d2];
                                    }
                                }
                            }
                        }
                    }
                }
            }
            var total = C[2 * N, N];
            var valid = 0L;
            for (var k = 1; k <= K; ++k) {
                valid += F[K, N, N, k, k];
            }
            return 1.0 * valid / total;
        }
    }
}
