using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1610 {
        public int VisiblePoints(IList<IList<int>> ps, int angle, IList<int> origin) {
            var points = new List<IList<int>>();
            var result = 0;
            foreach (var p in ps) {
                if (p[0] == origin[0] && p[1] == origin[1]) {
                    result++;
                    continue;
                }
                points.Add(p);
            }
            return result + count(points, origin, angle);
        }

        private int count(List<IList<int>> points, IList<int> origin, int angle) {
            var angles = new List<double>();
            foreach (var p in points) {
                var a = ang(p[0] - origin[0], p[1] - origin[1]);
                angles.Add(a);
                angles.Add(a + 360);
            }
            angles.Sort();
            var window = new Window(angle);
            var result = 0;
            foreach (var a in angles) {
                result = Math.Max(result, window.add(a));
            }
            return result;
        }

        private static double ang(int dx, int dy) {
            var phi = Math.Atan2(dy, dx);
            if (phi < 0) {
                phi = 2 * Math.PI + phi;
            }
            return phi * 180 / Math.PI;
        }

        private class Window {
            private readonly Queue<double> queue;
            private readonly int angle;

            public Window(int angle) {
                this.queue = new Queue<double>();
                this.angle = angle;
            }

            public int add(double a) {
                while (queue.Count > 0) {
                    if (a - queue.Peek() > angle) {
                        queue.Dequeue();
                    }
                    else break;
                }
                queue.Enqueue(a);
                return queue.Count;
            }
        }
    }
}
