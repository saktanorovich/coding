using System;
using System.Collections;
using System.Collections.Generic;

namespace Topcoder.Algorithm {
      public class InterestingDigits {
            public int[] digits(int b) {
                  bool[] interesting = Array.ConvertAll(new bool[b],
                        delegate(bool x) {
                              return true;
                  });
                  for (int d1 = 0; d1 < b; ++d1) {
                        for (int d2 = 0; d2 < b; ++d2) {
                              for (int d3 = 0; d3 < b; ++d3) {
                                    int m = (d1 * b + d2) * b + d3;
                                    if (m > 0) {
                                          for (int d = 2; d < b; ++d) {
                                                if (m % d == 0) {
                                                      interesting[d] &= (d1 + d2 + d3) % d == 0;
                                                }
                                          }
                                    }
                              }
                        }
                  }
                  List<int> result = new List<int>();
                  for (int d = 2; d < b; ++d) {
                        if (interesting[d]) {
                              result.Add(d);
                        }
                  }
                  return result.ToArray();
            }
      }
}