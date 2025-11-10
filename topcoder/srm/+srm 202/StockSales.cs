using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopCoder.Algorithm {
    public class StockSales {
        public int[] getAmounts(int[] values) {
            /**
             * By Bezout's identity for any integers (a1,.., an) there are integers (x1,.., xn) such that
             *   d = gcd(a1, .., an) = a1 * x1 + .. an * xn
             * with the following properties:
             *   (*) d is the smallest positive integer of this form;
             *   (*) every number of this form is a multiple of d.
             * In order to minimize value of the first term lets rewrite gcd(a1,.., an) as
             *   gcd(a1, .., an) = gcd(a1, gcd(a2, ..., gcd(an-1, an)..)).
             */
            if (values.Length > 1) {
                return getAmounts(values, values.Length);
            }
            return new[] { 1 };
        }

        private static int[] getAmounts(int[] a, int n) {
            var g = (int[])a.Clone();
            for (var i = n - 2; i >= 0; --i) {
                g[i] = gcd(a[i], g[i + 1]);
            }
            var revenue = 1L * g[0];
            var res = new int[n];
            for (var i = 0; i + 1 < n; ++i) {
                for (var take = 0;; ++take) {
                    if (check(a[i], +take, g[i + 1], ref revenue)) {
                        res[i] = +take;
                        break;
                    }
                    if (check(a[i], -take, g[i + 1], ref revenue)) {
                        res[i] = -take;
                        break;
                    }
                }
            }
            res[n - 1] = (int)(revenue / a[n - 1]);
            return res;
        }

        private static bool check(long a, long x, long b, ref long d) {
            if ((d - a * x) % b == 0) {
                d -= a * x;
                return true;
            }
            return false;
        }

        private static int gcd(int a, int b) {
            while (a != 0 && b != 0) {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }
            return a + b;
        }
        /*
        private static int gcd(int a, int b, out int x, out int y) {
            if (b == 0) {
                x = 1;
                y = 0;
                return a;
            }
            int p, q;
            var result = gcd(b, a % b, out p, out q);
            x = q;
            y = p - (a / b) * q;
            return result;
        }
        */
    }
}
