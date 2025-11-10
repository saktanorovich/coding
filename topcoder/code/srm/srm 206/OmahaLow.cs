using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopCoder.Algorithm {
    public class OmahaLow {
        public string low(string shared, string player) {
            var res = string.Empty;
            for (var i = 0; i < 4; ++i) {
                for (var j = i + 1; j < 4; ++j) {
                    for (var x = 0; x < 5; ++x) {
                        for (var y = x + 1; y < 5; ++y) {
                            for (var z = y + 1; z < 5; ++z) {
                                var set = new[] {
                                    player[i],
                                    player[j],
                                    shared[x],
                                    shared[y],
                                    shared[z],
                                }.OrderByDescending(c => alphabet.IndexOf(c)).ToArray();
                                res = less(res, set);
                            }
                        }
                    }
                }
            }
            return res;
        }

        private static string less(string res, char[] cards) {
            if (cards.Distinct().Count() > 4) {
                if ('1' <= cards[0] && cards[0] <= '8') {
                    var str = new string(cards);
                    if (res == String.Empty || res.CompareTo(str) > 0) {
                        return str;
                    }
                }
            }
            return res;
        }

        private static string alphabet = "0123456789TJQK";
    }
}
