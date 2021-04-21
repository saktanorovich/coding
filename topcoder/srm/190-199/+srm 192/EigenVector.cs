using System;

namespace TopCoder.Algorithm {
    public class EigenVector {
        private int[] eigenVector;

        public int[] findEigenVector(string[] a) {
            return findEigenVector(Array.ConvertAll(a, s => {
                return Array.ConvertAll(s.Split(' '), int.Parse);
            }));
        }

        private int[] findEigenVector(int[][] a) {
            eigenVector = new int[a.Length];
            for (var l0 = 1; l0 <= 9; ++l0) {
                if (findEigenVector(a, l0, 0, 0)) {
                    return eigenVector;
                }
            }
            throw new Exception();
        }

        private bool findEigenVector(int[][] a, int l0, int pos, int any) {
            if (pos < eigenVector.Length) {
                for (var e = -l0 * any; e <= +l0; ++e) {
                    eigenVector[pos] = e;
                    if (findEigenVector(a, l0 - Math.Abs(e), pos + 1, Math.Max(any, e > 0 ? 1 : 0))) {
                        return true;
                    }
                }
                return false;
            }
            return eigen(a, eigenVector);
        }

        private static bool eigen(int[][] a, int[] x) {
            var b = mul(a, x);
            for (var i = 0; i < a.Length; ++i)
                if (x[i] != 0) {
                    var k = b[i] / x[i];
                    if (k != 0) {
                        for (var j = 0; j < a.Length; ++j)
                            if (x[j] * k != b[j]) {
                                return false;
                            }
                        return true;
                    }
                }
            return false;
        }

        private static int[] mul(int[][] a, int[] b) {
            var result = new int[a.Length];
            for (var i = 0; i < a.Length; ++i)
                for (var j = 0; j < a.Length; ++j) {
                    result[i] += a[i][j] * b[j];
                }
            return result;
        }
    }
}