using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1090 {
        public int LargestValsFromLabels(int[] values, int[] labels, int num_wanted, int use_limit) {
            var ord = Enumerable.Range(0, values.Length).ToArray();
            Array.Sort(ord, (x, y) => {
                return -values[x] + values[y];
            });
            var cnt = new Dictionary<int, int>();
            var res = new List<int>();
            foreach (var i in ord) {
                if (cnt.ContainsKey(labels[i]) == false) {
                    cnt[labels[i]] = 1;
                } else {
                    cnt[labels[i]]++;
                }
                if (cnt[labels[i]] <= use_limit) {
                    if (res.Count < num_wanted) {
                        res.Add(values[i]);
                    }
                }
            }
            return res.Sum();
        }
    }
}
