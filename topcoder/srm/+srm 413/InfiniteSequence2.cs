using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class InfiniteSequence2 {
            public long calc(long n, int p, int q, int x, int y) {
                  if (n <= 0) {
                        return 1;
                  }
                  if (n < limit) {
                        if (memo[n] == 0) {
                              memo[n] = calc(n / p - x, p, q, x, y) + calc(n / q - y, p, q, x, y);
                        }
                        return memo[n];
                  }
                  else {
                        return calc(n / p - x, p, q, x, y) + calc(n / q - y, p, q, x, y);
                  }
            }

            private const long limit = 10 * 1000 * 1000;
            private long[] memo = new long[limit];

            public static void Main() {
                  Console.WriteLine(new InfiniteSequence2().calc(10000000, 2, 3, 10000000, 10000000));
                  Console.WriteLine(new InfiniteSequence2().calc(9999999999998, 2, 2, 3000000, 0));

                  Console.WriteLine("Press any key to continue...");
                  Console.ReadKey();
            }
      }
}