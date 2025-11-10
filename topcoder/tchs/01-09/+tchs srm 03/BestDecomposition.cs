using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class BestDecomposition {
        public int maxProduct(int n) {
            var res = 1;
            while (n > 4) {
                res = (res * 3) % 10007;
                n = n - 3;
            }
            res = (res * n) % 10007;
            return res;
        }
    }
}
