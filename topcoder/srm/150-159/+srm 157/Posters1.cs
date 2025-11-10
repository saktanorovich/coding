using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class Posters {
            public int maxCover(int width, int height, int[] pWidth, int[] pHeight) {
                  int totalSquare = 0;
                  List<Rectangle> posters = new List<Rectangle>();
                  for (int i = 0; i < pWidth.Length; ++i) {
                        totalSquare += pWidth[i] * pHeight[i];
                        posters.Add(new Rectangle(pWidth[i], pHeight[i]));
                  }
                  foreach (List<Rectangle> permutation in PermuteUtils.permute(posters)) {
                        maxCover(new Rectangle(width, height), permutation, 0, totalSquare);
                  }
                  return maxArea;
            }

            private int maxArea = 0;

            private void maxCover(Rectangle wall, List<Rectangle> posters, int cardinality, int squareRemains) {
                  if (cardinality < posters.Count) {
                        Rectangle poster = posters[cardinality];
                        if (getCover(posters, cardinality) + squareRemains > maxArea) {
                              if (cardinality > 1) {
                                    int bestx = 0;
                                    int besty = 0;
                                    int bestc = 0;
                                    for (int x = 0; x + poster.height <= wall.height; ++x) {
                                          for (int y = 0; y + poster.widthh <= wall.widthh; ++y) {
                                                poster.x = x;
                                                poster.y = y;
                                                int currc = getCover(posters, cardinality + 1);
                                                if (currc > bestc) {
                                                      bestc = currc;
                                                      bestx = x;
                                                      besty = y;
                                                }
                                          }
                                    }
                                    poster.x = bestx;
                                    poster.y = besty;
                              }
                              else {
                                    poster.x = cardinality * (wall.height - poster.height);
                                    poster.y = cardinality * (wall.widthh - poster.widthh);
                              }
                              maxCover(wall, posters, cardinality + 1, squareRemains - poster.height * poster.widthh);
                        }
                  }
                  maxArea = Math.Max(maxArea, getCover(posters, cardinality));
            }

            private int getCover(List<Rectangle> posters, int numOfPosters) {
                  int result = 0;
                  for (int set = 1; set < 1 << numOfPosters; ++set) {
                        int sign = -1;
                        int xmin = int.MinValue, ymin = int.MinValue;
                        int xmax = int.MaxValue, ymax = int.MaxValue;
                        for (int i = 0; i < numOfPosters; ++i) {
                              if ((set & (1 << i)) != 0) {
                                    xmin = Math.Max(xmin, posters[i].x);
                                    ymin = Math.Max(ymin, posters[i].y);
                                    xmax = Math.Min(xmax, posters[i].x + posters[i].height);
                                    ymax = Math.Min(ymax, posters[i].y + posters[i].widthh);
                                    sign = -sign;
                              }
                        }
                        if (xmin < xmax && ymin < ymax) {
                              result += sign * (xmax - xmin) * (ymax - ymin);
                        }
                  }
                  return result;
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

            private static class PermuteUtils {
                  public static List<List<T>> permute<T>(List<T> list) {
                        List<List<T>> result = new List<List<T>>();
                        if (list.Count == 0) {
                              result.Add(new List<T>());
                        }
                        else {
                              for (int i = 0; i < list.Count; ++i) {
                                    foreach (List<T> res in permute(remove(list, i))) {
                                          result.Add(concat(new List<T>(new T[] { list[i] }), res));
                                    }
                              }
                        }
                        return result;
                  }

                  public static List<T> concat<T>(List<T> list1, List<T> list2) {
                        List<T> result = new List<T>(list1);
                        foreach (T item in list2) {
                              result.Add(item);
                        }
                        return result;
                  }

                  private static List<T> remove<T>(List<T> list, int index) {
                        List<T> result = new List<T>(list);
                        result.RemoveAt(index);
                        return result;
                  }
            }
      }
}