using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TopCoder.Algorithm {
      public class ContinuedFractions {
            public int[] squareRoot(int n) {
                  int r = root(n);
                  List<int> result = new List<int>(new int[] { r });
                  int a0 = 1, b0 = r;
                  int a1 = 1, b1 = r;
                  do {
                        a1 = (n - b1 * b1) / a1;
                        int div = (r + b1) / a1;
                        int mod = (r + b1) % a1;
                        b1 = r - mod;
                        result.Add(div);
                  } while (a1 != a0 || b1 != b0);
                  return result.ToArray();
            }

            public int root(int n) {
                  int result = 1;
                  while (result * result <= n) {
                        result = result + 1;
                  }
                  return result - 1;
            }
      }
}