using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class TravelOnMars {
            public int minTimes(int[] range, int startCity, int endCity) {
                  int n = range.Length;
                  int[,] d = new int[n, n];
                  for (int i = 0; i < n; ++i) {
                        for (int j = 0; j < n; ++j) {
                              d[i, j] = (int)1e+6;
                        }
                  }
                  for (int i = 0; i < n; ++i) {
                        for (int k = 1; k <= Math.Min(range[i], n - 1); ++k) {
                              for (int s = -1; s <= +1; s += 2) {
                                    d[i, (i + s * k + n) % n] = 1;
                              }
                        }
                  }
                  for (int k = 0; k < n; ++k) {
                        for (int i = 0; i < n; ++i) {
                              for (int j = 0; j < n; ++j) {
                                    d[i, j] = Math.Min(d[i, j], d[i, k] + d[k, j]);
                              }
                        }
                  }
                  return d[startCity, endCity];
            }

            internal static void Main(string[] args) {
                  Console.WriteLine(new TravelOnMars().minTimes(new int[] { 2, 1, 1, 1, 1, 1 }, 1, 4)); // Returns: 2
                  Console.WriteLine(new TravelOnMars().minTimes(new int[] { 2, 1, 1, 1, 1, 1 }, 4, 1)); // Returns: 3
                  Console.WriteLine(new TravelOnMars().minTimes(new int[] { 2, 1, 1, 2, 1, 2, 1, 1 }, 2, 6)); // Returns: 3
                  Console.WriteLine(new TravelOnMars().minTimes(new int[] { 3, 2, 1, 1, 3, 1, 2, 2, 1, 1, 2, 2, 2, 2, 3 }, 6, 13)); // Returns: 4
                  Console.WriteLine(new TravelOnMars().minTimes(new int[] { 1, 50, 1 }, 1, 2)); // Returns: 1

                  Console.WriteLine("\nPress any key to continue...");
                  Console.ReadKey();
            }
      }
}
