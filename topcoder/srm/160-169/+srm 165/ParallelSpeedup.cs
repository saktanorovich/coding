using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TopCoder.Algorithm {
      public class ParallelSpeedup {
            public int numProcessors(int k, int overhead) {
                  int result = 1;
                  for (int numProcessors = 2; numProcessors <= k; ++numProcessors) {
                        if (amount(k, overhead, result) > amount(k, overhead, numProcessors)) {
                              result = numProcessors;
                        }
                  }
                  return result;
            }

            private long amount(int k, int overhead, int numProcessors) {
                  long result = 1L * overhead * numProcessors * (numProcessors - 1) / 2;
                  result += k / numProcessors;
                  if (k % numProcessors != 0) {
                        result = result + 1;
                  }
                  return result;
            }
      }
}