using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class Posters {
            public int maxCover(int width, int height, int[] pWidth, int[] pHeight) {
                  Rectangle[] posters = new Rectangle[pWidth.Length];
                  for (int i = 0; i < pWidth.Length; ++i) {
                        posters[i] = new Rectangle(pWidth[i], pHeight[i]);
                  }
                  return maxCover(new Rectangle(width, height), posters, 0, 0);
            }

            private int maxCover(Rectangle wall, Rectangle[] posters, int set, int cardinality) {
                  if (cardinality < posters.Length) {
                        List<int> xx = new List<int>();
                        List<int> yy = new List<int>();
                        add(xx, +0);
                        add(yy, +0);
                        add(xx, -wall.height);
                        add(yy, -wall.widthh);
                        for (int i = 0; i < posters.Length; ++i) {
                              if ((set & (1 << i)) != 0) {
                                    add(xx, -posters[i].x);
                                    add(yy, -posters[i].y);
                                    add(xx, +posters[i].x + posters[i].height);
                                    add(yy, +posters[i].y + posters[i].widthh);
                              }
                        }
                        int result = 0;
                        for (int i = 0; i < posters.Length; ++i) {
                              if ((set & (1 << i)) == 0) {
                                    if (cardinality > 1) {
                                          foreach (int x in xx) {
                                                foreach (int y in yy) {
                                                      int px = x;
                                                      if (px < 0) {
                                                            px = -px - posters[i].height;
                                                      }
                                                      int py = y;
                                                      if (py < 0) {
                                                            py = -py - posters[i].widthh;
                                                      }
                                                      if (0 <= px && px + posters[i].height <= wall.height) {
                                                            if (0 <= py && py + posters[i].widthh <= wall.widthh) {
                                                                  posters[i].x = px;
                                                                  posters[i].y = py;
                                                                  result = Math.Max(result, maxCover(wall, posters, set | (1 << i), cardinality + 1));
                                                            }
                                                      }
                                                }
                                          }
                                    }
                                    else {
                                          posters[i].x = cardinality * (wall.height - posters[i].height);
                                          posters[i].y = cardinality * (wall.widthh - posters[i].widthh);
                                          result = Math.Max(result, maxCover(wall, posters, set | (1 << i), cardinality + 1));
                                    }
                              }
                        }
                        return result;
                  }
                  return getCover(posters);
            }
            private int getCover(Rectangle[] posters) {
                  XEvent[] xevents = new XEvent[2 * posters.Length];
                  YEvent[] yevents = new YEvent[2 * posters.Length];
                  for (int i = 0; i < posters.Length; ++i) {
                        xevents[2 * i + 0] = new XEvent(+1, posters[i].x);
                        xevents[2 * i + 1] = new XEvent(-1, posters[i].x + posters[i].height);

                        yevents[2 * i + 0] = new YEvent(+1, posters[i].x, posters[i].x + posters[i].height, posters[i].y);
                        yevents[2 * i + 1] = new YEvent(-1, posters[i].x, posters[i].x + posters[i].height, posters[i].y + posters[i].widthh);
                  }
                  Array.Sort(xevents);
                  Array.Sort(yevents);

                  int result = 0, xcnt = 0;
                  for (int i = 0; i < 2 * posters.Length; ++i) {
                        if (xcnt > 0) {
                              int lasty = 0, ycnt = 0;
                              foreach (YEvent yevent in yevents) {
                                    if (yevent.x0 < xevents[i].xcoord && xevents[i].xcoord <= yevent.x1) {
                                          if (ycnt == 0) {
                                                lasty = yevent.ycoord;
                                          }
                                          ycnt += yevent.evtype;
                                          if (ycnt == 0) {
                                                result += (xevents[i].xcoord - xevents[i - 1].xcoord) * (yevent.ycoord - lasty);
                                          }
                                    }
                              }
                        }
                        xcnt += xevents[i].evtype;
                  }
                  return result;
            }

            private void add(List<int> list, int x) {
                  if (!list.Contains(x)) {
                        list.Add(x);
                  }
            }

            private class XEvent : IComparable<XEvent> {
                  public int evtype;
                  public int xcoord;

                  public XEvent(int evtype, int xcoord) {
                        this.evtype = evtype;
                        this.xcoord = xcoord;
                  }

                  public int CompareTo(XEvent other) {
                        if (this.xcoord != other.xcoord) {
                              return this.xcoord.CompareTo(other.xcoord);
                        }
                        return this.evtype.CompareTo(other.evtype);
                  }
            }

            private class YEvent : IComparable<YEvent> {
                  public int evtype;
                  public int ycoord;
                  public int x0, x1;

                  public YEvent(int evtype, int x0, int x1, int ycoord) {
                        this.evtype = evtype;
                        this.ycoord = ycoord;
                        this.x0 = x0;
                        this.x1 = x1;
                  }

                  public int CompareTo(YEvent other) {
                        if (this.ycoord != other.ycoord) {
                              return this.ycoord.CompareTo(other.ycoord);
                        }
                        return this.evtype.CompareTo(other.evtype);
                  }
            }

            private class Rectangle {
                  public int x;
                  public int y;
                  public int widthh;
                  public int height;

                  public Rectangle(int widthh, int height) {
                        this.x = 0;
                        this.y = 0;
                        this.widthh = widthh;
                        this.height = height;
                  }
            }
      }
}