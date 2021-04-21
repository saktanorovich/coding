using System;

namespace TopCoder.Algorithm {
    public class CityLink {
        public int timeTaken(int[] x, int[] y) {
            var dist = new int[x.Length, y.Length];
            for (var i = 0; i < x.Length; ++i) {
                for (var j = 0; j < x.Length; ++j) {
                    dist[i, j] = distance(x[i], y[i], x[j], y[j]);
                }
            }
            for (var k = 0; k < x.Length; ++k)
                for (var i = 0; i < x.Length; ++i)
                    for (var j = 0; j < x.Length; ++j) {
                        dist[i, j] = Math.Min(dist[i, j], Math.Max(dist[i, k], dist[k, j]));
                    }
            var result = 0;
            for (var i = 0; i < x.Length; ++i) {
                for (var j = 0; j < x.Length; ++j) {
                    result = Math.Max(result, dist[i, j]);
                }
            }
            return result;
        }

        private static int distance(int x1, int y1, int x2, int y2) {
            if (x1 == x2) {
                return (Math.Abs(y2 - y1) + 1) / 2;
            }
            if (y1 == y2) {
                return (Math.Abs(x2 - x1) + 1) / 2;
            }
            return Math.Max(Math.Abs(x2 - x1), Math.Abs(y2 - y1));
        }
    }
}