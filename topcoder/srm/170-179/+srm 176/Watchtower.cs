using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class Watchtower {
        public int[] orderByArea(int[] x, int[] y) {
            var points = new Point[x.Length];
            for (var seqno = 0; seqno < x.Length; ++seqno) {
                points[seqno] = new Point(x[seqno], y[seqno]);
                points[seqno].Tag = seqno;
            }
            return orderByArea(points);
        }

        private static int[] orderByArea(Point[] points) {
            var square = new double[points.Length];
            for (var seqno = 0; seqno < points.Length; ++seqno) {
                square[seqno] = -calc(points, points[seqno], 0, 100);
            }
            Array.Sort(square, points);
            return points.Select(point => point.Tag).ToArray();
        }

        private static double calc(IEnumerable<Point> points, Point target, int min, int max) {
            var polygon = new List<Point> {
                new Point(min, min),
                new Point(max, min),
                new Point(max, max),
                new Point(min, max)
            };
            foreach (var point in points) {
                var x = (point.X + target.X) / 2;
                var y = (point.Y + target.Y) / 2;
                polygon = cut(polygon, target, new Line(point, target).normal(new Point(x, y)));
            }
            return MathUtils.square(polygon);
        }

        private static List<Point> cut(IList<Point> polygon, Point center, Line half) {
            var target = half.halfplane(center);
            Predicate<Point> belong = point => {
                return half.halfplane(point) == 0 ||
                       half.halfplane(point) == target;
            };
            var result = new List<Point>();
            var points = new List<Point>(polygon) { polygon[0] };
            for (var seqno = 0; seqno + 1 < points.Count; ++seqno) {
                var curr = points[seqno];
                var next = points[seqno + 1];
                if (belong(curr)) {
                    result.Add(curr);
                }
                if (belong(curr) != belong(next)) {
                    result.Add(half.intersect(new Line(curr, next)));
                }
            }
            return result;
        }

        private class Point {
            public readonly double X;
            public readonly double Y;
            public int Tag;

            public Point(double x, double y) {
                X = x;
                Y = y;
            }
        }

        private class Line {
            private readonly double A;
            private readonly double B;
            private readonly double C;

            public Line(Point a, Point b) {
                A = a.Y - b.Y;
                B = b.X - a.X;
                C = a.X * b.Y - a.Y * b.X;
            }

            public Point intersect(Line line) {
                var x = -1 * MathUtils.det(C, B, line.C, line.B) / MathUtils.det(A, B, line.A, line.B);
                var y = -1 * MathUtils.det(A, C, line.A, line.C) / MathUtils.det(A, B, line.A, line.B);
                return new Point(x, y);
            }

            public int halfplane(Point point) {
                return MathUtils.sign(A * point.X + B * point.Y + C);
            }

            public Line normal(Point point) {
                return new Line(point, new Point(point.X + A, point.Y + B));
            }
        }

        private static class MathUtils {
            private const double Eps = 1e-9;

            public static int sign(double x) {
                if (x + Eps < 0)
                    return -1;
                if (x - Eps > 0)
                    return +1;
                return 0;
            }

            public static double det(double a, double b, double c, double d) {
                return a * d - b * c;
            }

            public static double square(IList<Point> polygon) {
                var result = 0.0;
                var points = new List<Point>(polygon) { polygon[0] };
                for (var i = 0; i + 1 < points.Count; ++i) {
                    result += points[i].X * points[i + 1].Y - points[i].Y * points[i + 1].X;
                }
                return result / 2.0;
            }
        }
    }
}