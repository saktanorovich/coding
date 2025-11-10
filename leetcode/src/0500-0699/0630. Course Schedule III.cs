using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0630 {
        public int ScheduleCourse(int[][] courses) {
            var c = new Course[courses.Length];
            for (var i = 0; i < courses.Length; ++i) {
                c[i] = new Course {
                    t = courses[i][0],
                    d = courses[i][1]
                };
            }
            Array.Sort(c, (a, b) => a.d - b.d);
            if (c.Length < 1) {
                return small(c, c.Length);
            } else {
                return large(c, c.Length);
            }
        }

        private int large(Course[] c, int n) {
            var heap = new SortedSet<int>(
                Comparer<int>.Create((a, b) => {
                    if (c[a].t != c[b].t) {
                        return c[a].t - c[b].t;
                    }
                    return a - b;
            }));
            var time = 0;
            for (var i = 0; i < n; ++i) {
                if (time + c[i].t <= c[i].d) {
                    time = c[i].t + time;
                    heap.Add(i);
                } else {
                    var tmax = c[heap.Max].t;
                    if (tmax > c[i].t) {
                        heap.Remove(heap.Max);
                        heap.Add(i);
                        time -= tmax;
                        time += c[i].t;
                    }
                }
            }
            return heap.Count;
        }

        private int small(Course[] c, int n) {
            var maxd = c.Last().d;
            var best = new int[maxd + 1];
            for (var t = 1; t <= maxd; ++t) {
                best[t] = -1;
            }
            for (var i = 0; i < n; ++i) {
                for (var t = c[i].d - c[i].t; t >= 0; --t) {
                    if (best[t] >= 0) {
                        if (best[t + c[i].t] < best[t] + 1) {
                            best[t + c[i].t] = best[t] + 1;
                        }
                    }
                }
            }
            return best.Max();
        }

        private class Course {
            public int t;
            public int d;
        }
    }
}
