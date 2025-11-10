using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class StreetParking {
            public int freeParks(string street) {
                  int result = 0;
                  for (int i = 0; i < street.Length; ++i) {
                        if (charAt(street, i) == '-') {
                              if (charAt(street, i + 1) != 'B')
                              if (charAt(street, i + 2) != 'B')
                              if (charAt(street, i + 1) != 'S')
                              if (charAt(street, i - 1) != 'S') {
                                    result = result + 1;
                              }
                        }
                  }
                  return result;
            }

            private char charAt(string s, int ix) {
                  if (0 <= ix && ix < s.Length) {
                        return s[ix];
                  }
                  return '-';
            }
      }
}