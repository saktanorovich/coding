using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class PulseRadar {
        public int[] deduceSpeeds(int[] x1, int[] y1, int[] x2, int[] y2, int[] x3, int[] y3) {
            return deduceSpeeds(new List<Point[]> {
                join(x1, y1),
                join(x2, y2),
                join(x3, y3)
            }, x1.Length);
        }

        private static int[] deduceSpeeds(IList<Point[]> blips, int numOfPoints) {
            var assignment = new List<Tuple<int, int>>[numOfPoints];
            for (var i0 = 0; i0 < numOfPoints; ++i0) {
                assignment[i0] = new List<Tuple<int, int>>();
                for (var i1 = 0; i1 < numOfPoints; ++i1) {
                    for (var i2 = 0; i2 < numOfPoints; ++i2) {
                        var p0 = blips[0][i0];
                        var p1 = blips[1][i1];
                        var p2 = blips[2][i2];
                        var v0 = p1 - p0;
                        var v1 = p2 - p1;
                        var v2 = p2 - p0;
                        if (v0.Equals(v1)) {
                            if (vector(v0, v2) == 0 && scalar(v0, v2) >= 0) {
                                assignment[i0].Add(new Tuple<int, int>(i1, i2));
                            }
                        }
                    }
                }
            }
            var speed = new int[numOfPoints];
            for (var i = 0; i < numOfPoints; ++i) {
                speed[i] = -1;
            }
            for (var exit = false; !exit; ) {
                exit = true;
                for (var i = 0; i < numOfPoints; ++i) {
                    if (speed[i] == -1) {
                        if (assignment[i].Count == 1) {
                            var assign = assignment[i][0];
                            var p = blips[1][assign.Item1];
                            var q = blips[2][assign.Item2];
                            var x = q.X - p.X;
                            var y = q.Y - p.Y;
                            speed[i] = (int)(Math.Sqrt(x * x + y * y) + 0.5);
                            for (var j = 0; j < numOfPoints; ++j) {
                                for (var k = 0; k < assignment[j].Count; ++k) {
                                    if (assignment[j][k].Item1 == assign.Item1 ||
                                        assignment[j][k].Item2 == assign.Item2) {
                                        assignment[j].RemoveAt(k);
                                        break;
                                    }
                                }
                            }
                            exit = false;
                        }
                    }
                }
            }
            for (var i = 0; i < numOfPoints; ++i)
                if (speed[i] < 0)
                    return new int[0];
            return speed;
        }

        private static Point[] join(int[] x, int[] y) {
            var result = new Point[x.Length];
            for (var i = 0; i < x.Length; ++i) {
                result[i] = new Point(x[i], y[i]);
            }
            return result;
        }

        private static int vector(Point a, Point b) {
            return a.X * b.Y - a.Y * b.X;
        }

        private static int scalar(Point a, Point b) {
            return a.X * b.X + a.Y * b.Y;
        }

        private class Point : IEquatable<Point> {
            public readonly int X;
            public readonly int Y;

            public Point(int x, int y) {
                X = x;
                Y = y;
            }

            public bool Equals(Point other) {
                return X == other.X && Y == other.Y;
            }

            public static Point operator -(Point a, Point b) {
                return new Point(a.X - b.X, a.Y - b.Y);
            }
        }
    }
}