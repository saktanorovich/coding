using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithms {
      public class Socialize {
            public int average(string[] layout) {
                  int numOfPeople = 0, n = layout.Length, m = layout[0].Length;
                  List<int> manx = new List<int>();
                  List<int> many = new List<int>();
                  for (int i = 0; i < n; ++i) {
                        for (int j = 0; j < m; ++j) {
                              if (layout[i][j].Equals('P')) {
                                    manx.Add(i);
                                    many.Add(j);
                                    numOfPeople = numOfPeople + 1;
                              }
                        }
                  }
                  int sumOfPaths = 0, numOfPaths = 0;
                  for (int a = 0; a < numOfPeople; ++a) {
                        int[,] dist = new int[n, m];
                        for (int i = 0; i < n; ++i) {
                              for (int j = 0; j < m; ++j) {
                                    dist[i, j] = int.MaxValue;
                              }
                        }
                        Queue<int> queue = new Queue<int>();
                        for (dist[manx[a], many[a]] = 0, queue.Enqueue(manx[a] * m + many[a]); queue.Count > 0; ) {
                              int man = queue.Dequeue();
                              int currx = man / m;
                              int curry = man % m;
                              for (int k = 0; k < 4; ++k) {
                                    int nextx = currx + dx[k];
                                    int nexty = curry + dy[k];
                                    if (0 <= nextx && nextx < n && 0 <= nexty && nexty < m) {
                                          if (layout[nextx][nexty] != '#') {
                                                if (dist[nextx, nexty] > dist[currx, curry] + 1) {
                                                      dist[nextx, nexty] = dist[currx, curry] + 1;
                                                      queue.Enqueue(nextx * m + nexty);
                                                }
                                          }
                                    }
                              }
                        }
                        for (int b = a + 1; b < numOfPeople; ++b) {
                              if (dist[manx[b], many[b]] < int.MaxValue) {
                                    sumOfPaths += dist[manx[b], many[b]];
                                    numOfPaths += 1;
                              }
                        }
                  }
                  if (numOfPaths > 0) {
                        return (int)(1.0 * sumOfPaths / numOfPaths + 0.5);
                  }
                  return 0;
            }

            private static readonly int[] dx = new int[4] { -1, 0, +1, 0 };
            private static readonly int[] dy = new int[4] { 0, -1, 0, +1 };

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
                  Console.WriteLine(new Socialize().average(new string[]{
"P...P",
"###..",
"P...#",
"####P"}));

                  Console.WriteLine("Press any key to continue...");
                  Console.ReadLine();
            }
      }
}