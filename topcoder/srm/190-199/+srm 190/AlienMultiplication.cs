using System;

namespace TopCoder.Algorithm {
    public class AlienMultiplication {
        public int getCorrections(int a, int b, int c) {
            var result = 6;
            getCorrections(a, b, c, 0, 0, ref result);
            return result;
        }

        private static void getCorrections(int a, int b, int c, int pos, int curr, ref int best) {
            if (lst(a, b) == c) {
                best = Math.Min(best, curr);
                return;
            }
            if (curr + 1 < best && pos < 6) {
                var ad = get(a, pos);
                var bd = get(b, pos);
                var cd = get(c, pos);
                for (var da = 0; da < 10; ++da)
                    for (var db = 0; db < 10; ++db) {
                        a = set(a, pos, da);
                        b = set(b, pos, db);
                        var dc = get(lst(a, b), pos);
                        c = set(c, pos, dc);
                        var errors = 0;
                        if (ad != da) ++errors;
                        if (bd != db) ++errors;
                        if (cd != dc) ++errors;
                        getCorrections(a, b, c, pos + 1, curr + errors, ref best);
                    }
            }
        }

        private static int get(int x, int pos) {
            return x / pow10[pos] % 10;
        }

        private static int set(int x, int pos, int d) {
            return x - (get(x, pos) - d) * pow10[pos];
        }

        private static int lst(long a, long b) {
            return (int)(a * b % pow10[6]);
        }

        private static readonly int[] pow10 = {
            1, 10, 100, 1000, 10000, 100000, 1000000
        };
    }
}