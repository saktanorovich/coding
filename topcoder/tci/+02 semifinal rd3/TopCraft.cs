using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class TopCraft {
            public int select(string[] select) {
                  List<int> unitx = new List<int>();
                  List<int> unity = new List<int>();
                  for (int i = 0; i < select.Length; ++i) {
                        for (int j = 0; j < select[0].Length; ++j) {
                              if (select[i][j].Equals('1')) {
                                    unitx.Add(i);
                                    unity.Add(j);
                              }
                        }
                  }
                  List<Region> regions = new List<Region>();
                  for (int xmin = 0; xmin < select.Length; ++xmin) {
                        for (int ymin = 0; ymin < select[0].Length; ++ymin) {
                              for (int xmax = xmin; xmax < select.Length; ++xmax) {
                                    for (int ymax = ymin; ymax < select[0].Length; ++ymax) {
                                          for (int xx = xmin; xx <= xmax; ++xx) {
                                                for (int yy = ymin; yy <= ymax; ++yy) {
                                                      if (select[xx][yy].Equals('2')) {
                                                            goto next;
                                                      }
                                                }
                                          }
                                          regions.Add(new Region(xmin, ymin, xmax, ymax));
                                          next:;
                                    }
                              }
                        }
                  }
                  /* note: the regions can be filtered in order to improve performance... */
                  int[] which = new int[regions.Count];
                  for (int r = 0; r < regions.Count; ++r) {
                        for (int u = 0; u < unitx.Count; ++u) {
                              if (regions[r].xmin <= unitx[u] && unitx[u] <= regions[r].xmax &&
                                  regions[r].ymin <= unity[u] && unity[u] <= regions[r].ymax) {
                                        which[r] |= (1 << u);
                              }
                        }
                  }
                  int[] dp = new int[1 << unitx.Count];
                  for (int set = 1; set < 1 << unitx.Count; ++set) {
                        dp[set] = int.MaxValue;
                  }
                  for (int r = 0; r < regions.Count; ++r) {
                        for (int set = 0; set < 1 << unitx.Count; ++set) {
                              if (dp[set] < int.MaxValue) {
                                    dp[set | which[r]] = Math.Min(dp[set | which[r]], dp[set] + 1);
                              }
                        }
                  }
                  return dp[dp.Length - 1];
            }

            private class Region {
                  public int xmin, ymin;
                  public int xmax, ymax;

                  public Region(int xmin, int ymin, int xmax, int ymax) {
                        this.xmin = xmin;
                        this.ymin = ymin;
                        this.xmax = xmax;
                        this.ymax = ymax;
                  }

                  public bool Contains(Region region) {
                        return (xmin <= region.xmin && region.xmax <= xmax && ymin <= region.ymin && region.ymax <= ymax);
                  }
            }
      }
}