using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bsuir.algorithm {
    public class Problem_03 {
        public bool process(int testCase, InputReader reader, OutputWriter writer) {
            var n = reader.NextInt();
            var k = reader.NextInt();
            var a = new int[n];
            for (var i = 0; i < n; ++i) {
                a[i] = reader.NextInt();
            }
            Array.Sort(a, (x, y) => {
                return Math.Abs(y) - Math.Abs(x);
            });
            var ans = prod(a.Take(k));
            var sgn = sign(a.Take(k));
            if (sgn < 0) {
                if (a.Max() <= 0) {
                    ans = prod(a.Skip(n - k));
                } else {
                    ans = best(a, n, k);
                }
            }
            writer.WriteLine(ans);
            return true;
        }

        private long best(IList<int> a, int n, int k) {
            int x1, y1;
            int x2, y2;
            find(a, n, k, x => x >= 0, out x1, out y1);
            find(a, n, k, x => x <= 0, out x2, out y2);
            if (x2 >= 0) {
                if (1L * a[y2] * a[x1] > 1L * a[y1] * a[x2]) {
                    x1 = x2;
                    y1 = y2;
                }
            }
            var ans = 1L * a[y1];
            for (var i = 0; i < k; ++i) {
                if (x1 != i) {
                    ans *= a[i];
                    ans %= MOD;
                    ans += MOD;
                    ans %= MOD;
                }
            }
            return ans;
        }

        private void find(IList<int> a, int n, int k, Predicate<int> f, out int x, out int y) {
            x = k - 1;
            y = k;
            while (x >= 0 && f(a[x])) {
                x = x - 1;
            }
            while (y < n && !f(a[y])) {
                y = y + 1;
            }
            if (y == n) {
                y = x;
            }
        }

        private long prod(IEnumerable<int> a) {
            var ans = 1L;
            foreach (var x in a) {
                ans *= x;
                ans %= MOD;
                ans += MOD;
                ans %= MOD;
            }
            return ans;
        }

        private long sign(IEnumerable<int> a) {
            var ans = 1;
            foreach (var x in a) {
                ans *= Math.Sign(x);
            }
            return ans;
        }

        private readonly long MOD = (int)1e9 + 7;
    }
}
