using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class GuessTheNumber {
            public int noGuesses(int upper, int answer) {
                  int lo = 1, hi = upper;
                  for (int it = 1; true; ++it) {
                        int x = (lo + hi) >> 1;
                        if (x == answer) {
                              return it;
                        }
                        if (x < answer) lo = x + 1;
                        if (x > answer) hi = x - 1;
                  }
            }
      }
}