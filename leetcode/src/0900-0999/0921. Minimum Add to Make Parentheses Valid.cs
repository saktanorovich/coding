using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0921 {
        public int MinAddToMakeValid(string s) {
            var add = 0;
            var bal = 0;
            foreach (var c in s) {
                bal += c == '(' ? +1 : -1;
                if (bal < 0) {
                    bal ++;
                    add ++;
                }
            }
            return bal + add;
        }
    }
}
