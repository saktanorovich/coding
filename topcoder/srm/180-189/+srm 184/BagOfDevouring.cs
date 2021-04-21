using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class BagOfDevouring {
        public double expectedYield(int[] values, int[] weights) {
            return expectedYield(values, weights, 100 + weights.Sum(), (1 << values.Length) - 1);
        }

        private double expectedYield(int[] values, int[] weights, int totalWeight, int set) {
            double result;
            if (!memo.TryGetValue(set, out result)) {
                for (var remove = 0; remove < values.Length; ++remove) {
                    if (contains(set, remove)) {
                        double expected = values[remove], devouredProb = 0;
                        for (var devour = 0; devour < values.Length; ++devour) {
                            if (remove != devour && contains(set, devour)) {
                                var prob = 1.0 * weights[devour] / (totalWeight - weights[remove]);
                                expected += prob * expectedYield(values, weights, totalWeight - weights[remove] - weights[devour], set ^ (1 << remove) ^ (1 << devour));
                                devouredProb += prob;
                            }
                        }
                        expected += expectedYield(values, weights, totalWeight - weights[remove], set ^ (1 << remove)) * (1 - devouredProb);
                        if (result < expected)
                            result = expected;
                    }
                }
                memo[set] = result;
            }
            return result;
        }

        private static bool contains(int set, int item) {
            return (set & (1 << item)) != 0;
        }

        private readonly IDictionary<int, double> memo = new Dictionary<int, double>();
    }
}