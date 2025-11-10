using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class CandyGame {
            public int maximumCandy(string[] graph, int target) {
                  return maximumCandy(Array.ConvertAll(graph, delegate(string s) {
                        List<int> result = new List<int>();
                        for (int i = 0; i < s.Length; ++i) {
                              if (s[i].Equals('Y')) {
                                    result.Add(i);
                              }
                        }
                        return result;
                  }), target);
            }

            private int maximumCandy(List<int>[] graph, int target) {
                  int[] maximumDepth = new int[graph.Length];
                  calcMaximumDepth(target, -1, graph, maximumDepth);
                  for (int i = 0; i < graph.Length; ++i) {
                        if (maximumDepth[i] < 1) {
                              return -1;
                        }
                  }
                  long result = 0;
                  foreach (int child in graph[target]) {
                        result += getMaximumCandy(child, target, graph, maximumDepth, 1);
                  }
                  return (result > (long)2e9 ? -1 : (int)result);
            }

            private long getMaximumCandy(int curr, int prev, List<int>[] graph, int[] maximumDepth, int candy) {
                  int best = -1;
                  foreach (int node in graph[curr]) {
                        if (node != prev) {
                              if (best == -1 || maximumDepth[node] > maximumDepth[best]) {
                                    best = node;
                              }
                        }
                  }
                  long result = 0;
                  if (best != -1) {
                        result = getMaximumCandy(best, curr, graph, maximumDepth, 2 * candy + 1);
                        foreach (int next in graph[curr]) {
                              if (next != prev && next != best) {
                                    result += getMaximumCandy(next, curr, graph, maximumDepth, 1);
                              }
                        }
                  }
                  return Math.Max(result, candy);
            }

            private void calcMaximumDepth(int curr, int prev, List<int>[] graph, int[] maximumDepth) {
                  maximumDepth[curr] = 1;
                  foreach (int next in graph[curr]) {
                        if (next != prev) {
                              calcMaximumDepth(next, curr, graph, maximumDepth);
                              maximumDepth[curr] = Math.Max(maximumDepth[curr], maximumDepth[next] + 1);
                        }
                  }
            }

            public static void Main(string[] args) {
                  Console.WriteLine(new CandyGame().maximumCandy(new string[] {"NYN", "YNY", "NYN"}, 1));
                  Console.WriteLine(new CandyGame().maximumCandy(new string[] {"NYN", "YNY", "NYN" }, 2));
                  Console.WriteLine(new CandyGame().maximumCandy(new string[] {"NYYY", "YNNN", "YNNN", "YNNN"}, 0));
                  Console.WriteLine(new CandyGame().maximumCandy(new string[] {"NYYY", "YNNN", "YNNN", "YNNN" }, 1));
                  Console.WriteLine(new CandyGame().maximumCandy(new string[] {"NYNNNYN", "YNYNYNN", "NYNYNNN", "NNYNNNN", "NYNNNNN", "YNNNNNY", "NNNNNYN"}, 0));

                  Console.WriteLine(new CandyGame().maximumCandy(new string[] {
                        "NYNNNNNNNNNNNNNNNNNNNNNNNNNNNNNN", "YNYNNNNNNNNNNNNNNNNNNNNNNNNNNNNN", "NYNYNNNNNNNNNNNNNNNNNNNNNNNNNNNN",
                        "NNYNYNNNNNNNNNNNNNNNNNNNNNNNNNNN", "NNNYNYNNNNNNNNNNNNNNNNNNNNNNNNNN", "NNNNYNYNNNNNNNNNNNNNNNNNNNNNNNNN",
                        "NNNNNYNYNNNNNNNNNNNNNNNNNNNNNNNN", "NNNNNNYNYNNNNNNNNNNNNNNNNNNNNNNN", "NNNNNNNYNYNNNNNNNNNNNNNNNNNNNNNN",
                        "NNNNNNNNYNYNNNNNNNNNNNNNNNNNNNNN", "NNNNNNNNNYNYNNNNNNNNNNNNNNNNNNNN", "NNNNNNNNNNYNYNNNNNNNNNNNNNNNNNNN",
                        "NNNNNNNNNNNYNYNNNNNNNNNNNNNNNNNN", "NNNNNNNNNNNNYNYNNNNNNNNNNNNNNNNN", "NNNNNNNNNNNNNYNYNNNNNNNNNNNNNNNN",
                        "NNNNNNNNNNNNNNYNYNNNNNNNNNNNNNNN", "NNNNNNNNNNNNNNNYNYNNNNNNNNNNNNNN", "NNNNNNNNNNNNNNNNYNYNNNNNNNNNNNNN",
                        "NNNNNNNNNNNNNNNNNYNYNNNNNNNNNNNN", "NNNNNNNNNNNNNNNNNNYNYNNNNNNNNNNN", "NNNNNNNNNNNNNNNNNNNYNYNNNNNNNNNN",
                        "NNNNNNNNNNNNNNNNNNNNYNYNNNNNNNNN", "NNNNNNNNNNNNNNNNNNNNNYNYNNNNNNNN", "NNNNNNNNNNNNNNNNNNNNNNYNYNNNNNNN",
                        "NNNNNNNNNNNNNNNNNNNNNNNYNYNNNNNN", "NNNNNNNNNNNNNNNNNNNNNNNNYNYNNNNN", "NNNNNNNNNNNNNNNNNNNNNNNNNYNYNNNN",
                        "NNNNNNNNNNNNNNNNNNNNNNNNNNYNYNNN", "NNNNNNNNNNNNNNNNNNNNNNNNNNNYNYNN", "NNNNNNNNNNNNNNNNNNNNNNNNNNNNYNYN",
                        "NNNNNNNNNNNNNNNNNNNNNNNNNNNNNYNY", "NNNNNNNNNNNNNNNNNNNNNNNNNNNNNNYN"}, 0));
                  Console.WriteLine(new CandyGame().maximumCandy(new string[] {"NYNNNN", "YNYYNN", "NYNNYN", "NYNNNY", "NNYNNN", "NNNYNN"}, 0));
                  Console.WriteLine(new CandyGame().maximumCandy(new string[] {"NYNNNNN", "YNYYNNN", "NYNNYNY", "NYNNNYN", "NNYNNNN", "NNNYNNN", "NNYNNNN" }, 0));
                  Console.WriteLine(new CandyGame().maximumCandy(new string[] {
                        "NYNNNNYNNNNYNNNNYNNNNN", "YNYNNNNNNNNNNNNNNNNNNN", "NYNYNNNNNNNNNNNNNNNNNN", "NNYNYNNNNNNNNNNNNNNNNN",
                        "NNNYNYNNNNNNNNNNNNNNNN", "NNNNYNNNNNNNNNNNNNNNNN", "YNNNNNNYNNNNNNNNNNNNNN", "NNNNNNYNYNNNNNNNNNNNNN",
                        "NNNNNNNYNYNNNNNNNNNNNN", "NNNNNNNNYNYNNNNNNNNNNN", "NNNNNNNNNYNNNNNNNNNNNN", "YNNNNNNNNNNNYNNNNNNNNN",
                        "NNNNNNNNNNNYNYNNNNNNNN", "NNNNNNNNNNNNYNYNNNNNNN", "NNNNNNNNNNNNNYNYNNNNNN", "NNNNNNNNNNNNNNYNNNNNNN",
                        "YNNNNNNNNNNNNNNNNYNNNN", "NNNNNNNNNNNNNNNNYNYNNN", "NNNNNNNNNNNNNNNNNYNYNN", "NNNNNNNNNNNNNNNNNNYNYN",
                        "NNNNNNNNNNNNNNNNNNNYNY", "NNNNNNNNNNNNNNNNNNNNYN"}, 12));

                  Console.WriteLine("Press any key to continue...");
                  Console.ReadKey();
            }
      }
}