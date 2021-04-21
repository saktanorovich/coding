using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0354 {
        public int MaxEnvelopes(int[][] envelopes) {
            if (envelopes == null || envelopes.Length == 0) {
                return 0;
            }
            Array.Sort(envelopes, (a, b) => {
                return a[0] - b[0];
            });
            var best = new int[envelopes.Length];
            for (var i = 0; i < envelopes.Length; ++i) {
                best[i] = 1;
                for (var j = 0; j < i; ++j) {
                    if (fit(envelopes[j], envelopes[i])) {
                        if (best[i] < best[j] + 1) {
                            best[i] = best[j] + 1;
                        }
                    }
                }
            }
            return best.Max();
        }

        private bool fit(int[] a, int[] b) {
            return a[0] < b[0] && a[1] < b[1];
        }
    }
}
