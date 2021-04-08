using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0132 {
        public int MinCut(string s) {
            var p = new bool[s.Length, s.Length];
            for (var i = 0; i < s.Length; ++i) {
                p[i, i] = true;
                if (i > 0) {
                    p[i, i - 1] = true;
                }
            }
            for (var l = 1; l < s.Length; ++l) {
                for (var j = l; j < s.Length; ++j) {
                    p[j - l, j] = p[j - l + 1, j - 1] && s[j - l] == s[j];
                }
            }
            var f = new int[s.Length];
            for (var i = 0; i < s.Length; ++i) {
                if (p[0, i] == false) {
                    f[i] = int.MaxValue;
                    for (var j = 0; j < i; ++j) {
                        if (p[j + 1, i]) {
                            if (f[i] > f[j] + 1) {
                                f[i] = f[j] + 1;
                            }
                        }
                    }
                }
            }
            return f[s.Length - 1];
        }
    }
}
