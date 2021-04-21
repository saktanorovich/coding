using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class VectorMatching {
        public double minimumLength(int[] x, int[] y) {
            var res = long.MaxValue;
            for (var set = 0; set < 1 << x.Length; ++set) {
                long size = 0, sx = 0, sy = 0;
                for (var i = 0; i < x.Length; ++i) {
                    if ((set & (1 << i)) != 0) {
                        sx += x[i];
                        sy += y[i];
                        ++size;
                    }
                    else {
                        sx -= x[i];
                        sy -= y[i];
                    }
                }
                if (2 * size == x.Length) {
                    res = Math.Min(res, sx * sx + sy * sy);
                }
            }
            return Math.Sqrt(res);
        }
    }
}
