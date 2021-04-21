using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class DNASingleMatcher {
        public int longestMatch(string sequence1, string sequence2) {
            var d = new int[sequence1.Length + 1, sequence2.Length + 1];
            for (var i = 1; i <= sequence1.Length; ++i) {
                for (var j = 1; j <= sequence2.Length; ++j) {
                    if (sequence1[i - 1] == sequence2[j - 1]) {
                        d[i, j] = 1 + d[i - 1, j - 1];
                    }
                }
            }
            var result = 0;
            for (var i = 1; i <= sequence1.Length; ++i) {
                for (var j = 1; j <= sequence2.Length; ++j) {
                    result = Math.Max(result, d[i, j]);
                }
            }
            return result;
        }
    }
}