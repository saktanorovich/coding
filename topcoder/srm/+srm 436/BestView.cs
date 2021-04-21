using System;

namespace TopCoder.Algorithm {
      public class BestView {
            public int numberOfBuildings(int[] heights) {
                  int result = 0;
                  for (int i = 0; i < heights.Length; ++i) {
                        result = Math.Max(result, numberOfBuildings(heights, i));
                  }
                  return result;
            }

            private int numberOfBuildings(int[] heights, int ix) {
                  int result = 0;
                  for (int i = 0; i < heights.Length; ++i) {
                        if (i < ix) {
                              bool canBeSeen = true;
                              for (int j = i + 1; j < ix; ++j) {
                                    if (vector(j - ix, heights[j] - heights[ix], i - ix, heights[i] - heights[ix]) >= 0) {
                                          canBeSeen = false;
                                          break;
                                    }
                              }
                              if (canBeSeen) ++result;
                              continue;
                        }
                        if (i > ix) {
                              bool canBeSeen = true;
                              for (int j = ix + 1; j < i; ++j) {
                                    if (vector(j - ix, heights[j] - heights[ix], i - ix, heights[i] - heights[ix]) <= 0) {
                                          canBeSeen = false;
                                          break;
                                    }
                              }
                              if (canBeSeen) ++result;
                              continue;
                        }
                  }
                  return result;
            }

            private long vector(long ax, long ay, long bx, long by) {
                  return ax * by - ay * bx;
            }

            public static void Main(string[] args) {
                  Console.WriteLine(new BestView().numberOfBuildings(new int[] { 10 }));
                  Console.WriteLine(new BestView().numberOfBuildings(new int[] { 5, 5, 5, 5 }));
                  Console.WriteLine(new BestView().numberOfBuildings(new int[] { 1, 2, 7, 3, 2 }));
                  Console.WriteLine(new BestView().numberOfBuildings(new int[] { 1, 5, 3, 2, 6, 3, 2, 6, 4, 2, 5, 7, 3, 1, 5 }));
                  Console.WriteLine(new BestView().numberOfBuildings(new int[] { 1000000000, 999999999, 999999998, 999999997, 999999996, 1, 2, 3, 4, 5 }));
                  Console.WriteLine(new BestView().numberOfBuildings(new int[] { 244645169, 956664793, 752259473, 577152868, 605440232, 569378507, 111664772, 653430457, 454612157, 397154317 }));
                  Console.WriteLine(new BestView().numberOfBuildings(new int[] { 398, 435, 491, 292, 563, 532, 140, 167, 557, 111, 286, 157, 25, 210 }));

                  Console.ReadLine();
            }
      }
}