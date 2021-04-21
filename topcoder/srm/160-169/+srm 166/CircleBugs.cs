using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopCoder.Algorithm {
    public class CircleBugs {
        public int cycleLength(string formation) {
            var next = lexi(formation);
            memo[next] = 0;
            for (var time = 1;; ++time) {
                next = generate(next);
                if (memo.ContainsKey(next)) {
                    return time - memo[next];
                }
                memo[next] = time;
            }
        }

        private static string generate(string formation) {
            var forward = string.Empty;
            var reverse = string.Empty;
            for (var i = 0; i < formation.Length; ++i) {
                var cc = take(formation + formation, i);
                forward = forward + cc;
                reverse = cc + reverse;
            }
            return min(lexi(forward), lexi(reverse));
        }

        private static string take(string formation, int pos) {
            if (formation[pos].Equals(formation[pos + 1]))
                return "R";
            return "G";
        }

        private static string lexi(string formation) {
            string minimum = formation, doubled = formation + formation;
            for (var i = 1; i < formation.Length; ++i) {
                minimum = min(minimum, doubled.Substring(i, formation.Length));
            }
            return minimum;
        }

        private static string min(string a, string b) {
            if (a.CompareTo(b) < 0) {
                return a;
            }
            return b;
        }

        private readonly IDictionary<string, int> memo = new Dictionary<string, int>();
    }
}