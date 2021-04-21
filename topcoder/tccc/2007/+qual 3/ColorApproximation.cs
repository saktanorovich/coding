using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class ColorApproximation {
        public string bestApproximation(string[] colors) {
            var clr = getCodes(colors);
            var min = new[] { int.MaxValue, int.MaxValue, int.MaxValue };
            var max = new[] { int.MinValue, int.MinValue, int.MinValue };
            for (var i = 0; i < clr.Count; ++i)
                for (var j = 0; j < 3; ++j) {
                    min[j] = Math.Min(clr[i][j], min[j]);
                    max[j] = Math.Max(clr[i][j], max[j]);
                }
            var answ = new int[3];
            var best = int.MaxValue;
            for (var r = 0; r < 256; ++r)
                for (var g = 0; g < 256; ++g)
                    for (var b = 0; b < 256; ++b) {
                        var distR = Math.Max(Math.Abs(r - min[0]), Math.Abs(r - max[0]));
                        var distG = Math.Max(Math.Abs(g - min[1]), Math.Abs(g - max[1]));
                        var distB = Math.Max(Math.Abs(b - min[2]), Math.Abs(b - max[2]));
                        var dist = 0;
                        dist = Math.Max(dist, distR);
                        dist = Math.Max(dist, distG);
                        dist = Math.Max(dist, distB);
                        if (best > dist) {
                            best = dist;
                            answ[0] = r;
                            answ[1] = g;
                            answ[2] = b;
                        }
                    }
            var result = string.Empty;
            for (var i = 0; i < 3; ++i) {
                result += toHex(answ[i]);
            }
            return result;
        }

        private List<int[]> getCodes(string[] colors) {
            var result = new List<int[]>();
            for (var i = 0; i < colors.Length; ++i) {
                var c = colors[i].Split(' ');
                for (var j = 0; j < c.Length; ++j) {
                    var what = new int[3];
                    for (var k = 0; k < 3; ++k) {
                        what[k] = int.Parse(c[j].Substring(2 * k, 2), System.Globalization.NumberStyles.HexNumber);
                    }
                    result.Add(what);
                }
            }
            return result;
        }

        private string toHex(int x) {
            return "0123456789ABCDEF"[x / 16].ToString() +
                   "0123456789ABCDEF"[x % 16].ToString();
        }
    }
}
