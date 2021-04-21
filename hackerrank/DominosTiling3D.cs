using System;

namespace interview.hackerrank {
    public class DominosTiling3D {
        private const long modulo = (long)1e9 + 7;
        private const int maximum = (int)1e6;

        private readonly long[] state0 = new long[maximum + 1];
        private readonly long[] state1 = new long[maximum + 1];
        private readonly long[] state2 = new long[maximum + 1];
        private readonly long[] state3 = new long[maximum + 1];
        private readonly long[] state4 = new long[maximum + 1];

        public long[] count(int[] tests) {
            state0[0] = 1;
            state0[1] = 2;
            state1[1] = 1;
            state2[1] = 1;
            state3[1] = 1;
            state4[1] = 1;
            for (var i = 2; i <= maximum; ++i) {
                state0[i] += state0[i - 1];
                state0[i] += state0[i - 1];
                state0[i] += state0[i - 2];
                state0[i] += state1[i - 1];
                state0[i] += state2[i - 1];
                state0[i] += state3[i - 1];
                state0[i] += state4[i - 1];
                state0[i] %= modulo;

                state1[i] += state0[i - 1];
                state1[i] += state2[i - 1];
                state1[i] %= modulo;

                state2[i] += state0[i - 1];
                state2[i] += state1[i - 1];
                state2[i] %= modulo;

                state3[i] += state0[i - 1];
                state3[i] += state4[i - 1];
                state3[i] %= modulo;

                state4[i] += state0[i - 1];
                state4[i] += state3[i - 1];
                state4[i] %= modulo;
            }
            var result = new long[tests.Length];
            for (var i = 0; i < tests.Length; ++i) {
                result[i] = state0[tests[i]];
            }
            return result;
        }
    }
}
