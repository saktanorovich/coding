using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class TreeCount {
            public int[] count(string[] graph) {
                  return count(Array.ConvertAll(graph, delegate(string s) {
                        List<int> adjacency = new List<int>();
                        for (int i = 0; i < s.Length; ++i) {
                              if (s[i].Equals('Y')) {
                                    adjacency.Add(i);
                              }
                        }
                        return adjacency;
                  }));
            }

            private int[] count(List<int>[] graph) {
                  /* dp[node, kstable, used] -- the number of kstable sets in the tree rooted at node considering that node is used. */
                  dp = new long[graph.Length, graph.Length, 2];
                  int[] result = new int[graph.Length];
                  for (int kstable = 0; kstable < graph.Length; ++kstable) {
                        for (int node = 0; node < graph.Length; ++node) {
                              for (int kfactor = 0; kfactor < graph.Length; ++kfactor) {
                                    dp[node, kfactor, 0] = dp[node, kfactor, 1] = undefined;
                              }
                        }
                        result[kstable] = (int)((run(graph, 0, -1, kstable, kstable, 0) + run(graph, 0, -1, kstable, kstable, 1)) % modulo);
                  }
                  return result;
            }

            private const long modulo = 112901989;
            private const long undefined = -1;
            private long[,,] dp;

            private long run(List<int>[] graph, int curr, int prev, int kstable, int kfactor, int used) {
                  if (dp[curr, kfactor, used] == undefined) {
                        if (used == 0) {
                              dp[curr, kfactor, 0] = 1;
                              foreach (int next in graph[curr]) {
                                    if (next != prev) {
                                          /* we should use multiplication because empty set {} will be presented in each descendant subtree... */
                                          dp[curr, kfactor, 0] *= run(graph, next, curr, kstable, kstable, 0) + run(graph, next, curr, kstable, kstable, 1);
                                          dp[curr, kfactor, 0] %= modulo;
                                    }
                              }
                        }
                        else {
                              /* calculate answer for each child of the current node... */
                              int childrenCount = 0;
                              List<long> fuse = new List<long>();
                              List<long> fnot = new List<long>();
                              foreach (int next in graph[curr]) {
                                    if (next != prev) {
                                          long countForUse = 0;
                                          long countForNot = run(graph, next, curr, kstable, kstable, 0);
                                          if (kfactor > 0) {
                                                countForUse = run(graph, next, curr, kstable, kstable - 1, 1);
                                          }
                                          fuse.Add(countForUse);
                                          fnot.Add(countForNot);
                                          childrenCount = childrenCount + 1;
                                    }
                              }
                              /* comb[childrenCount, kfactor] -- the number of ways to make kfactor sets with root considering
                               * that root is connected to kfactor childs. */
                              long[,] comb = new long[childrenCount + 1, kfactor + 1];
                              comb[0, 0] = 1;
                              for (int i = 1; i <= childrenCount; ++i) {
                                    for (int j = 0; j <= Math.Min(i, kfactor); ++j) {
                                          comb[i, j] += comb[i - 1, j] * fnot[i - 1];
                                          comb[i, j] %= modulo;
                                          if (j > 0) {
                                                comb[i, j] += comb[i - 1, j - 1] * fuse[i - 1];
                                                comb[i, j] %= modulo;
                                          }
                                    }
                              }
                              dp[curr, kfactor, 1] = 0;
                              for (int k = 0; k <= kfactor; ++k) {
                                    dp[curr, kfactor, 1] += comb[childrenCount, k];
                                    dp[curr, kfactor, 1] %= modulo;
                              }
                        }
                  }
                  return dp[curr, kfactor, used];
            }

            private static string ToString<T>(T[] a) {
                  string result = string.Empty;
                  for (int i = 0; i < a.Length; ++i) {
                        result += a[i].ToString();
                        if (i + 1 < a.Length) {
                              result += ' ';
                        }
                  }
                  return "[" + result + "]";
            }

            public static void Main() {
                  Console.WriteLine(ToString(new TreeCount().count(new[] { "NYY", "YNN", "YNN" })));// { 5, 7, 8 }
                  Console.WriteLine(ToString(new TreeCount().count(new[] { "N" })));// { 2 }
                  Console.WriteLine(ToString(new TreeCount().count(new[] {
                        "NYNNNYY", "YNNNNNN", "NNNNNNY", "NNNNNNY", "NNNNNNY", "YNNNNNN", "YNYYYNN" })));// { 44, 73, 104, 124, 128, 128, 128 }
                  Console.WriteLine(ToString(new TreeCount().count(new[] { "NY", "YN" })));// { 3, 4 }
                  Console.WriteLine(ToString(new TreeCount().count(new[] {
                        "NYYYYY", "YNNNNN", "YNNNNN", "YNNNNN", "YNNNNN", "YNNNNN" })));// { 33, 38, 48, 58, 63, 64 }
                  Console.WriteLine(ToString(new TreeCount().count(new[] {
                        "NNNYN", "NNYNN", "NYNNY", "YNNNY", "NNYYN"})));// { 13, 24, 32, 32, 32 }

                  Console.WriteLine("Press any key to continue...");
                  Console.ReadKey();
            }
      }
}