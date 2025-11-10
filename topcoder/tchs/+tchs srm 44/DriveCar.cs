using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class DriveCar {
        public int minNumberOfDrifts(string[] road) {
            return minNumberOfDrifts(road, road[0].Length);
        }

        private static int minNumberOfDrifts(string[] road, int n) {
            var g = new int[3, n];
            for (var i = 0; i < 3; ++i) {
                for (var j = 0; j < n; ++j) {
                    g[i, j] = int.MaxValue / 2;
                    if (road[i][j].Equals('#')) {
                        g[i, j] = -1;
                    }
                }
            }
            if (g[1, 0] != -1) {
                g[1, 0] = 0;
            }
            for (var j = 1; j < n; ++j) {
                if (g[0, j] != -1) {
                    update(ref g[0, j], g[0, j - 1], 0);
                    update(ref g[0, j], g[1, j - 1], 1);
                }
                if (g[1, j] != -1) {
                    update(ref g[1, j], g[1, j - 1], 0);
                    update(ref g[1, j], g[0, j - 1], 1);
                    update(ref g[1, j], g[2, j - 1], 1);
                }
                if (g[2, j] != -1) {
                    update(ref g[2, j], g[2, j - 1], 0);
                    update(ref g[2, j], g[1, j - 1], 1);
                }
            }
            var result = int.MaxValue / 2;
            for (var i = 0; i < 3; ++i) {
                if (g[i, n - 1] != -1) {
                    result = Math.Min(result, g[i, n - 1]);
                }
            }
            if (result < int.MaxValue / 2) {
                return result;
            }
            return -1;
        }

        private static void update(ref int x, int y, int change) {
            if (y != -1) {
                x = Math.Min(x, y + change);
            }
        }
    }
}
