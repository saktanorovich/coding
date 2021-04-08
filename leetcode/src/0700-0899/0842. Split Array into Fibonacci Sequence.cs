using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0842 {
        public IList<int> SplitIntoFibonacci(string s) {
            for (var i = 0; i < s.Length; ++i) {
                if (parse(s, 0, i + 1, out var a)) {
                    for (var j = i + 1; j < s.Length; ++j) {
                        if (parse(s, i + 1, j - i, out var b)) {
                            var fib = split(s, j + 1, a, b);
                            if (fib.Count > 0) {
                                return fib;
                            }
                        } else break;
                    }
                } else break;
            }
            return new int[0];
        }

        private IList<int> split(string s, int k, long a, long b) {
            var res = new List<int> { (int)a, (int)b };
            while (k < s.Length) {
                var c = a + b;
                if (c <= int.MaxValue && match(c.ToString(), s, ref k)) {
                    res.Add((int)c);
                    a = b;
                    b = c;
                } else {
                    return new List<int>();
                }
            }
            return res.Count > 2 ? res : new List<int>();
        }

        private bool match(string t, string s, ref int k) {
            for (var i = 0; i < t.Length; ++i, ++k) {
                if (k >= s.Length || s[k] != t[i]) {
                    return false;
                }
            }
            return true;
        }

        private bool parse(string s, int i, int n, out int x) {
            var z = s.Substring(i, n);
            if (z[0] == '0' && z.Length > 1) {
                z = "";
            }
            return int.TryParse(z, out x);
        }
    }
}
