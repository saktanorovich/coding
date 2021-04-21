using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0405 {
        public string ToHex(int num) {
            if (num == 0) {
                return "0";
            }
            var res = new StringBuilder();
            while (num != 0 && res.Length < 8) {
                res.Insert(0, h(num & 0xf));
                num >>= 4;
            }
            return res.ToString();
        }

        private char h(int b) {
            if (b < 10) {
                return (char)('0' + b);
            } else {
                return (char)('a' + b - 10);
            }
        }
    }
}
