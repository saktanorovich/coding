using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class SquarePoints {
        public string determine(int[] x, int[] y) {
            var points = new Point[x.Length];
            for (var i = 0; i < x.Length; ++i) {
                points[i] = new Point(x[i], y[i]);
            }
            var count = determine(points);
            if (count > 0) {
                if (count > 1)
                    return "ambiguous";
                return "consistent";
            }
            return "inconsistent";
        }

        private static int determine(Point[] points) {
            /* by Dirichlet's principle there will be a line with at least two points on it.. */
            var result = 0;
            var slopes = new HashSet<Slope>();
            for (var i = 0; i < points.Length; ++i) {
                for (var j = i + 1; j < points.Length; ++j) {
                    var slope = new Slope(points[i], points[j]);
                    if (slopes.Contains(slope) ||
                        slopes.Contains(slope.Normal())) {
                        continue;
                    }
                    slopes.Add(slope);
                    slopes.Add(slope.Normal());
                    result += determine(points, slope, slope.Normal());
                }
            }
            return result;
        }

        private static int determine(Point[] points, Slope slope, Slope normal) {
            /* move slope and its normal from origin (0, 0) in order to find bounding box.. */
            int smin = int.MaxValue, smax = int.MinValue;
            int nmin = int.MaxValue, nmax = int.MinValue;
            foreach (var p in points) {
                smin = Math.Min(smin, slope[p]);
                smax = Math.Max(smax, slope[p]);
                nmin = Math.Min(nmin, normal[p]);
                nmax = Math.Max(nmax, normal[p]);
            }
            return determine(slope, smin, smax, normal, nmin, nmax, points);
        }

        private static int determine(Slope slope, int smin, int smax, Slope normal, int nmin, int nmax, Point[] points) {
            var result = 0;
            if (test(slope, smin, smax, normal, nmin, nmax, points) > 0) {
                var sdiff = smax - smin;
                var ndiff = nmax - nmin;
                if (ndiff < sdiff) {
                    result += test(slope, smin, smax, normal, nmax - sdiff, nmax, points);
                    result += test(slope, smin, smax, normal, nmax - sdiff, nmax, points, 1000);
                    result += test(slope, smin, smax, normal, nmin, nmin + sdiff, points);
                    result += test(slope, smin, smax, normal, nmin, nmin + sdiff, points, 1000);
                }
                else if (sdiff < ndiff) {
                    result += test(slope, smax - ndiff, smax, normal, nmin, nmax, points);
                    result += test(slope, smax - ndiff, smax, normal, nmin, nmax, points, 1000);
                    result += test(slope, smin, smin + ndiff, normal, nmin, nmax, points);
                    result += test(slope, smin, smin + ndiff, normal, nmin, nmax, points, 1000);
                }
                else {
                    result += test(slope, smin, smax, normal, nmin, nmax, points);
                    result += test(slope, smin, smax, normal, nmin, nmax, points, 1000);
                }
            }
            return result;
        }

        private static int test(Slope slope, int smin, int smax, Slope normal, int nmin, int nmax, Point[] points, int d) {
            /* these tests are required for checking single line, single corner.. */
            var result = 0;
            result += test(slope, smin - d, smax, normal, nmin - d, nmax, points);
            result += test(slope, smin - d, smax, normal, nmin, nmax + d, points);
            result += test(slope, smin, smax + d, normal, nmin - d, nmax, points);
            result += test(slope, smin, smax + d, normal, nmin, nmax + d, points);
            return result;
        }

        private static int test(Slope slope, int smin, int smax, Slope normal, int nmin, int nmax, Point[] points) {
            foreach (var p in points) {
                if (slope[p] != smin && slope[p] != smax && normal[p] != nmin && normal[p] != nmax) {
                    return 0;
                }
            }
            return 1;
        }

        private struct Point {
            public readonly int X;
            public readonly int Y;

            public Point(int x, int y) {
                X = x;
                Y = y;
            }
        }

        private struct Slope {
            private readonly int A;
            private readonly int B;

            private Slope(int a, int b) {
                A = a / MathUtils.Gcd(a, b);
                B = b / MathUtils.Gcd(a, b);
                if (A < 0) {
                    A = -A;
                    B = -B;
                }
            }

            public Slope(Point a, Point b)
                : this(a.Y - b.Y, b.X - a.X) {
            }

            public int this[Point p] {
                get { return A * p.X + B * p.Y; }
            }

            public Slope Normal() {
                return new Slope(-B, A);
            }
        }

        private static class MathUtils {
            public static int Gcd(int a, int b) {
                a = Math.Abs(a);
                b = Math.Abs(b);
                while (a != 0 && b != 0) {
                    if (a > b)
                        a %= b;
                    else
                        b %= a;
                }
                return a + b;
            }
        }
    }
}