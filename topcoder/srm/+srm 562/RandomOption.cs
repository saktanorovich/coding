using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithms {
      public class RandomOption {
            public double getProbability(int keyCount, int[] badLane1, int[] badLane2) {
                  bool[,] bad = new bool[keyCount, keyCount];
                  for (int i = 0; i < badLane1.Length; ++i) {
                        bad[badLane1[i], badLane2[i]] = true;
                        bad[badLane2[i], badLane1[i]] = true;
                  }
                  double result = getPermutationsCount(keyCount, bad);
                  for (int i = 1; i <= keyCount; ++i) {
                        result /= i;
                  }
                  return result;
            }

            private readonly long[,] memo = new long[1 << 14, 14];

            /* returns the number of permutations of the set {1, ..., n} where no adjacent
             * positions i and j such that bad[i, j] = true or bad[j, i] = true... */
            private long getPermutationsCount(int n, bool[,] bad) {
                  int set = (1 << n) - 1;
                  for (int j = 0; j < n; ++j) {
                        memo[0, j] = 1;
                        for (int i = 1; i <= set; ++i) {
                              memo[i, j] = -1;
                        }
                  }
                  long result = 0;
                  for (int ix = 0; ix < n; ++ix) {
                        result += doit(n, set ^ (1 << ix), ix, bad);
                  }
                  return result;
            }

            private long doit(int n, int set, int last, bool[,] bad) {
                  if (memo[set, last] == -1) {
                        memo[set, last] = 0;
                        for (int next = 0; next < n; ++next) {
                              if ((set & (1 << next)) != 0) {
                                    if (!bad[next, last]) {
                                          memo[set, last] += doit(n, set ^ (1 << next), next, bad);
                                    }
                              }
                        }
                  }
                  return memo[set, last];
            }

            public static void Main(string[] args) {
                  Console.WriteLine(new RandomOption().getProbability(5, new int[] { 0 }, new int[] { 3 }));

                  Console.WriteLine("Press any key to continue...");
                  Console.ReadLine();
            }
      }
}