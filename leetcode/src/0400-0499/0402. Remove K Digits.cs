using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0402 {
        public string RemoveKdigits(string num, int k) {
            var s = new Stack<char>();
            foreach (var c in num) {
                while (s.Count > 0) {
                    if (c < s.Peek() && k-- > 0) {
                        s.Pop();
                    }
                    else break;
                }
                s.Push(c);
            }
            while (s.Count > 0) {
                if (k-- > 0) {
                    s.Pop();
                }
                else break;
            }
            var res = new String(s.Reverse().ToArray()).TrimStart('0');
            if (res != "") {
                return res;
            } else {
                return "0";
            }
        }
    }
}
