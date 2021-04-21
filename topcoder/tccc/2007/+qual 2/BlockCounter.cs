using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class BlockCounter {
        public long countBlocks(string word) {
            return (long)run(word.Replace(",", ""));
        }

        private static Triple run(string word) {
            if (string.IsNullOrEmpty(word)) {
                return new Triple('#', '#', 0);
            }
            else if (word.Length == 1) {
                return new Triple(word[0], word[0], 1);
            }
            else if ("AB".IndexOf(word[0]) >= 0) {
                return run(word.Substring(0, 1)) + run(word.Substring(1));
            }
            else if (word[0] == '(') {
                for (int i = 0, cnt = 0; i < word.Length; ++i) {
                    if (word[i] == '(') ++cnt;
                    if (word[i] == ')') --cnt;
                    if (cnt == 0) {
                        return run(word.Substring(1, i - 1)) + run(word.Substring(i + 1));
                    }
                }
            }
            return int.Parse(word[0].ToString()) * run(word.Substring(1));
        }

        private class Triple {
            private readonly char beg;
            private readonly char end;
            private readonly long cnt;

            public Triple(char beg, char end, long cnt) {
                this.beg = beg;
                this.end = end;
                this.cnt = cnt;
            }

            public static Triple operator +(Triple a, Triple b) {
                var beg = a.beg;
                var end = b.end == '#' ? a.end : b.end;
                if (a.end == b.beg) {
                    return new Triple(beg, end, a.cnt + b.cnt - 1);
                }
                else {
                    return new Triple(beg, end, a.cnt + b.cnt);
                }
            }

            public static Triple operator *(int times, Triple t) {
                if (t.beg == t.end) {
                    return new Triple(t.beg, t.end, t.cnt * times - times + 1);
                }
                else {
                    return new Triple(t.beg, t.end, t.cnt * times);
                }
            }

            public static explicit operator long(Triple t) {
                return t.cnt;
            }
        }
    }
}
