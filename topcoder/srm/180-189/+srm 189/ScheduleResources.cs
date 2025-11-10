using System;

namespace TopCoder.Algorithm {
    public class ScheduleResources {
        public int schedule(int[] a, int[] b) {
            return schedule(a, b, a.Length);
        }

        private static int schedule(int[] a, int[] b, int n) {
            var input = new int[1 << n];
            for (var set = 0; set < 1 << n; ++set) {
                for (var i = 0; i < n; ++i) {
                    if (contains(set, i)) {
                        input[set] = input[set ^ (1 << i)] + a[i];
                        break;
                    }
                }
            }
            var best = new int[1 << n];
            for (var set = 1; set < 1 << n; ++set) {
                best[set] = int.MaxValue;
            }
            for (var set = 0; set < 1 << n; ++set) {
                for (var last = 0; last < n; ++last) {
                    if (!contains(set, last)) {
                        best[set | (1 << last)] = Math.Min(best[set | (1 << last)], Math.Max(best[set], input[set | (1 << last)]) + b[last]);
                    }
                }
            }
            return best[(1 << n) - 1];
        }

        private static bool contains(int set, int ix) {
            return (set & (1 << ix)) != 0;
        }
    }
}