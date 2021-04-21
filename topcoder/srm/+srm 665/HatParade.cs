using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class HatParade {
        public int getPermutation(int[] val, int[] sum) {
            return getPermutation(val, sum, val.Length, val.Sum(x => 1L * x));
        }

        private static int getPermutation(int[] val, int[] sum, int n, long total) {
            var min = new long[n];
            for (var i = 0; i < n; ++i) {
                min[i] = Math.Min(sum[i], total - sum[i] + val[i]);
            }
            for (var i = 0; i < n; ++i) {
                for (var j = i + 1; j < n; ++j) {
                    if (min[j] < min[i]) {
                        swap(ref min[i], ref min[j]);
                        swap(ref val[i], ref val[j]);
                        swap(ref sum[i], ref sum[j]);
                    }
                }
            }
            var result = 1L;
            var prefix = 0L;
            var suffix = 0L;
            for (var i = 0; i < n; ++i) {
                if (prefix == suffix) {
                    if (i + 1 < n) {
                        result = (2 * result) % modulo;
                    }
                }
                if (prefix + val[i] == min[i]) {
                    prefix += val[i];
                    continue;
                }
                if (suffix + val[i] == min[i]) {
                    suffix += val[i];
                    continue;
                }
                return 0;
            }
            return (int)result;
        }

        private static void swap<T>(ref T one, ref T two) {
            var tmp = one;
            one = two;
            two = tmp;
        }

        private const long modulo = (long)1e9 + 7;
    }
}