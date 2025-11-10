using System;
using System.Collections.Generic;

public class GrasslandFencer {
      private double[] dp = new double[1 << 16];

      public double maximalFencedArea(int[] fences) {
            for (int i = 0; i < (1 << fences.Length); ++i) {
                  dp[i] = -1;
            }
            return maximalFencedArea((1 << fences.Length) - 1, fences);
      }

      private double maximalFencedArea(int set, int[] fences) {
            if (dp[set] < 0.0) {
                  dp[set] = 0.0;
                  if (cardinality(set) > 2) {
                        for (int i = 0; i < fences.Length; ++i) {
                              for (int j = i + 1; j < fences.Length; ++j) {
                                    for (int k = j + 1; k < fences.Length; ++k) {
                                          if (contains(set, i) && contains(set, j) && contains(set, k)) {
                                                int[] triangle = new int[3] { fences[i], fences[j], fences[k] };
                                                if (isValid(triangle)) {
                                                      dp[set] = Math.Max(dp[set], maximalFencedArea(exclude(exclude(exclude(set, i), j), k), fences) + square(triangle));
                                                }
                                          }
                                    }
                              }
                        }
                  }
            }
            return dp[set];
      }

      private double square(int[] triangle) {
            double A = triangle[0], B = triangle[1], C = triangle[2];
            double p = 1.0 * (A + B + C) / 2;
            return Math.Sqrt(p * (p - A) * (p - B) * (p - C));
      }

      private bool contains(int set, int x) {
            return ((set & (1 << x)) == (1 << x));
      }

      private int exclude(int set, int x) {
            return (set ^ (1 << x));
      }

      private bool isValid(int[] triangle) {
            Array.Sort(triangle);
            double A = triangle[0], B = triangle[1], C = triangle[2];
            return (A + B > C);
      }

      private int cardinality(int set) {
            int result = 0;
            for (; set > 0; set -= (set & (-set))) {
                  ++result;
            }
            return result;
      }

      static void Main(string[] args) {
            Console.WriteLine(new GrasslandFencer().maximalFencedArea(new int[] { 3, 4, 5, 6, 7, 8, 9 }));
            Console.WriteLine(new GrasslandFencer().maximalFencedArea(new int[] { 1, 2, 4, 8 }));
            Console.WriteLine(new GrasslandFencer().maximalFencedArea(new int[] { 7, 4, 4, 4 }));
            Console.WriteLine(new GrasslandFencer().maximalFencedArea(new int[] { 21, 72, 15, 55, 16, 44, 54, 63, 69, 35, 75, 69, 76, 70, 50, 81 }));

            Console.ReadKey();
      }
}
