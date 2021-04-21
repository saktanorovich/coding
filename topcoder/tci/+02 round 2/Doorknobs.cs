using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class Doorknobs {
            public int shortest(string[] house, int doorknobs) {
                  return shortest(Array.ConvertAll(house, delegate(string s) {
                        int[] result = new int[s.Length];
                        for (int i = 0; i < s.Length; ++i) {
                              switch (s[i]) {
                                    case '#': result[i] = -1; break;
                                    case 'o': result[i] = +1; break;
                              }
                        }
                        return result;
                  }), house.Length, house[0].Length, doorknobs);
            }

            private static readonly int[] dx = new int[4] { -1,  0, +1,  0 };
            private static readonly int[] dy = new int[4] {  0, -1,  0, +1 };

            private int shortest(int[][] house, int n, int m, int doorknobs) {
                  int numOfDoorknobs = 0;
                  for (int i = 0; i < n; ++i) {
                        for (int j = 0; j < m; ++j) {
                              if (house[i][j] > 0) {
                                    numOfDoorknobs = numOfDoorknobs + 1;
                                    house[i][j] = numOfDoorknobs;
                              }
                        }
                  }
                  int[,,] dp = new int[n, m, 1 << numOfDoorknobs];
                  for (int i = 0; i < n; ++i) {
                        for (int j = 0; j < m; ++j) {
                              for (int set = 0; set < 1 << numOfDoorknobs; ++set) {
                                    dp[i, j, set] = int.MaxValue;
                              }
                        }
                  }
                  dp[0, 0, makeSet(house[0][0])] = 0;
                  Queue<QueueEntry> queue = new Queue<QueueEntry>();
                  for (queue.Enqueue(new QueueEntry(0, 0, makeSet(house[0][0]))); queue.Count > 0; ) {
                        QueueEntry curr = queue.Dequeue();
                        for (int k = 0; k < 4; ++k) {
                              int x = curr.x + dx[k];
                              int y = curr.y + dy[k];
                              if (0 <= x && x < n && 0 <= y && y < m) {
                                    if (house[x][y] >= 0) {
                                          int set = curr.set | makeSet(house[x][y]);
                                          if (dp[x, y, set] > dp[curr.x, curr.y, curr.set] + 1) {
                                                dp[x, y, set] = dp[curr.x, curr.y, curr.set] + 1;
                                                queue.Enqueue(new QueueEntry(x, y, set));
                                          }
                                    }
                              }
                        }
                  }
                  int result = int.MaxValue;
                  for (int i = 0; i < n; ++i) {
                        for (int j = 0; j < m; ++j) {
                              for (int set = 0; set < 1 << numOfDoorknobs; ++set) {
                                    if (cardinality(set) == doorknobs) {
                                          result = Math.Min(result, dp[i, j, set]);
                                    }
                              }
                        }
                  }
                  return (result < int.MaxValue ? result : -1);
            }

            private int cardinality(int set) {
                  if (set > 0) {
                        return 1 + cardinality(set & (set - 1));
                  }
                  return 0;
            }

            private int makeSet(int x) {
                  if (x > 0) {
                        return 1 << (x - 1);
                  }
                  return 0;
            }

            private class QueueEntry {
                  public int x, y, set;

                  public QueueEntry(int x, int y, int set) {
                        this.x = x;
                        this.y = y;
                        this.set = set;
                  }
            }
      }
}