using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopCoder.Algorithm {
    public class Aaagmnrs {
        public string[] anagrams(string[] phrases) {
            var res = new List<string>();
            for (var i = 0; i < phrases.Length; ++i) {
                for (var j = 0; j < i; ++j) {
                    if (areSame(phrases[i], phrases[j])) {
                        goto next;
                    }
                }
                res.Add(phrases[i]);
                next:;
            }
            return res.ToArray();
        }

        private static bool areSame(string a, string b) {
            var cnt = new int[26];
            foreach (var c in a) {
                if (char.IsWhiteSpace(c) == false) {
                    ++cnt[code(c)];
                }
            }
            foreach (var c in b) {
                if (char.IsWhiteSpace(c) == false) {
                    --cnt[code(c)];
                }
            }
            return cnt.All(x => x == 0);
        }

        private static int code(char c) {
            if ('a' <= c && c <= 'z') {
                return c - 'a';
            }
            return c - 'A';
        }
    }
}
