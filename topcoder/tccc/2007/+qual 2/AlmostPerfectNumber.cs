using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class AlmostPerfectNumber {
        public int count(int left, int right, int k) {
            var result = 0;
            for (var x = left; x <= right; ++x) {
                var sum = 0;
                for (var d = 1; d < x; ++d)
                    if (x % d == 0) {
                        sum += d;
                    }
                if (Math.Abs(x - sum) <= k) {
                    ++result;
                }
            }
            return result;
        }
    }
}
