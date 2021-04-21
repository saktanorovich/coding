using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class DitherCounter {
            public int count(string dithered, string[] screen) {
                  int result = 0;
                  for (int i = 0; i < screen.Length; ++i) {
                        for (int j = 0; j < screen[i].Length; ++j) {
                              if (dithered.IndexOf(screen[i][j]) >= 0) {
                                    result = result + 1;
                              }
                        }
                  }
                  return result;
            }
      }
}