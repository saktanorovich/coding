using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TopCoder.Algorithm {
      public class GearsDiv1 {
            private readonly string alphabet = "RGB";
            private readonly int undefined = -1;

            public int getmin(string color, string[] graph) {
                  return color.Length - getmax(Array.ConvertAll(color.ToCharArray(), delegate(char value) {
                        return alphabet.IndexOf(value);
                  }), graph);
            }

            private int getmax(int[] color, string[] mesh) {
                  int n = color.Length;
                  int result = 0;
                  for (int r = 0; r < 2; ++r) {
                        for (int g = 0; g < 2; ++g) {
                              for (int b = 0; b < 2; ++b) {
                                    if (r == g && g == b) {
                                          /* do not analyze the case when all gears have the same rotation because
                                           * this is the worst case. */
                                          continue;
                                    }
                                    int[] rot = new int[] { r, g, b };
                                    /* we need to remove a minimum number of gears such that there are no meshing gears
                                     * in the same part. This can be done by Kuhn algorithm because only two types
                                     * of gears can have the same color. */
                                    bool[,] bipartite = new bool[n, n];
                                    for (int i = 0; i < n; ++i) {
                                          for (int j = i + 1; j < n; ++j) {
                                                if (rot[color[i]] == rot[color[j]]) {
                                                      if (mesh[i][j].Equals('Y')) {
                                                            bipartite[i, j] = true;
                                                            bipartite[j, i] = true;
                                                      }
                                                }
                                          }
                                    }
                                    result = Math.Max(result, getMaxIndependentSet(bipartite, n));
                              }
                        }
                  }
                  return result;
            }

            private int getMaxIndependentSet(bool[,] bipartite, int n) {
                  return n - kuhn(bipartite, n);
            }

            private int kuhn(bool[,] bipartite, int n) {
                  int cardinality = 0;
                  int[] matching = Array.ConvertAll(new int[n],
                        delegate(int x) {
                              return undefined;
                        }
                  );
                  while (augment(bipartite, n, matching)) {
                        cardinality = cardinality + 1;
                  }
                  return cardinality;
            }

            private bool augment(bool[,] bipartite, int n, int[] matching) {
                  bool[] visited = new bool[n];
                  for (int curr = 0; curr < n; ++curr) {
                        if (matching[curr] == undefined && !visited[curr]) {
                              if (dfs(bipartite, n, curr, matching, visited)) {
                                    return true;
                              }
                        }
                  }
                  return false;
            }

            private bool dfs(bool[,] bipartite, int n, int curr, int[] matching, bool[] visited) {
                  if (visited[curr]) {
                        return false;
                  }
                  visited[curr] = true;
                  for (int next = 0; next < n; ++next) {
                        if (bipartite[curr, next] && !visited[next]) {
                              if (matching[next] == undefined || dfs(bipartite, n, matching[next], matching, visited)) {
                                    matching[curr] = next;
                                    matching[next] = curr;
                                    return true;
                              }
                        }
                  }
                  return false;
            }

            internal static void Main(string[] args) {
                  Console.WriteLine(new GearsDiv1().getmin("RGB", new string[] {
                        "NYY",
                        "YNY",
                        "YYN" })); // Returns: 1
                  Console.WriteLine(new GearsDiv1().getmin("RGBR", new string[] {
                        "NNNN",
                        "NNNN",
                        "NNNN",
                        "NNNN"})); // Returns: 0
                  Console.WriteLine(new GearsDiv1().getmin("RGBR", new string[] {
                        "NYNN",
                        "YNYN",
                        "NYNY",
                        "NNYN"})); // Returns: 1
                  Console.WriteLine(new GearsDiv1().getmin("RRRRRGRRBGRRGBBGGGBRRRGBRGRRGG", new string[] {
 "NNNNNYNNNYNNYNNNYNNNNNNNNYNNYY",
 "NNNNNNNNYNNNYNYNNYNNNNYNNYNNYY",
 "NNNNNYNNNNNNNNNNNNYNNNNNNYNNNY",
 "NNNNNNNNNYNNYNNYYYNNNNYNNYNNNN",
 "NNNNNNNNNYNNYNNYYYNNNNYNNNNNNN",
 "YNYNNNYYYNNYNYYNNNNNYYNYNNYYNN",
 "NNNNNYNNNNNNNNNYYYNNNNYNNYNNYY",
 "NNNNNYNNNNNNNNNYNNNNNNNNNNNNYN",
 "NYNNNYNNNYNNYNNYYYNNNNYNNYNNYY",
 "YNNYYNNNYNNNNYYNNNYNYYNYNNNNNN",
 "NNNNNNNNNNNNYNNYNYNNNNYNNNNNNY",
 "NNNNNYNNNNNNYNNYYYNNNNNNNNNNYN",
 "YYNYYNNNYNYYNYYNNNYNYNNYNNNNNN",
 "NNNNNYNNNYNNYNNYYYNNNNYNNYNYYY",
 "NYNNNYNNNYNNYNNYYYNNNNYNNYNNYY",
 "NNNYYNYYYNYYNYYNNNYNYNNYYNYYNN",
 "YNNYYNYNYNNYNYYNNNYNNNNYYNNYNN",
 "NYNYYNYNYNYYNYYNNNNYYNNYYNYNNN",
 "NNYNNNNNNYNNYNNYYNNNNNYNNYNNNY",
 "NNNNNNNNNNNNNNNNNYNNNNYNNYNNNY",
 "NNNNNYNNNYNNYNNYNYNNNNYNNNNNYY",
 "NNNNNYNNNYNNNNNNNNNNNNYNNNNNNN",
 "NYNYYNYNYNYNNYYNNNYYYYNYYNYNNN",
 "NNNNNYNNNYNNYNNYYYNNNNYNNNNNNY",
 "NNNNNNNNNNNNNNNYYYNNNNYNNYNNYY",
 "YYYYNNYNYNNNNYYNNNYYNNNNYNYYNN",
 "NNNNNYNNNNNNNNNYNYNNNNYNNYNNYN",
 "NNNNNYNNNNNNNYNYYNNNNNNNNYNNYY",
 "YYNNNNYYYNNYNYYNNNNNYNNNYNYYNN",
 "YYYNNNYNYNYNNYYNNNYYYNNYYNNYNN"})); // Returns: 3

                  Console.WriteLine("\nPress any key to continue...");
                  Console.ReadKey();
            }
      }
}
