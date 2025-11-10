using System;
using System.Linq;

namespace TopCoder.Algorithm {
    public class Display {
        public string[] transform(int[] x, int[] y) {
            var points = new Point[x.Length];
            for (var i = 0; i < x.Length; ++i) {
                points[i] = new Point(x[i], y[i]);
            }
            return Array.ConvertAll(transform(points), point => point.ToString());
        }

        private Point[] transform(Point[] points) {
            return scale(trans(points, 0, 0), 1000, 1000);
        }

        private Point[] scale(Point[] points, int xmax, int ymax) {
            var maxx = points.Select(point => point.X).Max();
            var maxy = points.Select(point => point.Y).Max();
            return points.Select(point => {
                var x = point.X;
                var y = point.Y;
                if (maxx != 0) x = (int)(1.0 * x * xmax / maxx + 0.5);
                if (maxy != 0) y = (int)(1.0 * y * ymax / maxy + 0.5);
                return new Point(x, y);
            }).ToArray();
        }

        private Point[] trans(Point[] points, int xmin, int ymin) {
            var minx = points.Select(point => point.X).Min();
            var miny = points.Select(point => point.Y).Min();
            return points.Select(point => {
                return new Point(point.X + xmin - minx, point.Y + ymin - miny);
            }).ToArray();
        }

        public struct Point {
            public readonly int X;
            public readonly int Y;

            public Point(int x, int y) {
                X = x;
                Y = y;
            }

            public override string ToString() {
                return string.Format("{0} {1}", X, Y);
            }
        }
    }
}