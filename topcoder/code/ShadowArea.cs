using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace TopCoder.Algorithm {
    public class ShadowArea {
        public double area(string[] room) {
            return area(room, room.Length, room[0].Length);
        }

        private double area(string[] room, int n, int m) {
            var origin = new Point(0.5, 0.5);
            var points = new List<Point>();
            points.Add(new Point(0, 0));
            points.Add(new Point(0, m));
            points.Add(new Point(n, 0));
            points.Add(new Point(n, m));
            var segments = new List<Segment>();
            segments.Add(new Segment(new Point(0, 0), new Point(0, m)));
            segments.Add(new Segment(new Point(0, 0), new Point(n, 0)));
            segments.Add(new Segment(new Point(n, 0), new Point(n, m)));
            segments.Add(new Segment(new Point(0, m), new Point(n, m)));
            var shadow = 1.0 * n * m;
            for (var i = 0; i < n; ++i) {
                for (var j = 0; j < m; ++j) {
                    if (room[i][j] == '*') {
                        origin = new Point(i + 0.5, j + 0.5);
                    }
                    if (room[i][j] == '#') {
                        var a = new Point(i, j);
                        var b = new Point(i, j + 1);
                        var c = new Point(i + 1, j);
                        var d = new Point(i + 1, j + 1);
                        points.Add(a);
                        points.Add(b);
                        points.Add(c);
                        points.Add(d);
                        segments.Add(new Segment(a, b));
                        segments.Add(new Segment(a, c));
                        segments.Add(new Segment(b, d));
                        segments.Add(new Segment(c, d));
                        shadow -= 1;
                    }
                }
            }
            points.Sort((a, b) => {
                var ax = a.x - origin.x;
                var ay = a.y - origin.y;
                var bx = b.x - origin.x;
                var by = b.y - origin.y;
                var aθ = Math.Atan2(ay, ax);
                var bθ = Math.Atan2(by, bx);
                return sign(aθ - bθ);
            });
            points.Add(points[0]);
            for (var i = 0; i + 1 < points.Count; ++i) {
                var a = points[i];
                var b = points[i + 1];
                var ax = a.x - origin.x;
                var ay = a.y - origin.y;
                var bx = b.x - origin.x;
                var by = b.y - origin.y;
                var v = ax * by - ay * bx;
                if (sign(v) < 0) {
                    throw new InvalidOperationException();
                }
                if (sign(v) > 0) {
                    var min = double.MaxValue;
                    foreach (var s in segments) {
                        min = Math.Min(min, area(origin, a, b, s));
                    }
                    shadow -= min;
                }
            }
            return shadow;
        }

        private double area(Point o, Point a, Point b, Segment s) {
            var p = make(o, a, s);
            var q = make(o, b, s);
            if (p != null & q != null) {
                return area(o, p, q);
            } else {
                return double.MaxValue;
            }
        }

        private double area(Point o, Point a, Point b) {
            var ax = a.x - o.x;
            var ay = a.y - o.y;
            var bx = b.x - o.x;
            var by = b.y - o.y;
            return Math.Abs(ax * by - ay * bx) / 2;
        }

        private Point make(Point o, Point a, Segment s) {

        }

        private int sign(double x) {
            if (x + 1e-9 < 0) {
                return -1;
            }
            if (x - 1e-9 > 0) {
                return +1;
            }
            return 0;
        }

        [DebuggerDisplay("({x}, {y})")]
        private struct Point {
            public readonly double x;
            public readonly double y;

            public Point(double x, double y) {
                this.x = x;
                this.y = y;
            }

            public double dist(Point p) {
                return (x - p.x) * (x - p.x) + (y - p.y) * (y - p.y);
            }
        }

        private struct Segment {
            public readonly Point a;
            public readonly Point b;

            public Segment(Point a, Point b) {
                this.a = a;
                this.b = b;
            }

            public Point mid() {
                var x = (a.x + b.x) * 0.5;
                var y = (a.y + b.y) * 0.5;
                return new Point(x, y);
            }
        }
    }
}
