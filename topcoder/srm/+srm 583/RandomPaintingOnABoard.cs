using System;

namespace TopCoder.Algorithm {
      public class RandomPaintingOnABoard {
            public double expectedSteps(string[] prob) {
                  return expectedSteps(Array.ConvertAll(prob, delegate(string s) {
                        return Array.ConvertAll(s.ToCharArray(), delegate(char c) {
                              return c - '0';
                        });
                  }), prob.Length, prob[0].Length);
            }

            /**
             * The expected value E = 1 * p[1] + 2 * p[2] + ... + k * p[k] + ... = n[0] + n[1] + n[2] + ... + n[k] + ...
             * where p[i] is probability that we finish after i steps and n[i] is probability that we
             * do not finish after i steps. The relation between these two functions is n[i] = p[i + 1] + n[i + 1].
             */
            private double expectedSteps(int[][] relativeProb, int rowsCount, int columnsCount) {
                  if (rowsCount < columnsCount) {
                        return expectedSteps(transpose(relativeProb, rowsCount, columnsCount), columnsCount, rowsCount);
                  }
                  else {
                        int totalSum = 0;
                        for (int i = 0; i < rowsCount; ++i) {
                              for (int j = 0; j < columnsCount; ++j) {
                                    totalSum += relativeProb[i][j];
                              }
                        }
                        /**
                         * Denoting by t[s, k] probability that set s of rows/cols would not be selected we can write
                         * the expression for n[k] as sigma(t[s, k], for all s).
                         * 
                         * Because such expression is difficul to calculate we introduce a new variable t'[s, k] which equals to probability
                         * that at least set s of rows/cols would not be selected after k steps. Then using inclusion-exclusion principle
                         * we can conclude that n[k] = sigma(t'[s, k], |s| is odd) - sigma(t'[s, k], |s| is even).
                         * 
                         * The expression for t'[s, k] can be obtained by introducing a new variable p[u] which equals to probability that at least
                         * one element of set u would be selected. Then t'[s, k] = p[S \ s] ^ k. Note that p[u] = sigma(u[i]) / T.
                         */
                        long[] diffOccurence = new long[totalSum];
                        for (int set = 0; set < (1 << columnsCount); ++set) {
                              int[] prob = new int[rowsCount];
                              for (int column = 0; column < columnsCount; ++column) {
                                    if ((set & (1 << column)) == 0) {
                                          for (int row = 0; row < rowsCount; ++row) {
                                                prob[row] += relativeProb[row][column];
                                          }
                                    }
                              }
                              long[,] occurence = new long[totalSum + 1, 2];
                              occurence[0, parity(set)] = 1;
                              for (var row = 0; row < rowsCount; ++row) {
                                    if (prob[row] > 0) {
                                          for (int total = totalSum - prob[row]; total >= 0; --total) {
                                                for (int level = 0; level < 2; ++level) {
                                                      occurence[total + prob[row], level] += occurence[total, level ^ 1];
                                                }
                                          }
                                    }
                                    else {
                                          for (int total = 0; total < totalSum; ++total) {
                                                long sum0 = occurence[total, 0];
                                                long sum1 = occurence[total, 1];
                                                occurence[total, 0] += sum1;
                                                occurence[total, 1] += sum0;
                                          }

                                    }
                              }
                              for (int s = 0; s < totalSum; ++s) {
                                    diffOccurence[s] += occurence[s, 0] - occurence[s, 1];
                              }
                        }
                        double result = 0.0;
                        for (int u = 0; u < totalSum; ++u) {
                              result += 1.0 * diffOccurence[u] * totalSum / (totalSum - u);
                        }
                        return Math.Abs(result);
                  }
            }

            private int[][] transpose(int[][] a, int n, int m) {
                  int[][] result = new int[m][];
                  for (int i = 0; i < m; ++i) {
                        result[i] = new int[n];
                        for (int j = 0; j < n; ++j) {
                              result[i][j] = a[j][i];
                        }
                  }
                  return result;
            }

            private int parity(int set) {
                  if (set > 0) {
                        return 1 ^ parity(set & (set - 1));
                  }
                  return 0;
            }

            internal static void Main(string[] args) {
                  Console.WriteLine(new RandomPaintingOnABoard().expectedSteps(new string[] { "10", "01" })); // Returns: 3.0
                  Console.WriteLine(new RandomPaintingOnABoard().expectedSteps(new string[] { "11", "11" })); // Returns: 3.6666666666666665
                  Console.WriteLine(new RandomPaintingOnABoard().expectedSteps(new string[] { "11", "12" })); // Returns: 3.9166666666666665
                  Console.WriteLine(new RandomPaintingOnABoard().expectedSteps(new string[] { "0976", "1701", "7119" })); // Returns: 11.214334077031307
                  Console.WriteLine(new RandomPaintingOnABoard().expectedSteps(new string[] {
                        "000000000000001000000",
                        "888999988889890999988",
                        "988889988899980889999",
                        "889898998889980999898",
                        "988889999989880899999",
                        "998888998988990989998",
                        "998988999898990889899"})); // Returns: 1028.7662876159634

                  Console.WriteLine("\nPress any key to continue...");
                  Console.ReadKey();
            }
      }
}
