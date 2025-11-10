using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1409 {
        public int[] ProcessQueries(int[] queries, int m) {
            var perm = new int[m + 1];
            for (var i = 0; i < m; ++i) {
                perm[i] = i + 1;
            }
            var answ = new int[queries.Length];
            for (var i = 0; i < queries.Length; ++i) {
                for (var k = 0; i < m; ++k) {
                    if (perm[k] == queries[i]) {
                        for (var j = k; j > 0; --j) {
                            perm[j] = perm[j - 1];
                        }
                        perm[0] = queries[i];
                        answ[i] = k;
                        break;
                    }
                }
            }
            return answ;
        }
    }
}
