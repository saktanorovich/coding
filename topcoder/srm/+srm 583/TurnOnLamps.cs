using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class TurnOnLamps {
            public int minimize(int[] roads, string initState, string isImportant) {
                  int[,] state = new int[roads.Length + 1, roads.Length + 1];
                  for (int a = 0; a < roads.Length + 1; ++a) {
                        for (int b = a + 1; b < roads.Length + 1; ++b) {
                              state[a, b] = -1;
                              state[b, a] = -1;
                        }
                  }
                  for (int i = 0; i < roads.Length; ++i) {
                        if (isImportant[i].Equals('1')) {
                              state[roads[i], i + 1] = initState[i] - '0';
                              state[i + 1, roads[i]] = initState[i] - '0';
                        }
                  }
                  int result = 0;
                  while (hasAnyImportant(roads, state)) {
                        performToggling(roads, state);
                        result = result + 1;
                  }
                  return result;
            }

            private bool hasAnyImportant(int[] roads, int[,] state) {
                  for (int i = 0; i < roads.Length; ++i) {
                        if (state[roads[i], i + 1] == 0) {
                              return true;
                        }
                  }
                  return false;
            }

            private void performToggling(int[] tree, int[,] state) {
                  List<int> bestPath = null;
                  int bestRoadsToToggle = 0;
                  for (int a = 0; a < tree.Length + 1; ++a) {
                        for (int b = a + 1; b < tree.Length + 1; ++b) {
                              List<int> path = new List<int>();
                              if (dfs(a, b, -1, tree, state, path)) {
                                    int roadsToToggle = computeRoadsToToggle(path, state);
                                    if (roadsToToggle > bestRoadsToToggle) {
                                          bestPath = path;
                                          bestRoadsToToggle = roadsToToggle;
                                    }
                              }
                        }
                  }
                  if (bestPath != null) {
                        for (int i = 0; i + 1 < bestPath.Count; ++i) {
                              if (state[bestPath[i], bestPath[i + 1]] == 0) {
                                    state[bestPath[i], bestPath[i + 1]] = 1;
                                    state[bestPath[i + 1], bestPath[i]] = 1;
                              }
                        }
                  }
            }

            private int computeRoadsToToggle(List<int> path, int[,] state) {
                  int result = 0;
                  for (int i = 0; i + 1 < path.Count; ++i) {
                        if (state[path[i], path[i + 1]] == 0) {
                              result = result + 1;
                        }
                  }
                  return result;
            }

            private bool dfs(int curr, int goal, int prev, int[] tree, int[,] state, List<int> path) {
                  if (curr == goal) {
                        path.Add(curr);
                        return true;
                  }
                  foreach (int next in children(curr, tree)) {
                        if (next != prev && state[curr, next] != 1) {
                              if (dfs(next, goal, curr, tree, state, path)) {
                                    path.Add(curr);
                                    return true;
                              }
                        }
                  }
                  return false;
            }

            private IEnumerable<int> children(int node, int[] tree) {
                  List<int> result = new List<int>();
                  for (int i = 0; i < tree.Length; ++i) {
                        if (tree[i] == node) {
                              result.Add(i + 1);
                        }
                        if (i + 1 == node) {
                              result.Add(tree[i]);
                        }
                  }
                  return result;
            }

            internal static void Main(string[] args) {
                  Console.WriteLine(new TurnOnLamps().minimize(new int[] {0,0,1,1}, "0001", "0111")); // Returns: 1
                  Console.WriteLine(new TurnOnLamps().minimize(new int[] {0,0,1,1}, "0000", "0111")); // Returns: 2
                  Console.WriteLine(new TurnOnLamps().minimize(new int[] {0,0,1,1,4,4}, "000100", "111111")); // Returns: 2
                  Console.WriteLine(new TurnOnLamps().minimize(new int[] {0,0,1,1,4,4}, "100100", "011101")); // Returns: 2
                  Console.WriteLine(new TurnOnLamps().minimize(new int[] {0,0,2,2,3,1,6,3,1}, "010001110", "000110100")); // Returns: 1
                  Console.WriteLine(new TurnOnLamps().minimize(
                        new int[] {0,0,1,2,4,4,6,1,2,5,2,8,8,3,6,4,14,7,18,14,11,7,1,12,7,5,18,23,0,14,11,10,2,2,6,1,30,11,9,12,5,35,25,11,23,17,14,45,15},
                        "0000000000010000000000000010000010100000000000000",
                        "1010111111111011011111000110111111111111111110111")); // Returns: 14

                  Console.WriteLine("\nPress any key to continue...");
                  Console.ReadKey();
            }
      }
}
