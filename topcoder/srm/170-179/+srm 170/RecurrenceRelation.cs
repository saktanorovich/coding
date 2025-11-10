using System;

namespace TopCoder.Algorithm {
    public class RecurrenceRelation {
        public int moduloTen(int[] coefficients, int[] initial, int n) {
            if (n < initial.Length) {
                return normalize(initial[n]);
            }
            Array.Resize(ref initial, initial.Length + 1);
            for (var i = coefficients.Length; i <= n; ++i) {
                initial[coefficients.Length] = 0;
                for (var j = 0; j < coefficients.Length; ++j) {
                    initial[coefficients.Length] = normalize(initial[coefficients.Length] + coefficients[j] * initial[j]);
                }
                for (var j = 0; j < coefficients.Length; ++j) {
                    initial[j] = initial[j + 1];
                }
            }
            return initial[coefficients.Length];
        }

        private static int normalize(int x) {
            return ((x % 10) + 10) % 10;
        }
    }
}