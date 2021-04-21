using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopCoder.Algorithm {
    public class Amoebae {
        public int cultureX(string[] known, string[] candidate) {
            return cultureX(Array.ConvertAll(known, s => s.ToCharArray()), Array.ConvertAll(candidate, s => s.ToCharArray()), known.Length, known[0].Length);
        }

        private static int cultureX(char[][] known, char[][] candidate, int n, int m) {
            return cultureX(getAmoebaeBoxes(known, n, m), getAmoebaeBoxes(candidate, n, m));
        }

        private static int cultureX(IList<AmoebaeBox> a, IList<AmoebaeBox> b) {
            var aset = new List<string>(a.Select(x => x.Normalize()));
            var bset = new List<string>(b.Select(x => x.Normalize()));
            foreach (var amoebae in aset.ToArray()) {
                if (bset.Contains(amoebae)) {
                    aset.Remove(amoebae);
                    bset.Remove(amoebae);
                }
            }
            return aset.Count + bset.Count;
        }

        private static IList<AmoebaeBox> getAmoebaeBoxes(char[][] known, int n, int m) {
            var result = new List<AmoebaeBox>();
            for (int i = 0, seqno = 0; i < n; ++i)
                for (int j = 0; j < m; ++j)
                    if (known[i][j] == 'X') {
                        int xmin = i, ymin = j;
                        int xmax = i, ymax = j;
                        markit(known, n, m, seqno, ref xmin, ref ymin, ref xmax, ref ymax, i, j);
                        var amoebaeBox = new AmoebaeBox(xmin, ymin, xmax, ymax);
                        for (var x = xmin; x <= xmax; ++x)
                            for (var y = ymin; y <= ymax; ++y) {
                                amoebaeBox[x - xmin, y - ymin] = '.';
                                if (known[x][y] == (char)('Z' + seqno)) {
                                    amoebaeBox[x - xmin, y - ymin] = 'X';
                                }
                            }
                        result.Add(amoebaeBox);
                        seqno = seqno + 1;
                    }
            return result;
        }

        private static void markit(char[][] known, int n, int m, int seqno, ref int xmin, ref int ymin, ref int xmax, ref int ymax, int x, int y) {
            if (0 <= x && x < n && 0 <= y && y < m) {
                if (known[x][y] == 'X') {
                    xmin = Math.Min(xmin, x);
                    ymin = Math.Min(ymin, y);
                    xmax = Math.Max(xmax, x);
                    ymax = Math.Max(ymax, y);
                    known[x][y] = (char)('Z' + seqno);
                    markit(known, n, m, seqno, ref xmin, ref ymin, ref xmax, ref ymax, x - 1, y);
                    markit(known, n, m, seqno, ref xmin, ref ymin, ref xmax, ref ymax, x, y - 1);
                    markit(known, n, m, seqno, ref xmin, ref ymin, ref xmax, ref ymax, x + 1, y);
                    markit(known, n, m, seqno, ref xmin, ref ymin, ref xmax, ref ymax, x, y + 1);
                }
            }
        }

        private class AmoebaeBox {
            private readonly char[][] _box;

            public AmoebaeBox(int xmin, int ymin, int xmax, int ymax) {
                var n = xmax - xmin + 1;
                var m = ymax - ymin + 1;
                _box = new char[n][];
                for (var i = 0; i < n; ++i) {
                    _box[i] = new char[m];
                }
            }

            public char this[int i, int j] {
                get { return _box[i][j]; }
                set { _box[i][j] = value; }
            }

            public override string ToString() {
                var stringBuilder = new StringBuilder();
                foreach (var row in _box) {
                    stringBuilder.Append('(');
                    stringBuilder.Append(new string(row));
                    stringBuilder.Append(')');
                }
                return stringBuilder.ToString();
            }

            public IEnumerable<AmoebaeBox> Clone() {
                var result = new List<AmoebaeBox>();
                var obj = this;
                for (var i = 0; i < 4; ++i) {
                    result.Add(obj);
                    result.Add(obj.Reflect());
                    obj = obj.Rotate();
                }
                return result;
            }

            public string Normalize() {
                return Clone().OrderBy(x => x.ToString()).First().ToString();
            }

            private AmoebaeBox Rotate() {
                var result = new AmoebaeBox(0, 0, _box[0].Length - 1, _box.Length - 1);
                for (var i = 0; i < _box.Length; ++i)
                    for (var j = 0; j < _box[0].Length; ++j) {
                        result[j, i] = this[i, j];
                    }
                return result.Reflect();
            }

            private AmoebaeBox Reflect() {
                var result = new AmoebaeBox(0, 0, _box.Length - 1, _box[0].Length - 1);
                for (var i = 0; i < _box.Length; ++i)
                    for (var j = 0; j < _box[0].Length; ++j) {
                        result[i, j] = this[_box.Length - 1 - i, j];
                    }
                return result;
            }
        }
    }
}