using System;

namespace interview.hackerrank {
    public class SubstringCalculator {
        public long substringCalculator(string s) {
            var result = 1L * s.Length * (s.Length + 1) / 2;
            var lcp = getLcp(s, getSuffixArray(s, s.Length), s.Length);
            for (var i = 0; i < lcp.Length; ++i) {
                result -= lcp[i];
            }
            return result;
        }

        private int[] getLcp(string s, int[] suffixArray, int n) {
            /* kasai algorithm for lcp where lcp[i] is lcp for suffix[i] and suffix[i + 1].. */
            var lcp = new int[n];
            var pos = new int[n];
            for (var i = 0; i < n; ++i) {
                pos[suffixArray[i]] = i;
            }
            for (int i = 0, k = 0; i < n; ++i) {
                if (pos[i] + 1 < n) {
                    for (var j = suffixArray[pos[i] + 1]; true;) {
                        if (i + k < n && j + k < n) {
                            if (s[i + k] == s[j + k]) {
                                k = k + 1;
                                continue;
                            }
                        }
                        break;
                    }
                    lcp[pos[i]] = k;
                    k = Math.Max(k - 1, 0);
                }
            }
            return lcp;
        }

        private int[] getSuffixArray(string s, int n) {
            var suffixArray = new int[n];
            var currColor = new long[n];
            var nextColor = new long[n];
            for (var i = 0; i < s.Length; ++i) {
                suffixArray[i] = i;
                currColor[i] = s[i];
            }
            for (var h = 1; h < n; h <<= 1) {
                for (var i = 0; i < n; ++i) {
                    nextColor[i] = currColor[i] << 32;
                    if (i + h < n) {
                        nextColor[i] |= currColor[i + h] + 1;
                    }
                }
                Array.Sort(suffixArray, (a, b) => nextColor[a].CompareTo(nextColor[b]));
                currColor[suffixArray[0]] = 0;
                for (var i = 1; i < n; ++i) {
                    currColor[suffixArray[i]] = i;
                    if (nextColor[suffixArray[i]] == nextColor[suffixArray[i - 1]]) {
                        currColor[suffixArray[i]] = currColor[suffixArray[i - 1]];
                    }
                }
            }
            return suffixArray;
        }

        public long subsequenceCalculator(string s) {
            var memo = new long[s.Length + 1];
            var last = new long[256];
            memo[0] = 1;
            for (var i = 1; i <= s.Length; ++i) {
                memo[i] = 2 * memo[i - 1];
                if (last[s[i - 1]] > 0) {
                    memo[i] = memo[i] - memo[last[s[i - 1]] - 1];
                }
                last[s[i - 1]] = i;
            }
            return memo[s.Length];
        }
    }
}
