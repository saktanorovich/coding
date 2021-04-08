using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0006 {
        public string Convert(string s, int R) {
            var t = new List<char>[R];
            for (var i = 0; i < R; ++i) {
                t[i] = new List<char>();
            }
            var r = 0;
            var c = 0;
            for (var i = 0; i < s.Length;) {
                while (r < R && i < s.Length) {
                    t[r].Add(s[i]);
                    r++;
                    i++;
                }
                r = r - 2;
                c = c + 1;
                if (r < 0) {
                    r = 0;
                }
                while (r > 0 && i < s.Length) {
                    t[r].Add(s[i]);
                    r--;
                    c++;
                    i++;
                }
            }
            var b = new StringBuilder();
            for (var i = 0; i < R; ++i) {
                foreach (var a in t[i]) {
                    b.Append(a);
                }
            }
            return b.ToString();
        }
    }
}
