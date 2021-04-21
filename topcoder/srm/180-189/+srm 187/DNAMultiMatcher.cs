using System;

namespace TopCoder.Algorithm {
    public class DNAMultiMatcher {
        public int longestMatch(string[] sequence1, string[] sequence2, string[] sequence3) {
            return longestMatch(string.Concat(sequence1), string.Concat(sequence2), string.Concat(sequence3));
        }

        private static int longestMatch(string sequence1, string sequence2, string sequence3) {
            var d1 = longestMatch(sequence1, sequence3);
            var d2 = longestMatch(sequence2, sequence3);
            var result = 0;
            for (var i = 0; i < sequence3.Length; ++i) {
                result = Math.Max(result, Math.Min(d1[i], d2[i]));
            }
            return result;
        }

        private static int[] longestMatch(string sequence1, string sequence2) {
            var d = new int[sequence1.Length + 1, sequence2.Length + 1];
            for (var i = 1; i <= sequence1.Length; ++i) {
                for (var j = 1; j <= sequence2.Length; ++j) {
                    if (sequence1[i - 1] == sequence2[j - 1]) {
                        d[i, j] = 1 + d[i - 1, j - 1];
                    }
                }
            }
            var result = new int[sequence2.Length];
            for (var j = 1; j <= sequence2.Length; ++j) {
                for (var i = 1; i <= sequence1.Length; ++i) {
                    result[j - 1] = Math.Max(result[j - 1], d[i, j]);
                }
            }
            return result;
        }
    }
}