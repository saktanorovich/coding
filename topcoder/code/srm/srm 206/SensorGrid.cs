using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopCoder.Algorithm {
     public class SensorGrid {
        public int[] largestRectangle(int W, int H, int[] x, int[] y) {
            var xs = new HashSet<int>(x);
            for (var i = 0; i < x.Length; ++i) {
                xs.Add(x[i] - 1);
                xs.Add(x[i] + 1);
            }
            xs.Add(0); xs.Add(W - 1);
            var best = new int[4];
            foreach (var x1 in xs) {
                foreach (var x2 in xs) {
                    if (0 <= x1 && x1 <= x2 && x2 < W) {
                        var yss = new SortedSet<int>();
                        for (var i = 0; i < x.Length; ++i) {
                            if (x1 <= x[i] && x[i] <= x2) {
                                yss.Add(y[i]);
                            }
                        }
                        yss.Add(-1); yss.Add(H);

                        // sweep-line algorithm on y-coordinate
                        var ys = yss.ToArray();
                        for (var i = 0; i + 1 < ys.Length; ++i) {
                            var y1 = ys[i + 0] + 1;
                            var y2 = ys[i + 1] - 1;
                            var w = x2 - x1 + 1;
                            var h = y2 - y1 + 1;
                            relax(ref best, new[] { x1, y1, w, h});
                        }
                    }
                }
            }
            return best;
        }

        private static void relax(ref int[] best, int[] rect) {
            if (best[2] * best[3] < rect[2] * rect[3]) {
                best = rect;
                return;
            }
            if (best[2] * best[3] > rect[2] * rect[3]) {
                return;
            }
            if (best[1] > rect[1]) {
                best = rect;
                return;
            }
            if (best[1] < rect[1]) {
                return;
            }
            if (best[0] > rect[0]) {
                best = rect;
                return;
            }
            if (best[0] < rect[0]) {
                return;
            }
            if (best[2] < rect[2]) {
                best = rect;
                return;
            }
        }
    }
}
