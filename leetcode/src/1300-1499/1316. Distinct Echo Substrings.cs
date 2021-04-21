using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1316 {
        public int DistinctEchoSubstrings(string text) {
            var hash = new Hash(text);
            var answ = new HashSet<ulong>();
            for (var i = 0; i < text.Length; ++i) {
                for (var j = i + 1; j < text.Length; ++j) {
                    var len = j - i + 1;
                    if (len % 2 == 0) {
                        var k = (i + j) / 2;
                        if (hash[i, k] == hash[k + 1, j]) {
                            answ.Add(hash[i, k]);
                        }
                    }
                }
            }
            return answ.Count;
        }

        private sealed class Hash {
            private readonly ulong P = 239017;
            private readonly ulong[] h;
            private readonly ulong[] d;

            public Hash(string s) {
                h = new ulong[s.Length + 1];
                d = new ulong[s.Length + 1];
                h[0] = 0;
                d[0] = 1;
                for (var i = 0; i < s.Length; ++i) {
                    h[i + 1] = h[i] * P + s[i];
                    d[i + 1] = d[i] * P;
                }
            }

            public ulong this[int i, int j] {
                get {
                    return h[j + 1] - h[i] * d[j - i + 1];
                }
            }
        }
    }
}
