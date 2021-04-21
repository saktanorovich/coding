using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1453 {
        public int NumPoints(int[][] darts, int r) {
            if (darts.Length < 2) {
                return darts.Length;
            }
            if (darts.Length < 2) {
                return new Small().Solve(darts, r);
            } else {
                return new Large().Solve(darts, r);
            }
        }

        private class Small {
            public int Solve(int[][] darts, int r) {
                var res = 1;
                for (var i = 0; i < darts.Length; ++i) {
                    for (var j = i + 1; j < darts.Length; ++j) {
                        var x1 = darts[i][0];
                        var y1 = darts[i][1];
                        var x2 = darts[j][0];
                        var y2 = darts[j][1];
                        var x = x2 - x1;
                        var y = y2 - y1;
                        if (sign(x * x + y * y - 4 * r * r) > 0) {
                            continue;
                        }
                        var mx = (x1 + x2) / 2.0;
                        var my = (y1 + y2) / 2.0;
                        var d = Math.Sqrt(x * x + y * y);
                        var h = Math.Sqrt(r * r - d * d / 4);
                        var sin = (y2 - y1) / d;
                        var cos = (x2 - x1) / d;
                        var c1 = new double[] {
                            mx - h * sin,
                            my + h * cos
                        };
                        var c2 = new double[] {
                            mx + h * sin,
                            my - h * cos
                        };
                        res = Math.Max(res, count(darts, c1, r));
                        res = Math.Max(res, count(darts, c2, r));
                    }
                }
                return res;
            }

            private int count(int[][] darts, double[] center, int r) {
                var res = 0;
                for (var i = 0; i < darts.Length; ++i) {
                    var x = darts[i][0] - center[0];
                    var y = darts[i][1] - center[1];
                    if (sign(r * r - x * x - y * y) >= 0) {
                        res = res + 1;
                    }
                }
                return res;
            }
        }

        private class Large {
            public int Solve(int[][] darts, int r) {
                var res = 1;
                for (var i = 0; i < darts.Length; ++i) {
                    var x1 = darts[i][0];
                    var y1 = darts[i][1];
                    var ang = new List<(double, int)>();
                    for (var j = 0; j < darts.Length; ++j) {
                        if (i == j) {
                            continue;
                        }
                        var x2 = darts[j][0];
                        var y2 = darts[j][1];
                        var x = x2 - x1;
                        var y = y2 - y1;
                        var d = Math.Sqrt(x * x + y * y);
                        if (sign(d - 2 * r) > 0) {
                            continue;
                        }
                        var rot = Math.Atan2(y, x);
                        var phi = Math.Acos(d / r * 0.5);
                        ang.Add((rot - phi, +1));
                        ang.Add((rot + phi, -1));
                    }
                    ang.Sort((a, b) => {
                        if (a.Item1.CompareTo(b.Item1) != 0) {
                            return a.Item1.CompareTo(b.Item1);
                        } else {
                            return b.Item2.CompareTo(a.Item2);
                        }
                    });
                    var cnt = 1;
                    for (var j = 0; j < ang.Count; ++j) {
                        cnt += ang[j].Item2;
                        if (res < cnt) {
                            res = cnt;
                        }
                    }
                }
                return res;
            }
        }

        private static int sign(double x) {
            if (x + 1e-9 < 0)
                return -1;
            if (x - 1e-9 > 0)
                return +1;
            return 0;
        }
    }
}