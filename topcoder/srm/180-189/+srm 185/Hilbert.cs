using System;

namespace TopCoder.Algorithm {
    public class Hilbert {
        public int steps(int k, int x, int y) {
            if (k > 0) {
                return steps(k, 1 << (k - 1), x, y);
            }
            return 0;
        }

        private int steps(int k, int m, int x, int y) {
            var dx = new[] { 0, 0, m, m };
            var dy = new[] { 0, m, m, 0 };
            for (var quad = 0; quad < 4; ++quad) {
                var x0 = 1 + dx[quad];
                var y0 = 1 + dy[quad];
                var x1 = m + dx[quad];
                var y1 = m + dy[quad];
                if (x0 <= x && x <= x1 && y0 <= y && y <= y1) {
                    x -= dx[quad];
                    y -= dy[quad];
                    rotate(quad, m, ref x, ref y);
                    return quad * m * m + steps(k - 1, x, y);
                }
            }
            throw new Exception();
        }

        private static void rotate(int quad, int m, ref int x, ref int y) {
            var a = x;
            var b = y;
            switch (quad) {
                case 0:
                    x = b;
                    y = a;
                    break;
                case 3:
                    x = m + 1 - b;
                    y = m + 1 - a;
                    break;
            }
        }
    }
}