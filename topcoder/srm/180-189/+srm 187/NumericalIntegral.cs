using System;
using System.Linq;

namespace TopCoder.Algorithm {
    public class NumericalIntegral {
        public string integrate(double x1, double x2) {
            var result = 0.0;
            for (var x = x1; x <= x2; x += 1e-6) {
                result += Math.Exp(-x * x);
            }
            result *= 1e-6;
            return result.ToString("F5");
        }
    }
}