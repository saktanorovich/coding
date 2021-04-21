using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class PointInPolygon {
        public string testPoint(string[] vertices, int testPointX, int testPointY) {
            return testPoint(Array.ConvertAll(vertices, vertex => {
                var data = vertex.Split(' ');
                var x = int.Parse(data[0]);
                var y = int.Parse(data[1]);
                return new Point(x, y);
            }), new Point(testPointX, testPointY), vertices.Length);
        }

        private static string testPoint(Point[] points, Point test, int n) {
            var cross = 0;
            for (var i = 0; i < n; ++i) {
                Point a = points[i], b = points[(i + 1) % n];
                if (contains(a, b, test)) {
                    return Boundary;
                }
                if (a.X == b.X) {
                    var ymin = Math.Min(a.Y, b.Y);
                    var ymax = Math.Max(a.Y, b.Y);
                    if (test.X > a.X) {
                        /* scale polygon points two times in order to perform exact ray-casting.. */
                        if (2 * ymin < 2 * test.Y + 1 && 2 * test.Y + 1 < 2 * ymax) {
                            cross = cross + 1;
                        }
                    }
                }
            }
            if (cross % 2 == 0)
                return Exterior;
            return Interior;
        }

        private static bool contains(Point a, Point b, Point test) {
            var xmin = Math.Min(a.X, b.X);
            var ymin = Math.Min(a.Y, b.Y);
            var xmax = Math.Max(a.X, b.X);
            var ymax = Math.Max(a.Y, b.Y);
            if (xmin == xmax) {
                if (test.X == xmin)
                    return ymin <= test.Y && test.Y <= ymax;
            }
            if (ymin == ymax) {
                if (test.Y == ymin)
                    return xmin <= test.X && test.X <= xmax;
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
        }

        private const string Interior = "INTERIOR";
        private const string Exterior = "EXTERIOR";
        private const string Boundary = "BOUNDARY";
    }
}