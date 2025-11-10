using System;
using System.Linq;

namespace TopCoder.Algorithm {
    public class FanFailure {
        public int[] getRange(int[] capacities, int minCooling) {
            return new[] {
                getRange(minCooling, capacities.OrderBy(x => x).ToArray()),
                getRange(minCooling, capacities.OrderByDescending(x => x).ToArray())
            };
        }

        private static int getRange(int minCooling, int[] capacities) {
            int total = capacities.Sum(), failure = 0;
            for (var take = 1; take <= capacities.Length; ++take) {
                failure += capacities[take - 1];
                if (total - failure < minCooling) {
                    return take - 1;
                }
            }
            return capacities.Length;
        }
    }
}