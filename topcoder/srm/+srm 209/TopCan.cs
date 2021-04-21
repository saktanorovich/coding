using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class TopCan {
        public double minSurface(int volume) {
            var radius = Math.Pow(.5 * volume / Math.PI, 1.0 / 3);
            var height = volume / Math.PI / radius / radius;
            return 2 * Math.PI * radius * (radius + height);
        }
    }
}
