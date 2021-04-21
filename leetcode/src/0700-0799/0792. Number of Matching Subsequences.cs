using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0792 {
        public int NumMatchingSubseq(string s, string[] words) {
            var g = new int[s.Length + 1, 26];
            for (var c = 0; c < 26; ++c) {
                g[s.Length, c] = -1;
            }
            for (var i = s.Length - 1; i >= 0; --i) {
                for (var c = 0; c < 26; ++c) {
                    g[i, c] = g[i + 1, c];
                }
                g[i, s[i] - 'a'] = i + 1;
            }
            var res = 0;
            foreach (var word in words) {
                var i = 0;
                var k = 0;
                foreach (var c in word) {
                    if (g[i, c - 'a'] != -1) {
                        i = g[i, c - 'a'];
                        k = k + 1;
                    }
                    else break;
                }
                res += word.Length == k ? 1 : 0;
            }
            return res;
        }
    }
}
