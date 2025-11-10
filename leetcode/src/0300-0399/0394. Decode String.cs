using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0394 {
        public string DecodeString(string s) {
            var index = 0;
            return DecodeString(s, ref index);
        }

        private string DecodeString(string s, ref int index) {
            var buffer = new StringBuilder();
            var count  = 0;
            while (index < s.Length) {
                var c = s[index++];
                if (char.IsDigit(c)) {
                    count = count * 10 + (c - '0');
                } else if (char.IsLetter(c)) {
                    buffer.Append(c);
                } else if (c == '[') {
                    var substring = DecodeString(s, ref index);
                    for (; count > 0; --count) {
                        buffer.Append(substring);
                    }
                } else if (c == ']') break;
            }
            return buffer.ToString();
        }
    }
}
