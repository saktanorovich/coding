using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class LittleElephantAndPermutationDiv1 {
            private const long modulo = 1000000007;
            private const long undefined = -1;

            public int getNumber(int n, int k) {
                  long result = getNumberForOrdered(n, k);
                  if (result > 0) {
                        for (int i = 1; i <= n; ++i) {
                              result = (result * i) % modulo;
                        }
                  }
                  return (int)result;
            }

            /* Returns a number of matchings M such that |M| ≥ K where |M| = sum(|e|), |e| = max(e1, e2)
             * where e1 is an element from the 1st row, e2 is an element from the 2nd row,
             * the rows are ordered in inc (or dec) order.
             *
             * dp[current, required, firstToSecond, secondToFirst]-- a number of matchings
             * where current is current number, required is minimum sum to collect,
             * firstToSecond is a number of edges (e1, e2) such that e1 > e2,
             * secondToFirst is a number of edges (e2, e1) such that e2 > e1.
             * For impl it is enough to have a one parameter because rows are symmetric.*/

            private long[,,] cache;

            private long getNumberForOrdered(int n, int k) {
                  cache = new long[n + 1, k + 1, n + 1];
                  for (int i = 0; i <= n; ++i) {
                        for (int j = 0; j <= k; ++j) {
                              for (int p = 0; p <= n; ++p) {
                                    cache[i, j, p] = undefined;
                              }
                        }
                  }
                  cache[0, 0, 0] = 1;
                  return run(n, k, 0);
            }

            /**/
            private long run(int current, int required, int moreToLess) {
                  required = Math.Max(required, 0);
                  if (cache[current, required, moreToLess] == undefined) {
                        long result = 0;
                        if (current > 0) {
                              result += run(current - 1, required - current, moreToLess);
                              result += run(current - 1, required - 2 * current, moreToLess + 1);
                              if (moreToLess > 0) {
                                    result += moreToLess * moreToLess * run(current - 1, required, moreToLess - 1);
                                    result += moreToLess * run(current - 1, required - current, moreToLess);
                                    result += moreToLess * run(current - 1, required - current, moreToLess);
                              }
                        }
                        result %= modulo;
                        cache[current, required, moreToLess] = result;
                  }
                  return cache[current, required, moreToLess];
            }
            /**
            private IDictionary<long, long>[,] cache;
            private long[] factorial;

            private long getNumberForOrdered(int n, int k) {
                  cache = new IDictionary<long, long>[n + 1, k + 1];
                  for (int i = 0; i <= n; ++i) {
                        for (int j = 0; j <= k; ++j) {
                              cache[i, j] = new SortedDictionary<long, long>();
                        }
                  }
                  factorial = new long[n + 1];
                  for (long i = (factorial[0] = 1); i <= n; ++i) {
                        factorial[i] = (factorial[i - 1] * i) % modulo;
                  }
                  return run(n, k, (1L << n) - 1);
            }

             private long bruteForce(int current, int required, long state) {
                  if (required <= current) {
                        return factorial[current];
                  }
                  long result = 0;
                  if (!cache[current, required].TryGetValue(state, out result)) {
                        for (int i = 0; (state >> i) != 0; ++i) {
                              if ((state & (1L << i)) > 0) {
                                    result += bruteForce(current - 1, required - Math.Max(current, i + 1), state ^ (1L << i));
                              }
                        }
                        result %= modulo;
                        cache[current, required].Add(state, result);
                  }
                  return result;
            }
            /**/
            internal static void Main(string[] args) {
                  Console.WriteLine(new LittleElephantAndPermutationDiv1().getNumber(1, 1)); // Returns: 1
                  Console.WriteLine(new LittleElephantAndPermutationDiv1().getNumber(2, 1)); // Returns: 4
                  Console.WriteLine(new LittleElephantAndPermutationDiv1().getNumber(3, 8)); // Returns: 18
                  Console.WriteLine(new LittleElephantAndPermutationDiv1().getNumber(10, 74)); // Returns: 484682624
                  Console.WriteLine(new LittleElephantAndPermutationDiv1().getNumber(50, 1000)); // Returns: 539792695
                  Console.WriteLine(new LittleElephantAndPermutationDiv1().getNumber(50, 1500));

                  Console.WriteLine("\nPress any key to continue...");
                  Console.ReadKey();
            }
      }
}
