using System;

namespace interview.hackerrank {
    public class NonDominatableEntities {
        public int count(long[] x, long[] y, int n) {
            Array.Sort(x, y);
            var maximum = new long[n + 1];
            var nearest = new long[n + 1];
            maximum[n - 1] = y[n - 1];
            nearest[n - 1] = n;
            for (var i = n - 2; i >= 0; --i) {
                nearest[i] = i + 1;
                if (x[i] == x[i + 1]) {
                    nearest[i] = nearest[i + 1];
                }
                maximum[i] = Math.Max(y[i], maximum[i + 1]);
            }
            var result = 0;
            for (var i = 0; i < n; ++i) {
                if (maximum[nearest[i]] <= y[i]) {
                    result = result + 1;
                }
            }
            return result;
        }
    }
}
