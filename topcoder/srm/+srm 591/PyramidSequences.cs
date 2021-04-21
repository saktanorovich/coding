using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TopCoder.Algorithm {
      public class PyramidSequences {
            /* The length of the period is p = lcm(2n - 2, 2m - 2) = 2(n-1)(m-1)/gcd(n-1, m-1). The square length is gcd(n-1, m-1). */
            public long distinctPairs(int n, int m) {
                  long nn = n - 1;
                  long mm = m - 1;
                  long kk = gcd(nn, mm);
                  return (kk - 1) * (nn / kk) * (mm / kk) + calc(nn, mm, kk) + calc(nn - kk, mm - kk, kk);
            }

            private long calc(long nn, long mm, long kk) {
                  return (nn / kk / 2 + 1) * (mm / kk / 2 + 1);
            }

            private long gcd(long a, long b) {
                  while (a != 0 && b != 0) {
                        if (a > b) {
                              a %= b;
                        }
                        else {
                              b %= a;
                        }
                  }
                  return a + b;
            }

            public static void Main() {
                  Console.WriteLine(new PyramidSequences().distinctPairs(9, 7));
                  Console.WriteLine(new PyramidSequences().distinctPairs(3, 4));
                  Console.WriteLine(new PyramidSequences().distinctPairs(3, 5));
                  Console.WriteLine(new PyramidSequences().distinctPairs(43, 76));
                  Console.WriteLine(new PyramidSequences().distinctPairs(2, 1000000000));
                  Console.WriteLine(new PyramidSequences().distinctPairs(100000, 95555));
                  Console.WriteLine(new PyramidSequences().distinctPairs(27303491, 984873503)); // 10083931682964991
                  Console.WriteLine(new PyramidSequences().distinctPairs(100160064, 100170071)); // 1002552145595

                  Console.WriteLine("\nPress any key to continue...");
                  Console.ReadKey();
            }
      }
}
