using System;

namespace TopCoder.Algorithm {
    public class RaceCalculator {
        public int bestRace(int[] distances, int[] times) {
            var badness = new double[distances.Length];
            for (var k = 0; k < distances.Length; ++k) {
                badness[k] = double.MinValue;
                for (var i = 0; i < distances.Length; ++i)
                    for (var j = 0; j < distances.Length; ++j)
                        if (i != k && j != k && i != j) {
                            var percentage = times[k] / estimate(distances[k], distances[i], distances[j], times[i], times[j]) - 1;
                            if (badness[k] + 1e-9 < percentage) {
                                badness[k] = percentage;
                            }
                        }
            }
            var best = 0;
            for (var k = 1; k < distances.Length; ++k) {
                if (badness[k] + 1e-9 < badness[best]) best = k;
            }
            return best;
        }

        private static double estimate(double d, double d1, double d2, double t1, double t2) {
            if (d1 < d2) {
                return t1 * Math.Exp(Math.Log(t2 / t1) * Math.Log(d1 / d) / Math.Log(d1 / d2));
            }
            return estimate(d, d2, d1, t2, t1);
        }
    }
}