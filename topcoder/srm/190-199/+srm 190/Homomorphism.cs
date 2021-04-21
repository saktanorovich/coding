using System;
using System.Collections.Generic;
using System.Text;

namespace TopCoder.Algorithm {
    public class Homomorphism {
        public int count(string u, string v) {
            var set = new HashSet<string>();
            for (var i = 0; i < v.Length; ++i) {
                for (var j = i; j < v.Length; ++j) {
                    set.Add(v.Substring(i, j - i + 1));
                }
            }
            foreach (var hh in set) {
                if (apply(hh, inf('1'))(u) == v) return -1;
                if (apply(inf('0'), hh)(u) == v) return -1;
            }
            var homomorphism = new HashSet<Tuple<string, string>>();
            foreach (var h0 in set)
                foreach (var h1 in set) {
                    if (apply(h0, h1)(u) == v) {
                        homomorphism.Add(new Tuple<string, string>(h0, h1));
                    }
                }
            return homomorphism.Count;
        }

        private static Func<string, string> apply(string h0, string h1) {
            return s => {
                var result = new StringBuilder();
                foreach (var bit in s) {
                    if (bit == '0') result.Append(h0);
                    if (bit == '1') result.Append(h1);
                }
                return result.ToString();
            };
        }

        private static string inf(char bit) {
            return "".PadLeft(100, bit);
        }
    }
}