using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class Bonuses {
            public int[] getDivision(int[] points) {
                  int total = 0, divided = 0;
                  foreach (int value in points) {
                        total += value;
                  }
                  int[] result = new int[points.Length];
                  for (int i = 0; i < result.Length; ++i) {
                        result[i] = 100 * points[i] / total;
                        divided += result[i];
                  }
                  bool[] assigned = new bool[result.Length];
                  for (int i = 0; i < 100 - divided; ++i) {
                        int who = -1;
                        for (int p = 0; p < result.Length; ++p) {
                              if (!assigned[p]) {
                                    if (who == -1 || points[p] > points[who]) {
                                          who = p;
                                    }
                              }
                        }
                        assigned[who] = true;
                        ++result[who];
                  }
                  return result;
            }
      }
}