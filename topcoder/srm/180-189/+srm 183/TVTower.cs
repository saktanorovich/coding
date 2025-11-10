using System;

namespace TopCoder.Algorithm {
    public class TVTower {
        public double minRadius(int[] x, int[] y) {
            var points = new Point[x.Length];
            for (var i = 0; i < x.Length; ++i) {
                points[i] = new Point(x[i], y[i]);
            }
            return minRadius(points, points.Length);
        }

        private static double minRadius(Point[] points, int n) {
            var result = 1e+9;
            for (var i = 0; i < n; ++i) {
                result = Math.Min(result, get(points, points[i]));
                for (var j = i + 1; j < n; ++j) {
                    result = Math.Min(result, get(points, 0.5 * (points[i] + points[j])));
                    for (var k = j + 1; k < n; ++k) {
                        var side1 = new Line(points[i], points[j]);
                        var side2 = new Line(points[i], points[k]);
                        var bisector1 = side1.normal(0.5 * (points[i] + points[j]));
                        var bisector2 = side2.normal(0.5 * (points[i] + points[k]));
                        var center = bisector1.intersect(bisector2);
                        if (center != null) {
                            result = Math.Min(result, get(points, center));
                        }
                    }
                }
            }
            return Math.Sqrt(result);
        }

        private static double get(Point[] points, Point center) {
            var result = 0.0;
            foreach (var p in points) {
                result = Math.Max(result, center.distance(p));
            }
            return result;
        }

        private class Point {
            public readonly double X;
            public readonly double Y;

            public Point(double x, double y) {
                X = x;
                Y = y;
            }

            public double distance(Point other) {
                return (other.X - X) * (other.X - X) + (other.Y - Y) * (other.Y - Y);
            }

            public static Point operator +(Point a, Point b) {
                return new Point(a.X + b.X, a.Y + b.Y);
            }

            public static Point operator -(Point a, Point b) {
                return new Point(a.X - b.X, a.Y - b.Y);
            }

            public static Point operator *(double scalar, Point p) {
                return new Point(p.X * scalar, p.Y * scalar);
            }
        }

        private class Line {
            public readonly double A, B, C;

            public Line(Point a, Point b) {
                A = a.Y - b.Y;
                B = b.X - a.X;
                C = a.X * b.Y - a.Y * b.X;
            }

            public Point intersect(Line line) {
                if (!parallel(line)) {
                    var x = -1 * MathUtils.det(this.C, this.B, line.C, line.B) / MathUtils.det(this.A, this.B, line.A, line.B);
                    var y = -1 * MathUtils.det(this.A, this.C, line.A, line.C) / MathUtils.det(this.A, this.B, line.A, line.B);
                    return new Point(x, y);
                }
                return null;
            }

            public Line normal(Point point) {
                return new Line(point, new Point(point.X + A, point.Y + B));
            }

            private bool parallel(Line line) {
                return MathUtils.Sign(MathUtils.det(this.A, this.B, line.A, line.B)) == 0;
            }
        }

        private static class MathUtils {
            private const double Eps = 1e-10;

            public static int Sign(double x) {
                if (x + Eps < 0)
                    return -1;
                if (x - Eps > 0)
                    return +1;
                return 0;
            }

            public static double det(double a, double b, double c, double d) {
                return a * d - b * c;
            }
        }
    }
}