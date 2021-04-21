using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class EntertainingSegment {
        public int longestEntertainingSegment(int[] left, int[] right, int threahold) {
            var xs = new List<int>();
            xs.AddRange(left);
            xs.AddRange(right);
            return longestEntertainingSegment(left, right, xs.Distinct().OrderBy(z => z).ToArray(), threahold);
        }

        private int longestEntertainingSegment(int[] left, int[] right, int[] xs, int threahold) {
            var at = new int[xs.Length];
            for (var i = 0; i + 1 < xs.Length; ++i) {
                for (var k = 0; k < left.Length; ++k) {
                    if (left[k] <= xs[i] && xs[i + 1] <= right[k]) {
                        ++at[i];
                    }
                }
            }
            var res = 0;
            for (var i = 0; i < xs.Length; ++i) {
                for (var j = i + 1; j < xs.Length; ++j) {
                    for (var k = i; k < j; ++k) {
                        if (at[k] < threahold) {
                            goto next;
                        }
                    }
                    res = Math.Max(res, xs[j] - xs[i]);
                    next:;
                }
            }
            return res;
        }
    }
}
