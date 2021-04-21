using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bsuir.algorithm {
    public class Problem_09 {
        public bool process(int testCase, InputReader reader, OutputWriter writer) {
            var s = reader.Next();
            var n = reader.NextInt();
            var l = new bool[s.Length];
            var r = new bool[s.Length];
            for (var i = 0; i < n; ++i) {
                var a = reader.NextInt() - 1;
                var b = reader.NextInt() - 1;
                if (a <= b) {
                    l[a] = !l[a];
                    r[b] = !r[b];
                } else {
                    l[b] = !l[b];
                    r[a] = !r[a];
                }
            }
            var x = false;
            for (var i = 0; i < s.Length; ++i) {
                x ^= l[i];
                if (x) {
                    writer.Write(inv(s[i]));
                } else {
                    writer.Write(s[i]);
                }
                x ^= r[i];
            }
            writer.WriteLine();
            return false;
        }

        private char inv(char c) {
            if (char.IsUpper(c)) {
                return char.ToLower(c);
            } else {
                return char.ToUpper(c);
            }
        }
    }
}
