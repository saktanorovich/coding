/* Given an array of distinct numbers count the number of
 * triplets with sum less than or equal to the given value. */
using System;

namespace interview.hackerrank {
    public class Triplets {
        public long numOfTriplets(int threshold, int[] d) {
            Array.Sort(d);
            var result = 0L;
            for (var i = 0; i + 2 < d.Length; ++i) {
                result += numOfPairs(threshold - d[i], d, i + 1, d.Length - 1);
            }
            return result;
        }

        private long numOfPairs(int threshold, int[] d, int lo, int hi) {
            var result = 0L;
            while (lo < hi) {
                if (d[lo] + d[hi] > threshold) {
                    --hi;
                }
                else {
                    result += hi - lo;
                    ++lo;
                }
            }
            return result;
        }
    }
}
