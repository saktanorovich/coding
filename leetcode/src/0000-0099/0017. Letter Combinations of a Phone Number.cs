using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0017 {
        public IList<string> LetterCombinations(string digits) {
            if (String.IsNullOrEmpty(digits)) {
                return new string[0];
            }
            return doit(digits, 0, new StringBuilder()).ToList();
        }

        private IEnumerable<string> doit(string digits, int p, StringBuilder s) {
            if (p == digits.Length) {
                yield return s.ToString();
            } else {
                var d = digits[p] - '0' - 1;
                if (d > 0) {
                    foreach (var c in keyboard[d]) {
                        s.Append(c);
                        foreach (var t in doit(digits, p + 1, s)) {
                            yield return t;
                        }
                        s.Remove(s.Length - 1, 1);
                    }
                }
            }
        }

        private static string[] keyboard = new[] {
                "", "abc", "def",
             "ghi", "jkl", "mno",
            "pqrs", "tuv", "wxyz"
        };
    }
}
