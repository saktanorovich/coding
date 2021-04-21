using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class TVAntenna {
        public double minRadius(int[] x, int[] y) {
            var result = int.MaxValue;
            for (var i = -200; i <= +200; ++i)
                for (var j = -200; j <= +200; ++j) {
                    var distance = 0;
                    for (var k = 0; k < x.Length; ++k) {
                        distance = Math.Max(distance, (x[k] - i) * (x[k] - i) + (y[k] - j) * (y[k] - j));
                    }
                    result = Math.Min(result, distance);
                }
            return Math.Sqrt(result);
        }
    }
}