using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class Flags {
            public long numStripes(string numFlags, string[] forbidden) {
                  return numStripes(long.Parse(numFlags), Array.ConvertAll(forbidden,
                        delegate(string s) {
                              return new List<int>(Array.ConvertAll(s.Split(new char[] { ' ' }),
                                    delegate(string x) {
                                          return int.Parse(x);
                              }));
                  }), forbidden.Length);
            }

            private long numStripes(long numOfFlags, List<int>[] forbidden, int numOfColors) {
                  long[,] graph = new long[numOfColors, numOfColors]; /* a path of length n represents a flag with (n+1) stripes... */
                  long[,] paths = new long[numOfColors, numOfColors];
                  for (int i = 0; i < numOfColors; ++i) {
                        for (int j = 0; j < numOfColors; ++j) {
                              graph[i, j] = 1;
                              if (forbidden[i].Contains(j)) {
                                    graph[i, j] = 0;
                              }
                        }
                        paths[i, i] = 1;
                  }
                  if (val(mul(graph, graph, numOfColors), numOfColors) > val(graph, numOfColors)) {
                        for (long count = numOfColors, numOfStripes = 1; true; ++numOfStripes) {
                              if (count >= numOfFlags) {
                                    return numOfStripes;
                              }
                              paths = mul(paths, graph, numOfColors);
                              count = count + val(paths, numOfColors);
                        }
                  }
                  return 1 + (numOfFlags - numOfColors) / val(graph, numOfColors);
            }

            private static long[,] mul(long[,] a, long[,] b, int n) {
                  long[,] result = new long[n, n];
                  for (int i = 0; i < n; ++i) {
                        for (int j = 0; j < n; ++j) {
                              for (int k = 0; k < n; ++k) {
                                    result[i, j] = result[i, j] + a[i, k] * b[k, j];
                              }
                        }
                  }
                  return result;
            }

            private static long val(long[,] a, int n) {
                  long result = 0;
                  for (int i = 0; i < n; ++i) {
                        for (int j = 0; j < n; ++j) {
                              result += a[i, j];
                        }
                  }
                  return result;
            }
      }
}