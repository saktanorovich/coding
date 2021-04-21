using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class ElectronicScarecrows {
        public double largestArea(int[] x, int[] y, int max) {
            var result = 0;
            for (var i = 0; i < x.Length; ++i) {
                var points = new List<Point>();
                points.Add(new Point(x[i], y[i]));
                for (var j = 0; j < x.Length; ++j) {
                    if (j != i && x[j] >= x[i]) {
                        points.Add(new Point(x[j], y[j]));
                    }
                }
                result = Math.Max(result, largestArea(points.ToArray(), points.Count, max));
            }
            return result / 2.0;
        }

        private int largestArea(Point[] points, int n, int max) {
            Array.Sort(points, new PointsComparer(points[0]));
            var sq = new int[n, n];
            for (var i = 1; i < n; ++i)
                for (var j = i + 1; j < n; ++j) {
                    var triangleSquare = square(new[] { points[0], points[i], points[j] }, 3);
                    sq[i, j] = triangleSquare;
                    sq[j, i] = triangleSquare;
                }
            var best = new int[n, max + 1];
            for (var last = 0; last < n; ++last)
                for (var prev = 0; prev < last; ++prev)
                    for (var cnt = 3; cnt <= max; ++cnt) {
                        best[last, cnt] = Math.Max(best[last, cnt], best[prev, cnt - 1] + sq[prev, last]);
                    }
            var result = 0;
            for (var last = 0; last < n; ++last) {
                result = Math.Max(result, best[last, max]);
            }
            return result;
            /**
            var best = new int[n, n, max + 1];
            for (var curr = 0; curr < n; ++curr)
                for (var last = 0; last < curr; ++last)
                    for (var prev = 0; prev < last; ++prev)
                        for (var cnt = 3; cnt <= max; ++cnt) {
                            best[curr, last, cnt] = Math.Max(best[curr, last, cnt], best[last, prev, cnt - 1] + sq[curr, last]);
                        }
            var result = 0;
            for (var last = 0; last < n; ++last)
                for (var prev = 0; prev < last; ++prev) {
                    result = Math.Max(result, best[last, prev, max]);
                }
            return result;
            /**/
        }

        private static int square(IList<Point> points, int n) {
            var result = 0;
            for (var i = 0; i < n; ++i) {
                result += vector(points[i], points[(i + 1) % n]);
            }
            return Math.Abs(result);
        }

        private static int vector(Point a, Point b) { return a.X * b.Y - a.Y * b.X; }
        private static int scalar(Point a, Point b) { return a.X * b.X + a.Y * b.Y; }

        private struct Point {
            public readonly int X;
            public readonly int Y;

            public Point(int x, int y) {
                X = x;
                Y = y;
            }

            public static Point operator -(Point a, Point b) {
                return new Point(a.X - b.X, a.Y - b.Y);
            }
        }

        private struct PointsComparer : IComparer<Point> {
            private readonly Point _base;

            public PointsComparer(Point @base) {
                _base = @base;
            }

            public int Compare(Point x, Point y) {
                var a = x - _base;
                var b = y - _base;
                if (vector(a, b) != 0) {
                    return vector(a, b);
                }
                return scalar(a, a).CompareTo(scalar(b, b));
            }
        }
    }
}