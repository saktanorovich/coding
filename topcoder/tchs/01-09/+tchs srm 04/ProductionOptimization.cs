using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class ProductionOptimization {
        public int getMost(int[] costs, int[] times, int totCost, int totTime) {
            this.costs = costs;
            this.times = times;
            best = new int[totTime + 1, totCost + 1, times.Length + 1];
            for (var time = 0; time <= totTime; ++time) {
                for (var cost = 0; cost <= totCost; ++cost) {
                    for (var unit = 0; unit < times.Length; ++unit) {
                        best[time, cost, unit] = -1;
                    }
                }
            }
            return get(totTime, totCost, 0);
        }

        private int get(int time, int cost, int unit) {
            if (time < 0 || cost < 0) {
                return 0;
            }
            if (unit < costs.Length) {
                if (best[time, cost, unit] == -1) {
                    best[time, cost, unit] = 0;
                    var newTime = time - times[unit];
                    var newCost = cost - costs[unit];
                    if (newTime >= 0) {
                        // try to build a new unit and keep <take> amount of costs at <unit>
                        for (var take = 0; take <= newCost; ++take) {
                            best[time, cost, unit] = Math.Max(best[time, cost, unit],
                                get(newTime, newCost - take, unit + 1) +
                                get(newTime, take, unit));
                        }
                    }
                }
                return best[time, cost, unit];
            }
            return 1;
        }

        private int[] costs;
        private int[] times;
        private int[,,] best;
    }
}
