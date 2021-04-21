using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class DogWoods {
        public double howFar(int[] x, int[] y, int[] diameter, int startx, int starty) {
            var trees = new List<Circle>();
            for (var i = 0; i < x.Length; ++i) {
                trees.Add(new Circle(x[i], y[i], diameter[i] / 2.0));
            }
            return howFar(trees, new Point(startx, starty), 10);
        }

        private static double howFar(List<Circle> trees, Point start, int threshold) {
            var result = 0.0;
            for (Point p1, p2; MathUtils.Sign(start.Length() - threshold) > 0; ) {
                var trajectory = new Circle(0, 0, start.Length());
                State next = null;
                foreach (var tree in trees) {
                    if (trajectory.Intersect(tree, out p1, out p2)) {
                        relax(new State(tree, p1, trajectory.Distance(p1, start)), ref next);
                        relax(new State(tree, p2, trajectory.Distance(p2, start)), ref next);
                    }
                }
                if (next != null) {
                    var center = getCenterCircleFor(next.Circle, threshold);
                    next.Circle.Intersect(center, out p1, out p2);
                    var d1 = next.Circle.Distance(next.Point, p1);
                    var d2 = next.Circle.Distance(next.Point, p2);
                    if (MathUtils.Sign(d1 - d2) <= 0) {
                        next.Point = p1;
                        next.Distance += d1;
                    }
                    else {
                        next.Point = p2;
                        next.Distance += d2;
                    }
                    trees.Remove(next.Circle);
                    result += next.Distance;
                    start = next.Point;
                }
                else return -1;
            }
            return result;
        }

        private static Circle getCenterCircleFor(Circle tree, int threshold) {
            var center = new Circle(0, 0, threshold);
            if (tree.IsIntersect(center)) {
                return center;
            }
            return new Circle(0, 0, tree.Center.Length() - tree.Radius);
        }

        private static void relax(State state, ref State current) {
            if (current == null || MathUtils.Sign(state.Distance - current.Distance) < 0) {
                current = state;
            }
        }

        private class State {
            public Circle Circle { get; set; }
            public Point Point { get; set; }
            public double Distance { get; set; }

            public State(Circle circle, Point point, double distance) {
                Circle = circle;
                Point = point;
                Distance = distance;
            }
        }

        private class Circle {
            public readonly Point Center;
            public readonly double Radius;

            public Circle(double x, double y, double radius) {
                Center = new Point(x, y);
                Radius = radius;
            }

            public bool IsIntersect(Circle other) {
                return IsIntersect(this, other);
            }

            private static bool IsIntersect(Circle c1, Circle c2) {
                var d = (c2.Center - c1.Center).Length();
                var a = MathUtils.Abs(c1.Radius - c2.Radius);
                var b = MathUtils.Abs(c1.Radius + c2.Radius);
                if (MathUtils.Sign(a - d) < 0 && MathUtils.Sign(d - b) <= 0) {
                    return true;
                }
                return false;
            }

            public bool Intersect(Circle other, out Point p1, out Point p2) {
                p1 = p2 = null;
                if (IsIntersect(other)) {
                    Intersect(this, other, out p1, out p2);
                    return true;
                }
                return false;
            }

            /* get intersection points using Pythagorean theorem and simple rotations.. */
            private static void Intersect(Circle c1, Circle c2, out Point p1, out Point p2) {
                var d = (c2.Center - c1.Center).Length();
                var r = (c1.Radius * c1.Radius - c2.Radius * c2.Radius + d * d) / 2 / d;
                var cos = (c2.Center.X - c1.Center.X) / d;
                var sin = (c2.Center.Y - c1.Center.Y) / d;
                if (MathUtils.Sign(c1.Radius - r) > 0){ 
                    var h = Math.Sqrt(c1.Radius * c1.Radius - r * r);
                    p1 = c1.Center + new Point(r * cos - h * sin, r * sin + h * cos);
                    p2 = c1.Center + new Point(r * cos + h * sin, r * sin - h * cos);
                }
                else {
                    p1 = c1.Center + new Point(r * cos, r * sin);
                    p2 = c1.Center + new Point(r * cos, r * sin);
                }
            }

            /* returns distance from point p1 to p2 on the circle assuming counterclockwise direction.. */
            public double Distance(Point p1, Point p2) {
                var v1 = p1 - Center;
                var v2 = p2 - Center;
                var vector = MathUtils.Vector(v1, v2) / Radius / Radius;
                var scalar = MathUtils.Scalar(v1, v2) / Radius / Radius;
                if (MathUtils.Sign(vector) >= 0) {
                    return Math.Acos(scalar) * Radius;
                }
                return (2 * MathUtils.Pi - Math.Acos(scalar)) * Radius;
            }
        }

        private class Point {
            public readonly double X;
            public readonly double Y;

            public Point(double x, double y) {
                X = x;
                Y = y;
            }

            public override bool Equals(object obj) {
                var other = (Point)obj;
                return MathUtils.Sign(X - other.X) == 0 &&
                       MathUtils.Sign(Y - other.Y) == 0;
            }

            public double Length() {
                return Math.Sqrt(X * X + Y * Y);
            }

            public static Point operator+(Point a, Point b) {
                return new Point(a.X + b.X, a.Y + b.Y);
            }

            public static Point operator-(Point a, Point b) {
                return new Point(a.X - b.X, a.Y - b.Y);
            }
        }

        private static class MathUtils {
            public const double Eps = 1e-10;
            public const double Pi = 3.1415926535897932384626433832795;

            public static int Sign(double x) {
                if (x + Eps < 0)
                    return -1;
                if (x - Eps > 0)
                    return +1;
                return 0;
            }

            public static double Abs(double x) {
                if (Sign(x) < 0)
                    return -x;
                return x;
            }

            public static double Vector(Point a, Point b) {
                return a.X * b.Y - a.Y * b.X;
            }

            public static double Scalar(Point a, Point b) {
                return a.X * b.X + a.Y * b.Y;
            }
        }
    }
}