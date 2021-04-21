using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class SetOfBoxes {
        public double countThrow(string[] boxes, int inBox) {
            var x = new int[boxes.Length][];
            var y = new int[boxes.Length][];
            var s = new int[boxes.Length];
            for (var i = 0; i < boxes.Length; ++i) {
                var items = boxes[i].Split(' ', '.');
                x[i] = new int[3];
                y[i] = new int[3];
                x[i][0] = int.Parse(items[0]);
                y[i][0] = int.Parse(items[1]);
                x[i][1] = int.Parse(items[2]);
                y[i][1] = int.Parse(items[3]);
                x[i][2] = int.Parse(items[4]);
                y[i][2] = int.Parse(items[5]);
                s[i] = square(x[i], y[i]);
            }
            for (var i = 0; i < boxes.Length; ++i) {
                for (var j = i + 1; j < boxes.Length; ++j) {
                    if (s[j] < s[i]) {
                        swap(ref s[i], ref s[j]);
                        swap(ref x[i], ref x[j]);
                        swap(ref y[i], ref y[j]);
                    }
                }
            }
            var parent = new int[boxes.Length].Select(i => -1).ToArray();
            var height = new int[boxes.Length].Select(i => +1).ToArray();
            for (var i = 0; i < boxes.Length; ++i) {
                for (var j = i + 1; j < boxes.Length; ++j) {
                    var inside = true;
                    for (int a = 0, b = 1, c = 2; a < 3; ++a) {
                        var r1 = vector(x[i][0] - x[j][a], y[i][0] - y[j][a], x[j][b] - x[j][a], y[j][b] - y[j][a]);
                        var r2 = vector(x[i][0] - x[j][a], y[i][0] - y[j][a], x[j][c] - x[j][a], y[j][c] - y[j][a]);
                        if (r1 * r2 >= 0) {
                            inside = false;
                            break;
                        }
                        b = (b + 1) % 3;
                        c = (c + 1) % 3;
                    }
                    if (inside) {
                        parent[i] = j;
                        break;
                    }
                }
            }
            for (var i = boxes.Length - 1; i >= 0; --i) {
                if (parent[i] != -1) {
                    height[i] = height[parent[i]] + 1;
                }
            }
            if (inBox <= boxes.Length) {
                var prob = new[] { inBox == 0 ? 1e+4 : 0, 0 };
                for (var i = 0; i < boxes.Length; ++i) {
                    for (var d = 0; d < 2; ++d) {
                        if (height[i] == inBox + d) {
                            prob[d] += s[i] / 2.0;
                        }
                    }
                }
                return (prob[0] - prob[1]) / 1e+4;
            }
            return 0;
        }

        private static int square(int[] x, int[] y) {
            var x1 = x[1] - x[0];
            var y1 = y[1] - y[0];
            var x2 = x[2] - x[0];
            var y2 = y[2] - y[0];
            var sq = vector(x1, y1, x2, y2);
            return Math.Abs(sq);
        }

        private static int vector(int x1, int y1, int x2, int y2) {
            return x1 * y2 - x2 * y1;
        }

        private static void swap<T>(ref T x, ref T y) {
            var t = x;
            x = y;
            y = t;
        }
    }
}
