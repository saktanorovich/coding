using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1520 {
        public IList<string> MaxNumOfSubstrings(string s) {
            var l = new int[26];
            var r = new int[26];
            var o = new List<int>();
            for (var i = 0; i < s.Length; ++i) {
                var c = s[i] - 'a';
                if (o.Contains(c) == false) {
                    l[c] = i;
                    r[c] = i;
                    o.Add(c);
                } else {
                    r[c] = i;
                }
            }
            foreach (var c in o) {
                for (var i = l[c]; i <= r[c]; ++i) {
                    l[c] = Math.Min(l[c], l[s[i] - 'a']);
                    r[c] = Math.Max(r[c], r[s[i] - 'a']);
                }
            }
            o.Sort((a, b) => l[a] - l[b]);
            var res = new List<string>();
            var min = l[o[0]];
            var max = r[o[0]];
            foreach (var c in o) {
                if (max < l[c]) {
                    res.Add(s.Substring(min, max - min + 1));
                    min = l[c];
                    max = r[c];
                    continue;
                }
                if (max > r[c]) {
                    min = l[c];
                    max = r[c];
                } else {
                    max = r[c];
                }
            }
            res.Add(s.Substring(min, max - min + 1));
            return res;
        }
    }
}
