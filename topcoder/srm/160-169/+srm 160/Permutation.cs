using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TopCoder.Algorithm {
      public class Permutation {
            public string best(int n) {
                  List<int> primes = new List<int>(new int[] { 1, 2 });
                  for (int number = 3; number <= 2 * n; number += 2) {
                        for (int factor = 3; factor * factor <= number; factor += 2) {
                              if (number % factor == 0) {
                                    goto next;
                              }
                        }
                        primes.Add(number);
                        next:;
                  }
                  List<int> permutation = getPermutation(getCycleLengths(n, primes.Count, primes));
                  string result = string.Empty;
                  foreach (int element in permutation) {
                        result += alphabet[element].ToString();
                  }
                  return result;
            }

            private List<int> getPermutation(List<int> cycleLengths) {
                  List<int> result = new List<int>();
                  int position = 0;
                  foreach (int length in cycleLengths) {
                        for (int where = position + 1; where < position + length;) {
                              result.Add(where);
                              where = where + 1;
                        }
                        result.Add(position);
                        position += length;
                  }
                  return result;
            }

            private List<int> getCycleLengths(int n, int m, List<int> primes) {
                  int[,] lcm = new int[n + 1, m];
                  int[,] pow = new int[n + 1, m];
                  int[,] hoc = new int[n + 1, m];
                  for (int num = 0; num <= n; ++num) {
                        for (int curr = 0; curr < m; ++curr) {
                              lcm[num, curr] = 1;
                              pow[num, curr] = 1;
                        }
                  }
                  for (int num = 1; num <= n; ++num) {
                        for (int curr = 1; primes[curr] <= num; ++curr) {
                              for (int factor = primes[curr]; factor <= num; factor *= primes[curr]) {
                                    for (int prev = 0; prev < curr; ++prev) {
                                          if (lcm[num, curr] < lcm[num - factor, prev] * factor) {
                                                lcm[num, curr] = lcm[num - factor, prev] * factor;
                                                pow[num, curr] = factor;
                                                hoc[num, curr] = prev;
                                          }
                                    }
                              }
                        }
                  }
                  int best = 0;
                  for (int curr = 1; primes[curr] <= n; ++curr) {
                        if (lcm[n, best] < lcm[n, curr]) {
                              best = curr;
                        }
                  }
                  List<int> result = new List<int>();
                  while (n > 0) {
                        result.Add(pow[n, best]);
                        int next = hoc[n, best];
                        n -= pow[n, best];
                        best = next;
                  }
                  result.Sort();
                  return result;
            }

            private readonly string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
      }
}