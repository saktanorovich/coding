using System;

namespace TopCoder.Algorithm {
    public class TennisSet {
        public string firstSet(string[] points) {
            return firstSet(string.Concat(points), 0, 1, new int[2], new int[2]);
        }

        private static string firstSet(string points, int a, int b, int[] pts, int[] won) {
            if (completed(pts[a], pts[b], 4)) {
                if (pts[a] > pts[b])
                    ++won[a];
                else
                    ++won[b];
                return firstSet(points, b, a, new int[2], won);
            }
            if (completed(won[a], won[b], 6)) {
                return toString(won);
            }
            if (points.Length > 0) {
                if (points[0] == 'S')
                    ++pts[a];
                else
                    ++pts[b];
                return firstSet(points.Substring(1), a, b, pts, won);
            }
            return toString(won);
        }

        private static bool completed(int a, int b, int t) {
            if (a >= b) {
                return a >= t && a - b >= 2;
            }
            return completed(b, a, t);
        }

        private static string toString(int[] won) {
            return string.Format("{0}-{1}", won[0], won[1]);
        }
    }
}