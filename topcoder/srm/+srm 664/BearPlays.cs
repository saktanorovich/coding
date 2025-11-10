using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class BearPlays {
        public int pileSize(int a, int b, int k) {
            return pileSizeImpl(a, a + b, k);
        }

        private static int pileSizeImpl(long a, long sum, int k) {
            /* assume at some step we have 2a > sum; after that we change a to 2a-sum untill 2a < sum and so on.. */
            a = a * pow(2, k, sum) % sum;
            return (int)Math.Min(a, sum - a);
        }

        private static long pow(long x, long k, long mod) {
            if (k > 0) {
                if (k % 2 == 1)
                    return (x * pow(x, k - 1, mod)) % mod;
                return pow((x * x) % mod, k / 2, mod);
            }
            return 1;
        }
    }
}