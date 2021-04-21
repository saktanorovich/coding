using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bsuir.algorithm {
    public class Problem_10 {
        public bool process(int testCase, InputReader reader, OutputWriter writer) {
            var s = reader.Next();
            var res = 1L * s.Length * (s.Length + 1) / 2;
            var lcp = longestCommonPrefix(s, suffixArray(s));
            for (var i = 0; i < lcp.Length; ++i) {
                res -= lcp[i];
            }
            writer.WriteLine(res);
            return true;
        }

        private int[] longestCommonPrefix(string s, int[] suf) {
            /* kasai algorithm for lcp where lcp[i] is lcp for suf[i] and suf[i + 1].. */
            var lcp = new int[s.Length];
            var pos = new int[s.Length];
            for (var i = 0; i < s.Length; ++i) {
                pos[suf[i]] = i;
            }
            for (int i = 0, k = 0; i < s.Length; ++i) {
                if (pos[i] + 1 < s.Length) {
                    for (var j = suf[pos[i] + 1]; ;) {
                        if (i + k < s.Length && j + k < s.Length) {
                            if (s[i + k] == s[j + k]) {
                                k = k + 1;
                                continue;
                            }
                        }
                        break;
                    }
                    lcp[pos[i]] = k;
                    if (k > 0) {
                        k--;
                    }
                }
            }
            return lcp;
        }

        private int[] suffixArray(string s) {
            var suf = new int[s.Length];
            var cur = new long[s.Length];
            var nxt = new long[s.Length];
            for (var i = 0; i < s.Length; ++i) {
                suf[i] = i;
                cur[i] = s[i];
            }
            for (var h = 1; h < s.Length; h <<= 1) {
                for (var i = 0; i < s.Length; ++i) {
                    nxt[i] = cur[i] << 32;
                    if (i + h < s.Length) {
                        nxt[i] |= cur[i + h];
                    }
                }
                Array.Sort(suf, (a, b) => nxt[a].CompareTo(nxt[b]));
                cur[suf[0]] = 1;
                for (var i = 1; i < s.Length; ++i) {
                    cur[suf[i]] = i + 1;
                    if (nxt[suf[i]] == nxt[suf[i - 1]]) {
                        cur[suf[i]] = cur[suf[i - 1]];
                    }
                }
            }
            return suf;
        }
    }
}
