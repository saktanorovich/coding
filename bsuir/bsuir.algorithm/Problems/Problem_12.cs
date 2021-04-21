using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bsuir.algorithm {
    public class Problem_12 {
        public bool process(int testCase, InputReader reader, OutputWriter writer) {
            var x = reader.NextLong();
            int d;
            if (x < 100) {
                d = doit(x);
            } else {
                d = (int)(x % 10);
            }
            if (d > 0) {
                writer.WriteLine(d);
            } else {
                writer.WriteLine("NO");
            }
            return true;
        }

        private int doit(long x) {
            var d = 0;
            if (x > 0) {
                foreach (var c in x.ToString()) {
                    var k = c - '0';
                    if (k > d) {
                        var s = -doit(x - k);
                        if (s == 0) {
                            d = Math.Max(d, k);
                        }
                    }
                }
            }
            return d;
        }
    }
}
