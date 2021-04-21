using System;

namespace TopCoder.Algorithm {
    public class TriangleCount {
        public int count(int n) {
            var f = new int[n + 1];
            for (var k = 1; k <= n; ++k) {
                f[k] = f[k - 1];
                for (var i = 1; i <= k; ++i) {
                    f[k] += i;
                    if (i < k)
                        f[k] += Math.Min(i, k - i);
                }
            }
            return f[n];
        }
    }
}