using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class Spamatronic {
        public int[] filter(string[] knownSpam, string[] newMail) {
            var smap = new HashSet<string>();
            foreach (var mail in knownSpam) {
                append(smap, mail);
            }
            var result = new List<int>();
            for (var seqno = 0; seqno < newMail.Length; ++seqno) {
                var tokens = new Dictionary<string, int>();
                foreach (var word in split(newMail[seqno])) {
                    tokens[word] = 0;
                    if (smap.Contains(word.ToLower())) {
                        tokens[word] = 1;
                    }
                }
                if (tokens.Values.Sum() * 100 < tokens.Count * 75) {
                    result.Add(seqno);
                    continue;
                }
                append(smap, newMail[seqno]);
            }
            return result.ToArray();
        }

        private static void append(ISet<string> smap, string mail) {
            foreach (var word in split(mail)) {
                smap.Add(word);
            }
        }

        private static IEnumerable<string> split(string mail) {
            return Array.ConvertAll(mail.Split(delimeters.ToArray(),
                StringSplitOptions.RemoveEmptyEntries), word => word.ToLower());
        }

        static Spamatronic() {
            for (var code = 32; code <= 126; ++code) {
                var @char = (char)code;
                if (!char.IsLetter(@char))
                    delimeters.Add(@char);
            }
        }

        private static readonly IList<char> delimeters = new List<char>();
    }
}