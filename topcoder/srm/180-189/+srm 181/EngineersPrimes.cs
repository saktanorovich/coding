using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class EngineersPrimes {
        public long smallestNonPrime(int n) {
            for (var x = 2; x < isprime.Length; ++x) {
                isprime[x] = x & 1;
            }
            isprime[2] = 1;
            for (var x = 3; x < isprime.Length; x += 2) {
                if (isprime[x] == 1) {
                    for (var y = x + x; y < isprime.Length; y += x) {
                        isprime[y] = 0;
                    }
                }
            }
            for (var x = 2;; ++x) {
                n -= isprime[x];
                if (n == -1)
                    return 1L * x * x;
            }
        }

        private static readonly int[] isprime = new int[2000000];
    }
}