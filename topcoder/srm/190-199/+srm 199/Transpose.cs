using System;

namespace TopCoder.Algorithm {
    public class Transpose {
        public int numSwaps(int m, int n) {
            var a = new int[n * m];
            for (var i = 0; i < n; ++i) {
                for (var j = 0; j < m; ++j) {
                    a[i * m + j] = j * n + i;
                }
            }
            return numSwaps(a, n * m);
        }

        private static int numSwaps(int[] a, int n) {
            var result = 0;
            for (var i = 0; i < n; ++i) {
                for (var x = i; a[i] != i; x = a[x]) {
                    var temp = a[a[x]];
                    a[a[x]] = a[x];
                    a[x] = temp;
                    result = result + 1;
                }
            }
            return result;
        }
        /**
        public int numSwaps(int m, int n) {
            var source = new int[m, n];
            for (int i = 0, x = 0; i < m; ++i) {
                for (var j = 0; j < n; ++j, ++x) {
                    source[i, j] = x;
                }
            }
            return numSwaps(linearize(source, m, n), linearize(transpose(source, m, n), n, m), m * n);
        }

        private static int numSwaps(int[] source, int[] target, int n) {
            var result = 0;
            for (var i = 0; i < n; ++i) {
                if (source[i] != target[i]) {
                    for (var x = i; source[x] != target[x]; x = target[x]) {
                        var temp = source[x];
                        source[x] = target[x];
                        source[target[x]] = temp;
                        result = result + 1;
                    }
                }
            }
            return result;
        }

        private static int[] linearize(int[,] a, int m, int n) {
            var result = new int[m * n];
            for (int i = 0, x = 0; i < m; ++i) {
                for (var j = 0; j < n; ++j, ++x) {
                    result[x] = a[i, j];
                }
            }
            return result;
        }

        private static int[,] transpose(int[,] a, int m, int n) {
            var result = new int[n, m];
            for (var i = 0; i < n; ++i) {
                for (var j = 0; j < m; ++j) {
                    result[i, j] = a[j, i];
                }
            }
            return result;
        }
        /**/
    }
}