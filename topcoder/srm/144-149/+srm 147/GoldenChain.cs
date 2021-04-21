using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class GoldenChain {
            public int minCuts(int[] sections) {
                  Array.Sort(sections);
                  int result = 0;
                  for (int i = 0; i < sections.Length; ++i) {
                        for (int j = 1; j <= sections[i]; ++j) {
                              result = result + 1;
                              int remains = sections.Length - i - 1;
                              if (j != sections[i]) {
                                    remains = remains + 1;
                              }
                              if (remains <= result) {
                                    goto exit;
                              }
                        }
                  }
                  exit: {
                        return result;
                  }
            }
      }
}