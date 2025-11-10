using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bsuir.algorithm {
    public class Problem_14 {
        public bool process(int testCase, InputReader reader, OutputWriter writer) {
            var n = reader.NextInt();
            var m = reader.NextInt();
            var u = reader.NextInt() - 1;
            var v = reader.NextInt() - 1;
            var k = reader.NextInt();
            var g = new long[n, n];
            for (var i = 0; i < m; ++i) {
                var a = reader.NextInt() - 1;
                var b = reader.NextInt() - 1;
                ++g[a, b];
                ++g[b, a];
            }
            g = pow(g, n, k);
            writer.WriteLine(g[u, v]);
            return true;
        }

        private long[,] pow(long[,] a, int n, int k) {
            if (k == 0) {
                var c = new long[n, n];
                for (var i = 0; i < n; ++i) {
                    c[i, i] = 1;
                }
                return c;
            } else if (k % 2 == 0) {
                return pow(mul(a, a, n), n, k / 2);
            } else {
                return mul(a, pow(a, n, k - 1), n);
            }
        }

        private long[,] mul(long[,] a, long[,] b, int n) {
            var c = new long[n, n];
            for (var i = 0; i < n; ++i) {
                for (var j = 0; j < n; ++j) {
                    for (var k = 0; k < n; ++k) {
                        c[i, j] += a[i, k] * b[k, j];
                        c[i, j] %= MOD;
                    }
                }
            }
            return c;
        }

        private readonly long MOD = (long)1e9 + 7;
    }
}
