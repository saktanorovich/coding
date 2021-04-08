using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0392 {
        public bool IsSubsequence(string s, string t) {
            if (s.Length <= t.Length) {
                if (s.Length == 0) {
                    return true;
                }
                for (int i = 0, j = 0; i < t.Length; ++i) {
                    if (t[i] == s[j]) {
                        j = j + 1;
                        if (j == s.Length) {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
