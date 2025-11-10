using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class ProductsMatrix {
        public long nthElement(int n, int k) {
            long lo = 1, hi = 1L * n * n;
            while (lo < hi) {
                long x = (lo + hi) / 2, c = 0;
                for (var i = 1; i <= n; ++i) {
                    c += Math.Min(n, x / i);
                }
                if (c < k) {
                    lo = x + 1;
                }
                else {
                    hi = x;
                }
            }
            return lo;
        }
    }
}
