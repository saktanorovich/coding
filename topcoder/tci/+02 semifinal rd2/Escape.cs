using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class Escape {
            public int lowest(string[] harmful, string[] deadly) {
                  return lowest(parse(harmful, +1), parse(deadly, -1), 500, 500);
            }

            private static readonly int[] dx = new int[4] { -1, 0, +1, 0 };
            private static readonly int[] dy = new int[4] { 0, -1, 0, +1 };

            private int lowest(Region[] harmful,Region[] deadly, int n, int m) {
                  int[,] f = new int[n + 1, m + 1];
                  foreach (Region region in harmful) {
                        region.Mark(f);
                  }
                  foreach (Region region in deadly) {
                        region.Mark(f);
                  }
                  int[,] d = new int[n + 1, m + 1];
                  for (int i = 0; i <= n; ++i) {
                        for (int j = 0; j <= m; ++j) {
                              d[i, j] = int.MaxValue;
                        }
                  }
                  Queue<int> queue = new Queue<int>();
                  for (d[0, 0] = 0, queue.Enqueue(0); queue.Count > 0; ) {
                        int element = queue.Dequeue();
                        int currx = element / (m + 1);
                        int curry = element % (m + 1);
                        for (int k = 0; k < 4; ++k) {
                              int nextx = currx + dx[k];
                              int nexty = curry + dy[k];
                              if (0 <= nextx && nextx <= n && 0 <= nexty && nexty <= m) {
                                    if (f[nextx, nexty] >= 0) {
                                          if (d[nextx, nexty] > d[currx, curry] + f[nextx, nexty]) {
                                                d[nextx, nexty] = d[currx, curry] + f[nextx, nexty];
                                                queue.Enqueue(nextx * (m + 1) + nexty);
                                          }
                                    }
                              }
                        }
                  }
                  return (d[n, m] < int.MaxValue ? d[n, m] : -1);
            }

            private Region[] parse(string[] regions, int def) {
                  return Array.ConvertAll(regions, delegate(string s) {
                        Region result = Region.Parse(s);
                        result.def = def;
                        return result;
                  });
            }

            private class Region {
                  public int xmin, ymin;
                  public int xmax, ymax;
                  public int def;

                  public Region(int x1, int y1, int x2, int y2) {
                        this.xmin = Math.Min(x1, x2);
                        this.ymin = Math.Min(y1, y2);
                        this.xmax = Math.Max(x1, x2);
                        this.ymax = Math.Max(y1, y2);
                  }

                  public void Mark(int[,] f) {
                        for (int x = xmin; x <= xmax; ++x) {
                              for (int y = ymin; y <= ymax; ++y) {
                                    f[x, y] = def;
                              }
                        }
                  }

                  public static Region Parse(string s) {
                        string[] items = s.Split(new char[] { ' ' });
                        int x1 = int.Parse(items[0]);
                        int y1 = int.Parse(items[1]);
                        int x2 = int.Parse(items[2]);
                        int y2 = int.Parse(items[3]);
                        return new Region(x1, y1, x2, y2);
                  }
            }
      }
}