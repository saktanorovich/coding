using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class ArithmeticProgression {
            public double minCommonDifference(int a0, int[] seq) {
                  if (seq.Length > 0) {
                        bool inc = a0 <= seq[0];
                        for (int i = 1; i < seq.Length; ++i) {
                              inc &= seq[i - 1] <= seq[i];
                        }
                        if (inc) {
                              double res = double.MaxValue;
                              for (int k = 0; k < seq.Length; ++k) {
                                    double d = (1.0 * seq[k] - a0) / (k + 1);
                                    for (int i = 0; i < seq.Length; ++i) {
                                          if (Math.Floor(a0 + (i + 1) * d) != seq[i]) {
                                                goto next;
                                          }
                                    }
                                    res = Math.Min(res, d);
                                    next:;
                              }
                              return res < double.MaxValue ? res : -1;
                        }
                        return -1;
                  }
                  return 0;
            }

            public static void Main() {
                  Console.WriteLine(new ArithmeticProgression().minCommonDifference(201850,
                        new int[] { 202213, 202576, 202939, 203303, 203666, 204029, 204393, 204756,
                                    205119, 205483, 205846, 206209, 206573, 206936, 207299, 207663,
                                    208026, 208389, 208753, 209116, 209479, 209843, 210206, 210569,
                                    210933, 211296, 211659, 212023}));
                  Console.WriteLine(new ArithmeticProgression().minCommonDifference(1, new int[] { 2, 3, 4, 5, 6 }));
                  Console.WriteLine(new ArithmeticProgression().minCommonDifference(885027,
                        new int[] { 885116, 885206, 885296, 885385, 885475, 885565, 885654, 885744,
                                    885834, 885924, 886013, 886103, 886193, 886282, 886372, 886462,
                                    886552, 886641, 886731, 886821, 886910, 887000, 887090, 887179,
                                    887269, 887359, 887449, 887538, 887628, 887718, 887807, 887897,
                                    887987, 888077, 888166, 888256, 888346, 888435, 888525, 888615,
                                    888704, 888794, 888884 }));
                  Console.WriteLine(new ArithmeticProgression().minCommonDifference(3, new int[] { 3, 3, 3, 3, 4 }));
                  Console.WriteLine(new ArithmeticProgression().minCommonDifference(0, new int[] { 6, 13, 20, 27 }));
                  Console.WriteLine(new ArithmeticProgression().minCommonDifference(3, new int[] { }));
                  Console.WriteLine(new ArithmeticProgression().minCommonDifference(1, new int[] { -3 }));
                  Console.WriteLine(new ArithmeticProgression().minCommonDifference(0, new int[] { 6, 14 }));

                  Console.WriteLine("Press any key to continue...");
                  Console.ReadKey();
            }
      }
}