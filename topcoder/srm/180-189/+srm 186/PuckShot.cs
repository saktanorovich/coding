using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class PuckShot {
        public double caromAngle(int puckCoord, int[] xcoords, int[] ycoords) {
            var beg = new List<Point>();
            var end = new List<Point>();
            for (var i = 0; i < xcoords.Length; ++i) {
                beg.Add(new Point(xcoords[i] - 25, ycoords[i]));
                end.Add(new Point(xcoords[i] + 25, ycoords[i]));
                beg.Add(new Point(6000 - xcoords[i] - 25, ycoords[i]));
                end.Add(new Point(6000 - xcoords[i] + 25, ycoords[i]));
            }
            return caromAngle(new Point(puckCoord, 0), beg, end);
        }

        private static double caromAngle (Point puck, IList<Point> beg, IList<Point> end) {
            var post0 = new Point(4500 - 91.50, 1733);
            var post1 = new Point(4500 + 91.50, 1733);
            var tst = new List<Point> {
                new Point(post0.X + MathUtils.Eps, post0.Y),
                new Point(post1.X - MathUtils.Eps, post1.Y)
            };
            for (var i = 0; i < beg.Count; ++i) {
                tst.Add(new Point(beg[i].X - MathUtils.Eps, beg[i].Y));
                tst.Add(new Point(end[i].X + MathUtils.Eps, end[i].Y));
            }
            var result = -1.0;
            for (var i = 0; i < tst.Count; ++i) {
                if (MathUtils.Vector(tst[i] - puck, post0 - puck) > 0 &&
                    MathUtils.Vector(tst[i] - puck, post1 - puck) < 0) {
                        if (test(puck, tst[i], beg, end)) {
                            var angel = Math.Atan2(tst[i].Y, tst[i].X - puck.X) * 180 / Math.PI;
                            if (MathUtils.Sign(result - angel) < 0) {
                                result = angel;
                            }
                        }
                }
            }
            return result;
        }

        private static bool test(Point puck, Point target, IList<Point> beg, IList<Point> end) {
            for (var i = 0; i < beg.Count; ++i) {
                var b = MathUtils.Vector(target - puck, beg[i] - puck);
                var e = MathUtils.Vector(target - puck, end[i] - puck);
                if (b == 0 || e == 0 || b != e) {
                    return false;
                }
            }
            return true;
        }

        private struct Point {
            public readonly double X;
            public readonly double Y;

            public Point(double x, double y) {
                X = x;
                Y = y;
            }

            public static Point operator -(Point a, Point b) {
                return new Point(a.X - b.X, a.Y - b.Y);
            }
        }

        private static class MathUtils {
            public const double Eps = 1e-12;

            public static int Sign(double x) {
                if (x + Eps < 0)
                    return -1;
                if (x - Eps > 0)
                    return +1;
                return 0;
            }

            public static int Vector(Point a, Point b) {
                return Sign(a.X * b.Y - a.Y * b.X);
            }
        }
    }
}