using System;

namespace TopCoder.Algorithm {
    public class LuckyXor {
        public int construct(int a) {
            for (var b = a + 1; b <= 100; ++b) {
                if (isLucky(a ^ b)) {
                    return b;
                }
            }
            return -1;
        }

        private static bool isLucky(long x) {
            if (x > 0) {
                for (; x > 0; x /= 10) {
                    var d = x % 10;
                    if (d != 4 && d != 7) {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
    }
}