using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TopCoder.Algorithm {
      public class Quilting {
            public string lastPatch(int lengt, int width, string[] colorList) {
                  string[,] quilt = new string[lengt, width];
                  Dictionary<string, int> occurence = new Dictionary<string, int>();
                  foreach (string color in colorList) {
                        occurence[color] = 0;
                  }
                  int x = lengt / 2;
                  int y = width / 2;
                  for (int step = 0, d = 0; step < lengt * width; ++step) {
                        quilt[x, y] = getColor(quilt, lengt, width, colorList, occurence, x, y);
                        ++occurence[quilt[x, y]];
                        if (step + 1 < lengt * width) {
                              x = x + dx[d];
                              y = y + dy[d];
                              int r = (d + 1) % 4;
                              if (0 <= x + dx[r] && x + dx[r] < lengt) {
                                    if (0 <= y + dy[r] && y + dy[r] < width) {
                                          if (quilt[x + dx[r], y + dy[r]] == null) {
                                                d = r;
                                          }
                                    }
                              }
                        }
                  }
                  return quilt[x, y];
            }

            private string getColor(string[,] quilt, int lengt, int width, string[] colorList, Dictionary<string, int> occurence, int xpos, int ypos) {
                  Dictionary<string, int> cumul = new Dictionary<string, int>();
                  foreach (string color in colorList) {
                        cumul[color] = 0;
                  }
                  for (int x = -1; x <= +1; ++x) {
                        for (int y = -1; y <= +1; ++y) {
                              if (Math.Abs(x) + Math.Abs(y) != 0) {
                                    if (0 <= xpos + x && xpos + x < lengt) {
                                          if (0 <= ypos + y && ypos + y < width) {
                                                string color = quilt[xpos + x, ypos + y];
                                                if (color != null) {
                                                      ++cumul[color];
                                                }
                                          }
                                    }
                              }
                        }
                  }
                  if (getCnt(cumul, getMin(cumul)) > 1) {
                        Dictionary<string, int> neighbours = new Dictionary<string, int>();
                        foreach (string color in colorList) {
                              if (cumul[color] == getMin(cumul)) {
                                    neighbours[color] = occurence[color];
                              }
                        }
                        if (getCnt(neighbours, getMin(neighbours)) > 1) {
                              foreach (string color in colorList) {
                                    if (neighbours.ContainsKey(color)) {
                                          if (neighbours[color] == getMin(neighbours)) {
                                                return color;
                                          }
                                    }
                              }
                        }
                        return getAny(neighbours, getMin(neighbours));
                  }
                  return getAny(cumul, getMin(cumul));
            }

            private int getMin(Dictionary<string, int> lookup) {
                  int result = int.MaxValue;
                  foreach (KeyValuePair<string, int> entry in lookup) {
                        if (result > entry.Value) {
                              result = entry.Value;
                        }
                  }
                  return result;
            }

            private int getCnt(Dictionary<string, int> lookup, int value) {
                  int result = 0;
                  foreach (KeyValuePair<string, int> entry in lookup) {
                        if (entry.Value == value) {
                              result = result + 1;
                        }
                  }
                  return result;
            }

            private string getAny(Dictionary<string, int> lookup, int value) {
                  foreach (KeyValuePair<string, int> entry in lookup) {
                        if (entry.Value == value) {
                              return entry.Key;
                        }
                  }
                  return null;
            }

            private readonly int[] dx = new int[4] { -1,  0, +1,  0 };
            private readonly int[] dy = new int[4] {  0, -1,  0, +1 };
      }
}