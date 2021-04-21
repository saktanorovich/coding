using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class ClosestPoints {
        public int[] distance(int n, int range, int seed) {
            return distance(getPoints(n, range, seed));
        }

        private int[] distance(Point[] points) {
            var kdTree = makeBy(points, 0, 0, points.Length - 1);
            foreach (var point in points) {
                search(kdTree, point);
            }
            return new[] { (int)closestDistance, (int)closestPairsCount / 2 };
        }

        private long closestDistance = long.MaxValue;
        private long closestPairsCount = 0;

        private Point[] getPoints(int n, long range, long seed) {
            var rands = new List<long>();
            var hashSet = new HashSet<Point>();
            for (var i = 0; i < 3 * n; ++i) {
                seed = (seed * 16807) % int.MaxValue;
                rands.Add(seed % (2 * range) - range);
                if (rands.Count == 3) {
                    hashSet.Add(new Point(rands.ToArray()));
                    rands.Clear();
                }
            }
            return hashSet.ToArray();
        }

        private void search(KdTree tree, Point target) {
            if (tree == null)
                return;
            relax(target, tree.Point);
            var axis = tree.Axis;
            if (target[axis] < tree[axis]) {
                search(tree.LeChild, target);
                var distance = target[axis] - tree[axis];
                if (distance * distance <= closestDistance) {
                    search(tree.RiChild, target);
                }
            }
            else {
                search(tree.RiChild, target);
                var distance = target[axis] - tree[axis];
                if (distance * distance <= closestDistance) {
                    search(tree.LeChild, target);
                }
            }
        }

        private void relax(Point target, Point point) {
            var distance = target.Distance(point);
            if (distance != 0) {
                if (distance <= closestDistance) {
                    if (distance < closestDistance) {
                        closestPairsCount = 0;
                    }
                    closestDistance = distance;
                    ++closestPairsCount;
                }
            }
        }

        private static KdTree makeBy(Point[] points, int axis, int lo, int hi) {
            if (lo <= hi) {
                Array.Sort(points, lo, hi - lo + 1, new PointComparer(axis));
                var pivot = (lo + hi) / 2;
                return new KdTree(axis, points[pivot],
                    makeBy(points, axis + 1, lo, pivot - 1),
                    makeBy(points, axis + 1, pivot + 1, hi));
            }
            return null;
        }

        private class KdTree {
            public readonly int Axis;
            public readonly Point Point;
            public readonly KdTree LeChild;
            public readonly KdTree RiChild;

            public KdTree(int axis, Point point, KdTree leChild, KdTree riChild) {
                Axis = axis;
                Point = point;
                LeChild = leChild;
                RiChild = riChild;
            }

            public long this[int axis] {
                get {
                    return Point[axis % 3];
                }
            }
        }

        private struct Point {
            private readonly long[] _coord;

            public Point(long[] coord) {
                _coord = coord;
            }

            public long this[int axis] {
                get {
                    return _coord[axis % 3];
                }
            }

            public override bool Equals(object obj) {
                var other = (Point)obj;
                return _coord[0] == other._coord[0] &&
                       _coord[1] == other._coord[1] &&
                       _coord[2] == other._coord[2];
            }
            
            public override int GetHashCode() {
                return ToString().GetHashCode();
            }

            public override string ToString() {
                return string.Format("{0},{1},{2}", _coord[0], _coord[1], _coord[2]);
            }
            
            public long Distance(Point p) {
                return (this[0] - p[0]) * (this[0] - p[0]) +
                       (this[1] - p[1]) * (this[1] - p[1]) +
                       (this[2] - p[2]) * (this[2] - p[2]);
            }
        }

        private struct PointComparer : IComparer<Point> {
            private readonly int _axis;

            public PointComparer(int axis) {
                _axis = axis;
            }

            public int Compare(Point a, Point b) {
                return a[_axis].CompareTo(b[_axis]);
            }
        }
    }
}