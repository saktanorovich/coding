using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TopCoder.Algorithm {
      public class NumberGuesser {
            public int guess(string leftOver) {
                  /* the leftOver before removing can be represented as sum{9 * 11..1 * 10^k, for some k}
                   * so the leftOver should be divisible by 9.. */
                  int sum = 0;
                  foreach (char c in leftOver) {
                        sum += c - '0';
                  }
                  if (sum % 9 == 0) {
                        return 9;
                  }
                  return 9 - (sum % 9);
            }
      }
}