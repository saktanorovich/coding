using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopCoder.Algorithm {
    public class Untypeset {
        public int evaluate(string[] expression) {
            return evaluate(expression.Select(expr => expr.ToCharArray()).ToArray());
        }

        private static int evaluate(char[][] a) {
            a = trim(a);
            var sumIdx = sumIndex(a);
            if (sumIdx >= 0) {
                var le = new char[a.Length][];
                var ri = new char[a.Length][];
                for (var r = 0; r < a.Length; ++r) {
                    le[r] = a[r].Take(sumIdx).ToArray();
                    ri[r] = a[r].Skip(sumIdx + 1).ToArray();
                }
                return evaluate(le) + evaluate(ri);
            }
            var divIdx = divIndex(a);
            if (divIdx >= 0) {
                var lo = a.Take(divIdx).ToArray();
                var hi = a.Skip(divIdx + 1).ToArray();
                return evaluate(lo) / evaluate(hi);
            }
            return int.Parse(new string(a[0]));
        }

        private static int sumIndex(char[][] a) {
            for (var c = 0; c < a[0].Length; ++c) {
                var str = new List<char>();
                for (var r = 0; r < a.Length; ++r) {
                    str.Add(a[r][c]);
                }
                if (new string(str.ToArray()).Trim() == "+") {
                    return c;
                }
            }
            return -1;
        }

        private static int divIndex(char[][] a) {
            var div = "".PadLeft(a[0].Length, '-');
            for (var r = 0; r < a.Length; ++r) {
                if (new string(a[r]) == div) {
                    return r;
                }
            }
            return -1;
        }

        private static char[][] trim(char[][] a) {
            var s = a.Select(x => new string(x));
            s = s.SkipWhile(x => x.Trim() == String.Empty); s = s.Reverse();
            s = s.SkipWhile(x => x.Trim() == String.Empty); s = s.Reverse();
            while (s.All(x => x.StartsWith(" "))) {
                s = s.Select(x => x.Substring(1, x.Length - 1));
            }
            while (s.All(x => x.EndsWith(" "))) {
                s = s.Select(x => x.Substring(0, x.Length - 1));
            }
            return s.Select(x => x.ToCharArray()).ToArray();
        }
    }
}
