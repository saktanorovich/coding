using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bsuir.algorithm {
    public class Problem_06 {
        public bool process(int testCase, InputReader reader, OutputWriter writer) {
            var n = reader.NextInt();
            var m = reader.NextInt();
            var l = new int[m + 1];
            var r = new int[m + 1];
            var v = new int[m + 1];
            var e = new List<int>[n + 1];
            for (var i = 1; i <= n; ++i) {
                e[i] = new List<int>();
            }
            for (var i = 1; i <= m; ++i) {
                l[i] = reader.NextInt();
                r[i] = reader.NextInt();
                v[i] = reader.NextInt();
                e[l[i]].Add(+i);
                e[r[i]].Add(-i);
            }
            var T = new SortedSet<int>(Comparer<int>.Create(((a, b) => {
                if (v[a] != v[b]) {
                    return v[b] - v[a];
                } else if (l[a] != l[b]) {
                    return l[a] - l[b];
                } else if (r[a] != r[b]) {
                    return r[a] - r[b];
                } else {
                    return a - b;
                }
            })));
            T.Add(0);
            for (int i = 1, x = 0; i <= n; ++i) {
                foreach (var t in e[i]) {
                    if (t > 0) {
                        T.Add(+t);
                    }
                }
                x = Math.Max(x, v[T.First()]);
                writer.Write(x);
                if (i + 1 <= n) {
                    writer.Write(' ');
                }
                foreach (var t in e[i]) {
                    if (t < 0) {
                        T.Remove(-t);
                    }
                }
                x = Math.Min(x, v[T.First()]);
            }
            writer.WriteLine();
            return true;
        }
    }
}
