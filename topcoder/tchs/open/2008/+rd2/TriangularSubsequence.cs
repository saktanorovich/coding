using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class TriangularSubsequence {
        public int longest(int[] a) {
            Array.Sort(a);
            var result = Math.Min(2, a.Length);
            for (var i = 0; i < a.Length; ++i)
                for (var j = i + 1; j < a.Length; ++j)
                    for (var k = j + 1; k < a.Length; ++k) {
                        if (a[i] + a[j] > a[k]) {
                            result = Math.Max(result, 1 + k - j + 1);
                        }
                    }
            return result;
        }
    }
}
