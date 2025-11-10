using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class RectangularGrid {
            public long countRectangles(int width, int height) {
                  long result = 0;
                  for (int i = 0; i < width; ++i) {
                        for (int j = 0; j < height; ++j) {
                              result += count(width - i, height - j);
                        }
                  }
                  return result;
            }

            private long count(int width, int height) {
                  return 1L * width * height - Math.Min(width, height);
            }
      }
}