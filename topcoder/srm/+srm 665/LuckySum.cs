using System;

namespace TopCoder.Algorithm {
    public class LuckySum {
        public long construct(string note) {
            var digits = new int[note.Length];
            for (var i = 0; i < note.Length; ++i) {
                digits[note.Length - 1 - i] = "?0123456789".IndexOf(note[i]) - 1;
            }
            return construct(digits);
        }

        private static long construct(int[] digits) {
            /* from 4+4=8, 4+7=11, 7+7=14 we can build the following function f[at][i1][i2]
             * for the first at digits where at the at we have digits a[i1] and a[i2].. */
            var a = new[] { 4, 7, 0 };
            var f = new long[digits.Length + 1, 3, 3];
            for (var at = 0; at <= digits.Length; ++at) {
                for (var i1 = 0; i1 < 3; ++i1) {
                    for (var i2 = 0; i2 < 3; ++i2) {
                        f[at, i1, i2] = long.MaxValue;
                    }
                }
            }
            f[0, 0, 0] = 0;
            for (long at = 0, pow10 = 1; at < digits.Length; ++at, pow10 *= 10) {
                for (var i1 = 0; i1 < 3; ++i1) {
                    for (var i2 = 0; i2 < 3; ++i2) {
                        if (f[at, i1, i2] == long.MaxValue) {
                            continue;
                        }
                        for (var k1 = 0; k1 < 3; ++k1) {
                            for (var k2 = 0; k2 < 3; ++k2) {
                                if (at == 0 && k1 == 2) continue;
                                if (at == 0 && k2 == 2) continue;
                                if (i1 == 2 && k1 != 2) continue;
                                if (i2 == 2 && k2 != 2) continue;

                                var d = (a[k1] + a[k2] + (a[i1] + a[i2]) / 10) % 10;
                                if (d == digits[at] || digits[at] == -1) {
                                    if (at + 1 < digits.Length || d != 0) {
                                        if (f[at + 1, k1, k2] > d * pow10 + f[at, i1, i2]) {
                                            f[at + 1, k1, k2] = d * pow10 + f[at, i1, i2];
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            var result = long.MaxValue;
            for (var i1 = 0; i1 < 3; ++i1) {
                for (var i2 = 0; i2 < 3; ++i2) {
                    if (a[i1] + a[i2] < 9) {
                        result = Math.Min(result, f[digits.Length, i1, i2]);
                    }
                }
            }
            return result < long.MaxValue ? result : -1;
        }
    }
}