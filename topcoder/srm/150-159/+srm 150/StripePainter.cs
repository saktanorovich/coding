using System;
using System.Collections;
using System.Collections.Generic;

namespace Topcoder.Algorithm {
      public class StripePainter {
            public int minStrokes(string stripes) {
                  int[,] f = new int[stripes.Length, stripes.Length];
                  for (int p = 0; p < stripes.Length; ++p) {
                        f[p, p] = 1;
                  }
                  for (int len = 1; len < stripes.Length; ++len) {
                        for (int p = 0; p + len < stripes.Length; ++p) {
                              if (stripes[p] != stripes[p + len]) {
                                    f[p, p + len] = int.MaxValue;
                                    for (int x = p; x < p + len; ++x) {
                                          f[p, p + len] = Math.Min(f[p, p + len], f[p, x] + f[x + 1, p + len]);
                                    }
                                    continue;
                              }
                              f[p, p + len] = Math.Min(f[p + 1, p + len], f[p, p + len - 1]);
                        }
                  }
                  return f[0, stripes.Length - 1];
            }
      }
}