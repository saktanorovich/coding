using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class Isoceles {
            public int howMany(int[] x, int[] y) {
                  int result = 0;
                  for (int i = 0; i < x.Length; ++i) {
                        for (int j = 0; j < x.Length; ++j) {
                              for (int k = j + 1; k < x.Length; ++k) {
                                    if (i != j && i != k && j != k) {
                                          result += isocelesRightTriangle(x[i], y[i], x[j], y[j], x[k], y[k]);
                                    }
                              }
                        }
                  }
                  return result;
            }

            private int isocelesRightTriangle(long ax, long ay, long bx, long by, long cx, long cy) {
                  long a = distance(ax, ay, bx, by);
                  long b = distance(ax, ay, cx, cy);
                  long c = distance(bx, by, cx, cy);
                  if (a == b && a + b == c) {
                        return 1;
                  }
                  return 0;
            }

            private long distance(long bx, long by, long cx, long cy) {
                  return (cx - bx) * (cx - bx) + (cy - by) * (cy - by);
            }
      }
}