using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class MagicianTour {
        public int bestDifference(string[] roads, int[] populations) {
            /* note: each city must be assigned one of the two shows.. */
            var diff = new List<int>();
            var assigned = new bool[populations.Length];
            for (var i = 0; i < populations.Length; ++i) {
                if (!assigned[i]) {
                    diff.Add(calc(roads, populations, assigned, 0, i));
                }
            }
            var offset = populations.Sum();
            var possible = new bool[2 * offset + 1]; possible[offset] = true;
            foreach (var value in diff) {
                var next = new bool[2 * offset + 1];
                for (var sum = offset; sum < 2 * offset; ++sum) {
                    if (possible[sum]) {
                        next[sum - value] = true;
                        next[sum + value] = true;
                    }
                }
                possible = next;
            }
            for (var sum = 0; sum <= offset; ++sum)
                if (possible[offset - sum] || possible[offset + sum])
                    return sum;
            throw new Exception();
        }

        private static int calc(string[] roads, int[] populations, bool[] assigned, int show, int current) {
            assigned[current] = true;
            var result = populations[current] * (-2 * show + 1);
            for (var next = 0; next < roads.Length; ++next)
                if (roads[current][next] == '1')
                    if (!assigned[next]) {
                        result += calc(roads, populations, assigned, show ^ 1, next);
                    }
            return result;
        }
    }
}