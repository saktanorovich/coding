using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class EnclosingRectangle {
        public int smallestArea(string[] x, string[] y) {
            return smallestArea(
                string.Join("", x).Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray(),
                string.Join("", y).Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray());
        }

        private static int smallestArea(int[] x, int[] y) {
            var xx = new HashSet<int>();
            var yy = new HashSet<int>();
            for (var i = 0; i < x.Length; ++i) {
                xx.Add(x[i] - 1);
                xx.Add(x[i] + 1);
                yy.Add(y[i] - 1);
            }
            for (var i = 0; i < y.Length; ++i) {
                for (var j = i + 1; j < y.Length; ++j) {
                    if (y[j] < y[i]) {
                        var tx = x[i]; x[i] = x[j]; x[j] = tx;
                        var ty = y[i]; y[i] = y[j]; y[j] = ty;
                    }
                }
            }
            var best = int.MaxValue;
            foreach (var xmin in xx) {
                foreach (var xmax in xx) {
                    if (xmin < xmax) {
                        foreach (var ymin in yy) {
                            var inner = 0;
                            for (var k = 0; k < y.Length; ++k) {
                                if (xmin < x[k] && x[k] < xmax && ymin < y[k]) {
                                    inner = inner + 1;
                                    if (2 * inner >= x.Length) {
                                        best = Math.Min(best, (xmax - xmin) * (y[k] + 1 - ymin));
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return best;
        }
    }
}
