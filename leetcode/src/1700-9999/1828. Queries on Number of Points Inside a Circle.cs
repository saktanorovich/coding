using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1828 {
        public int[] CountPoints(int[][] points, int[][] queries) {
            var lookup = new Lookup(points.Select(p => new Point(p)).ToArray());
            var answ = new int[queries.Length];
            for (var i = 0; i < queries.Length; ++i) {
                answ[i] = lookup.Count(new Circle(queries[i]));
            }
            return answ;
        }

        private class Lookup {
            private readonly Dictionary<Point, int> hash;
            private readonly KDTree tree;

            public Lookup(Point[] points) {
                hash = new Dictionary<Point, int>();
                foreach (var p in points) {
                    hash.TryAdd(p, 0);
                    hash[p]++;
                }
                tree = KDTree.Create(hash.Keys.ToArray());
            }

            public int Count(Circle circle) {
                return Count(tree, circle.Center, circle.Radius);
            }

            private int Count(KDTree tree, Point target, int radius) {
                if (tree == null) {
                    return 0;
                }
                var count = 0;
                if (tree.Point.Distance(target) <= radius * radius) {
                    count += hash[tree.Point];
                }
                var axis = tree.Axis;
                if (tree[axis] > target[axis] - radius) {
                    count += Count(tree.LeChild, target, radius);
                }
                if (tree[axis] < target[axis] + radius) {
                    count += Count(tree.RiChild, target, radius);
                }
                return count;
            }
        }

        private class KDTree {
            public readonly int Axis;
            public readonly Point Point;
            public readonly KDTree LeChild;
            public readonly KDTree RiChild;

            private KDTree(int axis, Point point, KDTree leChild, KDTree riChild) {
                Axis = axis;
                Point = point;
                LeChild = leChild;
                RiChild = riChild;
            }

            public int this[int axis] => Point[axis % 2];

            public static KDTree Create(Point[] points) {
                return Create(points, 0, 0, points.Length - 1);
            }

            private static KDTree Create(Point[] points, int axis, int lo, int hi) {
                if (lo <= hi) {
                    Array.Sort(points, lo, hi - lo + 1, new PointComparer(axis));
                    var pivot = (lo + hi) / 2;
                    return new KDTree(axis, points[pivot],
                        Create(points, axis + 1, lo, pivot - 1),
                        Create(points, axis + 1, pivot + 1, hi));
                }
                return null;
            }
        }

        private struct Circle {
            public readonly Point Center;
            public readonly int Radius;

            public Circle(int[] circle) {
                Center = new Point(new[] { circle[0], circle[1] });
                Radius = circle[2];
            }
        }

        private struct Point {
            private readonly int[] coord;

            public Point(int[] coord) {
                this.coord = coord;
            }

            public int this[int axis] {
                get {
                    return coord[axis % 2];
                }
            }

            public override bool Equals(object obj) {
                var other = (Point)obj;
                return coord[0] == other.coord[0] &&
                       coord[1] == other.coord[1];
            }
            
            public override int GetHashCode() {
                return coord[0] * 1137 + coord[1];
            }

            public int Distance(Point that) {
                return (this[0] - that[0]) * (this[0] - that[0]) +
                       (this[1] - that[1]) * (this[1] - that[1]);
            }
        }

        private struct PointComparer : IComparer<Point> {
            private readonly int axis;

            public PointComparer(int axis) {
                this.axis = axis;
            }

            public int Compare(Point a, Point b) {
                return a[axis].CompareTo(b[axis]);
            }
        }
    }
}
