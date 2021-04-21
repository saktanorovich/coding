using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bsuir.algorithm {
    public class Problem_15 {
        public bool process(int testCase, InputReader reader, OutputWriter writer) {
            this.n = reader.NextInt();
            this.a = new int[n];
            for (var i = 0; i < n; ++i) {
                a[i] = reader.NextInt();
            }
            var l = build(n - 1, 0, -1);
            var r = build(0, n - 1, +1);
            var z = build();
            var res = 0L;
            for (var i = 0; i < n; ++i) {
                if (z[i] != i) {
                    continue;
                }
                if (l[i] != -1 && r[i] != -1) {
                    res += Math.Min(l[i], r[i]) - a[i];
                } else if (l[i] != -1) {
                    res += l[i] - a[i];
                } else if (r[i] != -1) {
                    res += r[i] - a[i];
                }
            }
            writer.WriteLine(res);
            return true;
        }

        private int[] build(int lo, int hi, int dx) {
            var b = new int[n + 1];
            var s = new Stack<int>();
            for (var i = lo; i != hi + dx; i += dx) {
                while (s.Count > 0) {
                    if (a[s.Peek()] < a[i]) {
                        b[s.Pop()] = a[i];
                    } else {
                        break;
                    }
                }
                s.Push(i);
            }
            while (s.Count > 0) {
                b[s.Pop()] = -1;
            }
            return b;
        }

        private int[] build() {
            var z = new int[n];
            var s = new Stack<int>();
            for (var i = 0; i < n; ++i) {
                z[i] = i;
                while (s.Count > 0) {
                    if (a[s.Peek()] <= a[i]) {
                        var x = s.Pop();
                        if (a[x] == a[i]) {
                            z[x] = i;
                        }
                    } else {
                        break;
                    }
                }
                s.Push(i);
            }
            return z;
        }

        private int[] a;
        private int n;
    }
}
