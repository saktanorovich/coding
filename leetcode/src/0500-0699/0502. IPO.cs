using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0502 {
        public int FindMaximizedCapital(int k, int W, int[] P, int[] C) {
            var O = Enumerable.Range(0, P.Length).OrderBy(a => C[a]).ToArray();
            var H = new SortedSet<int>(
                Comparer<int>.Create((a, b) => {
                    if (P[a] != P[b]) {
                        return P[a] - P[b];
                    }
                    return a - b;
            }));
            for (var i = 0; k > 0; --k) {
                for (; i < P.Length; ++i) {
                    if (C[O[i]] <= W) {
                        H.Add(O[i]);
                    }
                    else break;
                }
                if (H.Count > 0) {
                    var max = H.Max;
                    W += P[max];
                    H.Remove(max);
                }
                else break;
            }
            return W;
        }
    }
}
