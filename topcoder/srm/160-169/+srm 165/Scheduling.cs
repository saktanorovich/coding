using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TopCoder.Algorithm {
      public class Scheduling {
            public int fastest(string[] dag) {
                  for (int i = 0; i < dag.Length; ++i) {
                        graph[i] = new List<int>();
                  }
                  for (int i = 0; i < dag.Length; ++i) {
                        int[] items = Array.ConvertAll(dag[i].Split(new char[] { ':', ',' },
                              StringSplitOptions.RemoveEmptyEntries),
                                    delegate (string s) {
                                          return int.Parse(s);
                                    });
                        time[i] = items[0];
                        for (int j = 1; j < items.Length; ++j) {
                              graph[items[j]].Add(i);
                        }
                  }
                  return fastest((1 << dag.Length) - 1, 0, 0);
            }

            private readonly int[,,] memo = new int[1 << 12, 12 + 1, 10];
            private readonly List<int>[] graph = new List<int>[12];
            private readonly int[] time = new int[12];

            private int fastest(int set, int last, int delay) {
                  if (set + last == 0) {
                        return 0;
                  }
                  else {
                        if (memo[set, last, delay] == 0) {
                              int result = int.MaxValue;
                              int[] indegree = new int[graph.Length];
                              for (int i = 0; i < graph.Length; ++i) {
                                    if (contains(set, i)) {
                                          foreach (int element in graph[i]) {
                                                ++indegree[element];
                                          }
                                    }
                              }
                              if (last > 0) {
                                    result = delay + fastest(exclude(set, last - 1), 0, 0);
                                    for (int xtask = last - 1, ytask = 0; ytask < graph.Length; ++ytask) {
                                          if (contains(set, ytask) && indegree[ytask] == 0) {
                                                if (xtask != ytask) {
                                                      result = Math.Min(result, fastest(set, xtask, ytask, delay, time[ytask]));
                                                }
                                          }
                                    }
                              }
                              else {
                                    for (int xtask = 0; xtask < graph.Length; ++xtask) {
                                          if (contains(set, xtask) && indegree[xtask] == 0) {
                                                for (int ytask = 0; ytask < graph.Length; ++ytask) {
                                                      if (contains(set, ytask) && indegree[ytask] == 0) {
                                                            result = Math.Min(result, fastest(set, xtask, ytask, time[xtask], time[ytask]));
                                                      }
                                                }
                                          }
                                    }
                              }
                              memo[set, last, delay] = result;
                        }
                        return memo[set, last, delay];
                  }
            }

            private int fastest(int set, int xtask, int ytask, int xtime, int ytime) {
                  if (xtime != ytime) {
                        if (ytime < xtime) {
                              swap(ref xtask, ref ytask);
                              swap(ref xtime, ref ytime);
                        }
                        return xtime + fastest(exclude(set, xtask), ytask + 1, ytime - xtime);
                  }
                  return xtime + fastest(exclude(exclude(set, xtask), ytask), 0, 0);
            }

            private void swap(ref int a, ref int b) {
                  int temp = a; a = b; b = temp;
            }

            public static bool contains(int set, int x) {
                  return (set & (1 << x)) == 1 << x;
            }

            public static int exclude(int set, int x) {
                  if (contains(set, x)) {
                        return set ^ (1 << x);
                  }
                  return set;
            }
      }
}