using System;

namespace interview.hackerrank {
    public class ShortestPalindrome {
        public int shortPalin(string S) {
            var length = S.Length;
            var memo = new int[2, length + 1];
            for (int level = 0, prefix = 1; prefix <= length; ++prefix, level = level ^ 1) {
                for (var suffix = 1; suffix <= length; ++suffix) {
                    var max = Math.Max(memo[level, suffix - 1], memo[level ^ 1, suffix]);
                    if (S[prefix - 1] == S[length - suffix]) {
                        max = Math.Max(max, memo[level ^ 1, suffix - 1] + 1);
                    }
                    memo[level, suffix] = max;
                }
            }
            return S.Length - memo[1 ^ (length & 1), length];
        }
    }
}
