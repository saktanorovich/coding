using System;

namespace interview.hackerrank {
    public class FlipTheBits {
        /* we need to maximize the objective {S + (Oi - Ei), i is an interval},
         * where Oi is the number of 0, Ei is the number of 1 which is equivalent
         * to maximizing (Oi - Ei) over all intervals.. */
        public int count(int[] d) {
            int total = 0, maxim = 0;
            for (int i = 0, local = 0; i < d.Length; ++i) {
                total += d[i];
                local = Math.Max(local - 2 * d[i] + 1, 0);
                maxim = Math.Max(maxim, local);
            }
            return total + maxim;
        }
    }
}
