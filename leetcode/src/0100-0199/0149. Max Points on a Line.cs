using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0149 {
        public int MaxPoints(int[][] points) {
            if (points == null) {
                return 0;
            }
            var res = 0;
            for (var i = 0; i < points.Length; ++i) {
                var have = new Dictionary<Line, int>();
                var same = 1;
                for (var j = i + 1; j < points.Length; ++j) {
                    var p = points[i];
                    var q = points[j];
                    if (p[0] == q[0] && p[1] == q[1]) {
                        same = same + 1;
                        continue;
                    }
                    var L = new Line(p, q);
                    if (have.ContainsKey(L) == false) {
                        have.Add(L, 0);
                    }
                    have[L]++;
                }
                if (have.Any()) {
                    res = Math.Max(res, same + have.Values.Max());
                } else {
                    res = Math.Max(res, same);
                }
            }
            return res;
        }

        private class Line {
            public readonly int A;
            public readonly int B;
            public readonly int C;

            public Line(int[] p, int[] q) {
                A = p[1] - q[1];
                B = q[0] - p[0];
                C = p[0] * q[1] - p[1] * q[0];
                if (B < 0) {
                    A = -A;
                    B = -B;
                    C = -C;
                }
                if (A < 0) {
                    A = -A;
                    B = -B;
                    C = -C;
                }
                var D = gcd(A, gcd(B, C));
                if (D > 0) {
                    A /= D;
                    B /= D;
                    C /= D;
                }
            }

            public override bool Equals(object obj) {
                var line = (Line)obj;
                return A == line.A
                    && B == line.B
                    && C == line.C;
            }

            public override int GetHashCode() {
                unchecked {
                    var hashCode = 0;
                    hashCode = (hashCode * 397) ^ A;
                    hashCode = (hashCode * 397) ^ B;
                    hashCode = (hashCode * 397) ^ C;
                    return hashCode;
                }
            }

            private static int gcd(int a, int b) {
                a = Math.Abs(a);
                b = Math.Abs(b);
                while (a != 0 && b != 0) {
                    if (a > b) {
                        a %= b;
                    } else {
                        b %= a;
                    }
                }
                return a + b;
            }
        }
    }
}
