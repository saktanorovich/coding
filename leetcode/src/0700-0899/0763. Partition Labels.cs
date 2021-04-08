using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0763 {
        public IList<int> PartitionLabels(string s) {
            var l = new int[26];
            var r = new int[26];
            for (var c = 0; c < 26; ++c) {
                l[c] = int.MaxValue;
                r[c] = int.MinValue;
            }
            for (var i = 0; i < s.Length; ++i) {
                l[s[i] - 'a'] = Math.Min(l[s[i] - 'a'], i);
                r[s[i] - 'a'] = Math.Max(r[s[i] - 'a'], i);
            }
            var res = new List<int>();
            var min = 0;
            var max = r[s[0] - 'a'];
            foreach (var c in s) {
                if (l[c - 'a'] > max) {
                    res.Add(max - min + 1);
                    min = l[c - 'a'];
                    max = r[c - 'a'];
                } else {
                    max = Math.Max(max, r[c - 'a']);
                }
            }
            res.Add(max - min + 1);
            return res;
        }
    }
}
