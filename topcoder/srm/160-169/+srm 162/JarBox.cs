using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TopCoder.Algorithm {
    public class JarBox {
        public int numJars(int radius, int woodlength) {
            int result = 1;
            for (int gap = 0; gap <= 1; ++gap) {
                for (int inrow = 1; true; ++inrow) {
                    double wi = 2 * radius * inrow + gap * radius;
                    if (wi <= woodlength) {
                        for (int numRows = 1; true; ++numRows) {
                            double hi = height(radius, numRows);
                            if (MathUtils.sign(2 * wi + 2 * hi - woodlength) <= 0) {
                                int odd = inrow;
                                int eve = inrow - 1 + gap;
                                int cnt = 0;
                                cnt += ((numRows + 1) / 2) * odd;
                                cnt += ((numRows + 0) / 2) * eve;
                                result = Math.Max(result, cnt);
                            }
                            else break;
                        }
                    }
                    else break;
                }
            }
            return result;
        }

        private double height(int radius, int numRows) {
            return radius * (2 * numRows - (numRows - 1) * (2 - Math.Sqrt(3)));
        }

        private static class MathUtils {
            public static readonly double eps = 1e-6;

            public static int sign(double x) {
                if (x + eps < 0) {
                    return -1;
                }
                if (x - eps > 0) {
                    return +1;
                }
                return 0;
            }
        }
    }
}