using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace yandex.blitz2017.qual {
    // Difficult numbers
    public class Problem02 {
        public bool process(int testCase, StreamReader reader, StreamWriter writer) {
            foreach (var x in enumerate().Take(100)) {
                writer.WriteLine(x);
            }
            return false;
        }

        private static IEnumerable<int> enumerate() {
            for (var n = 1;; ++n) {
                if (okay(n)) {
                    yield return n;
                }
            }
        }

        private static bool okay(long n) {
            for (long m = 1;; ++m) {
                var p = n * m * m;
                if (p % 3 == 0) {
                    var k = p / 3;
                    var s = S(k);
                    var d = D(k);
                    if (s == m) {
                        return false;
                    }
                    else if (9 * d < m) {
                        return true;
                    }
                }
            }
        }

        private static int S(long k) {
            var s = 0L;
            for (; k > 0; k /= 10) {
                s += k % 10;
            }
            return (int)s;
        }

        private static int D(long k) {
            var d = 0;
            for (; k > 0; k /= 10) {
                d += 1;
            }
            return d;
        }
    }
}
