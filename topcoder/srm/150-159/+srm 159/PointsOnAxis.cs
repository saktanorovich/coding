using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TopCoder.Algorithm {
      public class PointsOnAxis {
            public int[] findPoints(int[] distances) {
                  Array.Sort(distances);
                  this.distances = distances;
                  this.points = new List<int>();
                  while (points.Count * (points.Count - 1) < 2 * distances.Length) {
                        points.Add(0);
                  }
                  foreach (int distance in distances) {
                        ++cumul[distance];
                  }
                  if (findPoints(0, points.Count, distances.Length - 1)) {
                        return points.ToArray();
                  }
                  return new int[0];
            }

            private int[] cumul = new int[1000000 + 1];
            private int[] distances;
            private List<int> points;

            private bool findPoints(int lo, int hi, int dix) {
                  if (lo + 1 < hi) {
                        if (cumul[distances[dix]] == 0) {
                              return findPoints(lo, hi, dix - 1);
                        }
                        else {
                              for (int i = hi; i < points.Count; ++i) {
                                    points[lo + 1] = points[i] - distances[dix];
                                    if (verifyPoints(lo + 1, lo, hi, dix - 1)) {
                                          return true;
                                    }
                              }
                              for (int i = 0; i <= lo; ++i) {
                                    points[hi - 1] = points[i] + distances[dix];
                                    if (verifyPoints(hi - 1, lo, hi, dix - 1)) {
                                          return true;
                                    }
                              }
                        }
                        return false;
                  }
                  else {
                        foreach (int distance in distances) {
                              if (cumul[distance] != 0) {
                                    return false;
                              }
                        }
                        return true;
                  }
            }

            private bool verifyPoints(int pix, int lo, int hi, int dix) {
                  if (dec(pix, 0, lo)) {
                        if (dec(pix, hi, points.Count - 1)) {
                              if (lo + 1 == pix) {
                                    lo = pix;
                              }
                              if (hi - 1 == pix) {
                                    hi = pix;
                              }
                              if (findPoints(lo, hi, dix)) {
                                    return true;
                              }
                              inc(pix, hi, points.Count - 1);
                        }
                        inc(pix, 0, lo);
                  }
                  return false;
            }

            private bool dec(int pix, int lo, int hi) {
                  for (int i = lo; i <= hi; ++i) {
                        int distance = Math.Abs(points[pix] - points[i]);
                        if (cumul[distance] > 0) {
                              --cumul[distance];
                        }
                        else {
                              inc(pix, lo, i - 1);
                              return false;
                        }
                  }
                  return true;
            }

            private bool inc(int pix, int lo, int hi) {
                  for (int i = lo; i <= hi; ++i) {
                        ++cumul[Math.Abs(points[pix] - points[i])];
                  }
                  return true;
            }
      }
}