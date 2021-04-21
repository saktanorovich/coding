using System;

namespace interview.hackerrank {
    public class Merge2ArraysIn1Array {
        public int[] mergeArrays(int[] a, int[] b, int M) {
            for (var i = 0; i < M; ++i) {
                b[M + i] = b[i];
            }
            int ia = 0, ib = M, ix = 0;
            while (ia < M && ib < 2 * M) {
                if (a[ia] <= b[ib]) {
                    b[ix] = a[ia];
                    ++ia;
                }
                else {
                    b[ix] = b[ib];
                    ++ib;
                }
                ++ix;
            }
            while (ia < M) {
                b[ix] = a[ia];
                ++ia;
                ++ix;
            }
            return b;
        }
    }
}
