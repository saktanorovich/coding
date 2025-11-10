using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class YahtzeeScore {
            public int maxPoints(int[] toss) {
                  int result = 0;
                  for (int i = 0; i < toss.Length; ++i) {
                        int max = 0;
                        for (int j = 0; j < toss.Length; ++j) {
                              if (toss[j] == toss[i]) {
                                    max += toss[i];
                              }
                        }
                        result = Math.Max(result, max);
                  }
                  return result;
            }
      }
}