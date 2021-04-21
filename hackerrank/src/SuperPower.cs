using System;

namespace interview.hackerrank {
    public class SuperPower {
        public int possible(int z) {
            for (var p = 2; p * p <= z; ++p) {
                for (long k = p * p; k <= z; k *= p) {
                    if (k == z) {
                        return 1;
                    }
                }
            }
            return 0;
        }
    }
}
