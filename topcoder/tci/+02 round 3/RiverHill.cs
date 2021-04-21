using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithms {
      public class RiverHill {
            public int largest(string[] map, int maxDist) {
                  return largest(Array.ConvertAll(map, delegate(string s) {
                        return Array.ConvertAll(s.ToCharArray(), delegate(char c) {
                              return int.Parse(c.ToString());
                        });
                  }), map.Length, map[0].Length, maxDist);
            }

            private static readonly int[] dx = new int[4] { -1, 0, +1, 0 };
            private static readonly int[] dy = new int[4] { 0, -1, 0, +1 };

            private int largest(int[][] map, int n, int m, int maxDist) {
                  int result = 0;
                  for (int i = 0; i < n; ++i) {
                        for (int j = 0; j < m; ++j) {
                              result = Math.Max(result, largest(map, n, m, i, j, maxDist));
                        }
                  }
                  return result;
            }

            private int largest(int[][] map, int n, int m, int originx, int originy, int maxDist) {
                  int[,] covered = new int[n, m];
                  for (int i = 0; i < n; ++i) {
                        for (int j = 0; j < m; ++j) {
                              covered[i, j] = -1;
                        }
                  }
                  Queue<int> queue = new Queue<int>();
                  for (covered[originx, originy] = maxDist, queue.Enqueue(originx * m + originy); queue.Count > 0; ) {
                        int element = queue.Dequeue();
                        int currx = element / m;
                        int curry = element % m;
                        for (int k = 0; k < 4; ++k) {
                              int nextx = currx + dx[k];
                              int nexty = curry + dy[k];
                              if (0 <= nextx && nextx < n && 0 <= nexty && nexty < m) {
                                    if (map[currx][curry] > map[nextx][nexty]) {
                                          covered[nextx, nexty] = maxDist;
                                          queue.Enqueue(nextx * m + nexty);
                                    }
                                    else if (map[currx][curry] == map[nextx][nexty]) {
                                          int remains = covered[currx, curry] - 1;
                                          if (remains >= 0 && remains > covered[nextx, nexty]) {
                                                covered[nextx, nexty] = remains;
                                                queue.Enqueue(nextx * m + nexty);
                                          }
                                    }
                              }
                        }
                  }
                  int result = 0;
                  for (int i = 0; i < n; ++i) {
                        for (int j = 0; j < m; ++j) {
                              if (covered[i, j] >= 0) {
                                    result = result + 1;
                              }
                        }
                  }
                  return result;
            }

            private static string ToString<T>(T[] a) {
                  string result = string.Empty;
                  for (int i = 0; i < a.Length; ++i) {
                        result += a[i].ToString();
                        if (i + 1 < a.Length) {
                              result += ' ';
                        }
                  }
                  return result + Environment.NewLine;
            }

            public static void Main(string[] args) {
                  Console.WriteLine(new RiverHill().largest(new string[] {
 "0000000000"
,"0111111110"
,"0122222210"
,"0123333210"
,"0123443210"
,"0123443210"
,"0123333210"
,"0122222210"
,"0111111110"
,"0000000000"}, 2));

                  Console.WriteLine("Press any key to continue...");
                  Console.ReadLine();
            }
      }
}