using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class Wizarding {
        public string counterspell(string spell, string rules) {
            var best = string.Empty;
            var cost = 0;
            for (var set = 1; set < 1 << spell.Length; ++set) {
                var pos = new List<int>();
                for (var i = 0; i < spell.Length; ++i) {
                    if ((set & (1 << i)) != 0) {
                        pos.Add(i);
                    }
                }
                var options = new char[pos.Count, 2];
                for (var i = 0; i < pos.Count; ++i) {
                    var ch = spell[pos[i]];
                    options[i, 0] = ch;
                    if (rules[ch - 'A'] != '-') {
                        ch = rules[ch - 'A'];
                    }
                    options[i, 1] = ch;
                }
                for (var take = 0; take < 1 << pos.Count; ++take) {
                    var newBest = string.Empty;
                    var newCost = 1;
                    for (var i = 0; i < pos.Count; ++i) {
                        var ch = options[i, (take >> i) & 1];
                        newBest += ch;
                        newCost *= ch - 'A' + 1;
                        newCost %= 77077;
                        if (newCost < cost) {
                            continue;
                        }
                        if (newCost > cost) {
                            goto update;
                        }
                        if (newBest.Length > best.Length) {
                            continue;
                        }
                        if (newBest.Length < best.Length) {
                            goto update;
                        }
                        if (newBest.CompareTo(best) > 0) {
                            continue;
                        }
                        update: {
                            cost = newCost;
                            best = newBest;
                        }
                    }
                }
            }
            return best;
        }
    }
}
