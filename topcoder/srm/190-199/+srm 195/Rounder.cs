using System;
using System.Linq;

namespace TopCoder.Algorithm {
    public class Rounder {
        public int round(int n, int b) {
            for (var res = 0;; res += b) {
                if (res <= n && n <= res + b) {
                    if (n - res < res + b - n)
                        return res;
                    return res + b;
                }
            }
        }
    }
}