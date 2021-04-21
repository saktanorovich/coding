using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1331 {
        public int[] ArrayRankTransform(int[] a) {
            var b = new SortedSet<int>(a);
            var c = new Dictionary<int, int>();
            foreach (var x in b) {
                c[x] = c.Count + 1;
            }
            var r = new int[a.Length];
            for (var i = 0; i < a.Length; ++i) {
                r[i] = c[a[i]];
            }
            return r;
        }
    }
}
