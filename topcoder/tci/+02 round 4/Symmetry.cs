using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class Symmetry {
            public int countLines(string[] p) {
                  List<Point> points = new List<Point>();
                  foreach (string item in p) {
                        string[] xy = item.Split(new char[] { ' ' });
                        for (int i = 0; i < xy.Length; i += 2) {
                              points.Add(new Point(2 * int.Parse(xy[i]), 2 * int.Parse(xy[i + 1])));
                        }
                  }
                  return countLines(points.ToArray(), points.Count);
            }

            private int countLines(Point[] points, int numOfPoints) {
                  bool collinear = true;
                  Line line = new Line(points[0], points[1]);
                  foreach (Point p in points) {
                        if (!line.Contains(p)) {
                              collinear = false;
                              break;
                        }
                  }
                  if (collinear) {
                        long maxx = points[0].X, minx = points[0].X;
                        long maxy = points[0].Y, miny = points[0].Y;
                        foreach (Point p in points) {
                              maxx = Math.Max(maxx, p.X);
                              maxy = Math.Max(maxy, p.Y);
                              minx = Math.Min(minx, p.X);
                              miny = Math.Min(miny, p.Y);
                        }
                        int[] hit = Array.ConvertAll(new int[numOfPoints],
                              delegate(int x) {
                                    return -1;
                        });
                        for (int i = 0; i < numOfPoints; ++i) {
                              if (hit[i] == -1) {
                                    for (int j = i; j < numOfPoints; ++j) {
                                          if (points[i].X + points[j].X == minx + maxx &&
                                                points[i].Y + points[j].Y == miny + maxy) {
                                                      hit[i] = j;
                                                      hit[j] = i;
                                                      break;
                                          }
                                    }
                              }
                              if (hit[i] == -1) return 1;
                        }
                        return 2;
                  }
                  else {
                        int result = 0;
                        bool[,] analyzed = new bool[numOfPoints, numOfPoints];
                        for (int a = 0; a < numOfPoints; ++a) {
                              for (int b = a + 1; b < numOfPoints; ++b) {
                                    if (!analyzed[a, b]) {
                                          int[] hit = Array.ConvertAll(new int[numOfPoints],
                                                delegate(int x) {
                                                      return -1;
                                          });
                                          hit[a] = b;
                                          hit[b] = a;
                                          Line abline = new Line(points[a], points[b]);
                                          for (int p = 0; p < numOfPoints; ++p) {
                                                if (hit[p] == -1) {
                                                      if (abline.ContainsOnNormal(points[p])) {
                                                            hit[p] = p;
                                                            continue;
                                                      }
                                                      for (int q = 0; q < numOfPoints; ++q) {
                                                            if (hit[q] == -1 && q != p) {
                                                                  Line pqline = new Line(points[p], points[q]);
                                                                  if (abline.ContainsOnNormal(pqline.Normal) && abline.IsParallel(pqline)) {
                                                                        hit[p] = q;
                                                                        hit[q] = p;
                                                                        break;
                                                                  }
                                                            }
                                                      }
                                                }
                                                if (hit[p] == -1) goto next;
                                          }
                                          result = result + 1;
                                          for (int p = 0; p < numOfPoints; ++p) {
                                                analyzed[p, hit[p]] = true;
                                          }
                                    next: ;
                                    }
                              }
                        }
                        return result;
                  }
            }

            private class Line {
                  public long A, B, C;
                  public Point Normal;

                  public Line(Point a, Point b) {
                        A = a.Y - b.Y;
                        B = b.X - a.X;
                        C = a.X * b.Y - a.Y * b.X;
                        Normal = new Point((a.X + b.X) / 2, (a.Y + b.Y) / 2);
                  }

                  public bool Contains(Point p) {
                        return (A * p.X + B * p.Y + C == 0);
                  }

                  public bool ContainsOnNormal(Point p) {
                        long a = -B;
                        long b = +A;
                        long c = -a * Normal.X - b * Normal.Y;
                        return (a * p.X + b * p.Y + c == 0);
                  }

                  public bool IsParallel(Line l) {
                        return A * l.B == B * l.A;
                  }
            }

            private class Point {
                  public long X;
                  public long Y;

                  public Point(long x, long y) {
                        X = x;
                        Y = y;
                  }
            }
      }
}