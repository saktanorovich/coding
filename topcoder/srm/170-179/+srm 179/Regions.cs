using System;

namespace TopCoder.Algorithm {
    public class Regions {
        public int numTaxes(int[] row, int[] col) {
            var result = 0L;
            for (var i = 0; i + 1 < row.Length; ++i) {
                result += numTaxes(row[i], col[i], row[i + 1], col[i + 1]);
            }
            if (result > (int)2e9) {
                return -1;
            }
            return (int)result;
        }

        private static long numTaxes(long x1, long y1, long x2, long y2) {
            var result = 0L;
            if (x1 != x2 || y1 != y2) {
                var dx = Math.Abs(x2 - x1);
                var dy = Math.Abs(y2 - y1);
                result += dx;
                result += dy;
                var dk = gcd(dx, dy);
                dx /= dk;
                dy /= dk;
                if (dx % 2 == 1 && dy % 2 == 1) {
                    result -= dk;
                }
            }
            return result;
        }

        private static long gcd(long a, long b) {
            while (a != 0 && b != 0) {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }
            return a + b;
        }
    }
}