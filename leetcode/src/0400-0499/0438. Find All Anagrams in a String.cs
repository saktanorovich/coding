using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0438 {
        public IList<int> FindAnagrams(string s, string p) {
            if (p.Length > s.Length) {
                return new int[0];
            }
            var etalon = new Dictionary<char, int>();
            var window = new Dictionary<char, int>();
            for (var i = 0; i < p.Length; ++i) {
                insert(etalon, p[i]);
                insert(window, s[i]);
            }
            var res = new List<int>();
            var lo = 0;
            var hi = p.Length - 1;
            while (hi < s.Length) {
                if (match(window, etalon)) {
                    res.Add(lo);
                }
                remove(window, s[lo]);
                lo ++;
                hi ++;
                if (hi < s.Length) {
                    insert(window, s[hi]);
                }
            }
            return res;
        }

        private void insert(Dictionary<char, int> dict, char c) {
            dict.TryGetValue(c, out var cnt);
            dict[c] = cnt + 1;
        }

        private void remove(Dictionary<char, int> dict, char c) {
            if (dict.TryGetValue(c, out var cnt)) {
                dict[c] = cnt - 1;
            }
        }

        private bool match(Dictionary<char, int> window, Dictionary<char, int> etalon) {
            foreach (var kvp in etalon) {
                if (window.TryGetValue(kvp.Key, out var cnt)) {
                    if (kvp.Value == cnt) {
                        continue;
                    }
                }
                return false;
            }
            return true;
        }
    }
}
