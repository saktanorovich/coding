using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class WarehouseJob {
        public int[] stackBoxes(int[] weight, string[] above) {
            var best = new int[1 << weight.Length];
            var prev = new int[1 << weight.Length];
            for (var set = 1; set < 1 << weight.Length; ++set) {
                best[set] = -1;
            }
            doit(weight, above, best, prev, (1 << weight.Length) - 1, 0);
            var path = new List<int>();
            for (var set = (1 << weight.Length) - 1; set > 0;) {
                path.Add(prev[set]);
                set = set ^ (1 << prev[set]);
            }
            return path.ToArray();
        }

        private static int doit(int[] weight, string[] above, int[] best, int[] prev, int set, int h) {
            if (best[set] == -1) {
                best[set] = int.MaxValue / 2;
                for (var next = 0; next < weight.Length; ++next) {
                    if ((set & (1 << next)) != 0) {
                        var consistent = true;
                        for (var has = 0; has < weight.Length; ++has) {
                            if ((set & (1 << has)) == 0) {
                                if (above[has][next] == '1') {
                                    consistent = false;
                                    break;
                                }
                            }
                        }
                        if (consistent) {
                            var cost = doit(weight, above, best, prev, set ^ (1 << next), h + 1);
                            if (best[set] > cost + weight[next] * h) {
                                best[set] = cost + weight[next] * h;
                                prev[set] = next;
                            }
                        }
                    }
                }
            }
            return best[set];
        }
    }
}
