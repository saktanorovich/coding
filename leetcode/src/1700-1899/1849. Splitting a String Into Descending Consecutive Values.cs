using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1849 {
        public bool SplitString(string s) {
            for (var i = 0; i < s.Length; ++i) {
                if (long.TryParse(s.Substring(0, i + 1), out var x)) {
                    if (split(s, i + 1, x)) {
                        return true;
                    }
                } else break;
            }
            return false;
        }

        private bool split(string s, int k, long x) {
            var res = 1;
            while (k < s.Length) {
                x = x - 1;
                if (match(x.ToString(), s, ref k)) {
                    res++;
                } else {
                    return false;
                }
            }
            return res > 1;
        }

        private bool match(string t, string s, ref int k) {
            while (k + 1 < s.Length && s[k] == '0') {
                k = k + 1;
            }
            for (var i = 0; i < t.Length; ++i, ++k) {
                if (k >= s.Length || t[i] != s[k]) {
                    return false;
                }
            }
            return true;
        }
    }
}
