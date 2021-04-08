using System;
using System.Collections.Generic;
using System.Text;

namespace coding.leetcode {
    public class Solution_0344 {
        public void ReverseString(char[] s) {
            for (int i = 0, j = s.Length - 1; i < j;) {
                var t = s[i];
                s[i] = s[j];
                s[j] = t;
                ++i;
                --j;
            }
        }
    }
}
