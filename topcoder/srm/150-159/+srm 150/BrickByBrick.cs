using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class BrickByBrick {
            public int timeToClear(string[] map) {
                  return timeToClear(extend(map, map.Length, map[0].Length), map.Length, map[0].Length);
            }

            private int timeToClear(int[][] map, int n, int m) {
                  int numOfBricks = 0;
                  for (int i = 1; i <= n; ++i) {
                        for (int j = 1; j <= m; ++j) {
                              if (map[i][j] == 1) {
                                    numOfBricks = numOfBricks + 1;
                              }
                        }
                  }
                  int time = 0;
                  int x = 1, dx = 1;
                  int y = 1, dy = 1;
                  for (int idle = 0; idle < 2 * n * m; ++idle) {
                        switch (map[x][y + dy]) {
                              case 0:
                                    y += dy;
                                    break;
                              case 1:
                                    map[x][y + dy] = 0;
                                    numOfBricks = numOfBricks - 1;
                                    dy = -dy;
                                    idle = 0;
                                    break;
                              case 2:
                                    dy = -dy;
                                    break;
                        }
                        time = time + 1;
                        if (numOfBricks == 0) {
                              break;
                        }
                        switch (map[x + dx][y]) {
                              case 0:
                                    x += dx;
                                    break;
                              case 1:
                                    map[x + dx][y] = 0;
                                    numOfBricks = numOfBricks - 1;
                                    dx = -dx;
                                    idle = 0;
                                    break;
                              case 2:
                                    dx = -dx;
                                    break;
                        }
                        time = time + 1;
                        if (numOfBricks == 0) {
                              break;
                        }
                  }
                  return (numOfBricks > 0 ? -1 : time);
            }

            private int[][] extend(string[] map, int n, int m) {
                  string[] res = new string[n + 2];
                  res[1 - 1] = string.Empty.PadLeft(m + 2, '#');
                  res[n + 1] = string.Empty.PadLeft(m + 2, '#');
                  for (int i = 1; i <= n; ++i) {
                        res[i] = string.Format("#{0}#", map[i - 1]);
                  }
                  return Array.ConvertAll(res, delegate(string s) {
                        return Array.ConvertAll(s.ToCharArray(), delegate(char c) {
                              return ".B#".IndexOf(c);
                        });
                  });
            }
      }
}