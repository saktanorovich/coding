using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class Warehouse {
        public int feetWide(int[] x, int[] y) {
            var points = new List<Point>();
            for (var i = 0; i < x.Length; ++i) {
                points.Add(new Point(x[i], y[i]));
            }
            return feedWide(points, 0, 200);
        }

        private static int feedWide(IList<Point> posts, int min, int max) {
            /* the optimal solution will be reached when trucks
             * route touches two posts on the same line.. */
            var points = new List<Point>(posts);
            for (var x = min; x <= max; ++x) {
                points.Add(new Point(x, min));
                points.Add(new Point(x, max));
            }
            return feedWide(points, points.Count, min, max);
        }

        private static int feedWide(List<Point> points, int numOfPoints, int min, int max) {
            var result = 0.0;
            for (var i = 0; i < numOfPoints; ++i) {
                if (!boundary(points[i], min, max)) {
                    for (var j = i + 1; j < numOfPoints; ++j) {
                        if (!boundary(points[j], min, max)) {
                            for (var k = 0; k < numOfPoints; ++k) {
                                if (!boundary(points[k], min, max) && k != i && k != j) {
                                    relax(ref result, feedWide(points, points[i], points[j], points[k]));
                                }
                            }
                            var normal = new Line(points[i], points[j]).normal();
                            var p1 = points[i];
                            var p2 = points[i] + normal;
                            var p3 = points[j];
                            relax(ref result, feedWide(points, p1, p2, p3));
                        }
                    }
                }
            }
            return (int)(result - MathUtils.Eps);
        }

        private static double feedWide(IList<Point> points, Point p1, Point p2, Point p3) {
            var caliper = new Line(p1, p2);
            foreach (var check in points) {
                if (MathUtils.Vector(check - p1, caliper.direct()) *
                    MathUtils.Vector(check - p3, caliper.direct()) < 0) {
                    return 0;
                }
            }
            return caliper.distance(p3);
        }

        private static void relax(ref double result, double value) {
            if (MathUtils.Sign(result - value) < 0) {
                result = value;
            }
        }

        private static bool boundary(Point point, int min, int max) {
            if (point.Y == min || point.Y == max) {
                return min < point.X && point.X < max;
            }
            return false;
        }

        private class Point {
            public readonly int X;
            public readonly int Y;

            public Point(int x, int y) {
                X = x;
                Y = y;
            }

            public static Point operator +(Point a, Point b) {
                return new Point(a.X + b.X, a.Y + b.Y);
            }

            public static Point operator -(Point a, Point b) {
                return new Point(a.X - b.X, a.Y - b.Y);
            }
        }

        private struct Line {
            public readonly int A, B, C;

            public Line(Point a, Point b) {
                A = a.Y - b.Y;
                B = b.X - a.X;
                C = a.X * b.Y - a.Y * b.X;
            }

            public Point normal() {
                return new Point(+A, +B);
            }

            public Point direct() {
                return new Point(-B, +A);
            }

            public double distance(Point p) {
                return Math.Abs(A * p.X + B * p.Y + C) / Math.Sqrt(A * A + B * B);
            }
        }

        private static class MathUtils {
            public const double Eps = 1e-9;

            public static int Sign(double x) {
                if (x + Eps < 0)
                    return -1;
                if (x - Eps > 0)
                    return +1;
                return 0;
            }

            public static int Vector(Point a, Point b) {
                return a.X * b.Y - a.Y * b.X;
            }
        }
    }
}