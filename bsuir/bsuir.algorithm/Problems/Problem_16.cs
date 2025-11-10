using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bsuir.algorithm {
    public class Problem_16 {
        public bool process(int testCase, InputReader reader, OutputWriter writer) {
            var x = reader.NextInt();
            var a = digitize(x);
            if (next(a)) {
                writer.WriteLine(String.Join("", a));
            } else {
                writer.WriteLine(-1);
            }
            return true;
        }

        private bool next(int[] a) {
            for (var i = a.Length - 2; i >= 0; --i) {
                if (a[i] < a[i + 1]) {
                    for (var j = a.Length - 1; j > i; --j) {
                        if (a[j] > a[i]) {
                            var t = a[i];
                            a[i] = a[j];
                            a[j] = t;
                            for (int x = i + 1, y = a.Length - 1; x < y; ++x, --y) {
                                t = a[x];
                                a[x] = a[y];
                                a[y] = t;
                            }
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private int[] digitize(int x) {
            var d = new List<int>();
            for (; x > 0; x /= 10) {
                d.Add(x % 10);
            }
            return Enumerable.Reverse(d).ToArray();
        }
    }
}
