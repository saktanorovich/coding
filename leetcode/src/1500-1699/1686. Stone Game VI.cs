using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1686 {
        public int StoneGameVI(int[] a, int[] b) {
            var o = Enumerable.Range(0, a.Length).ToArray();
            Array.Sort(o, (x, y) => {
                return (a[y] + b[y]) - (a[x] + b[x]);
            });
            var A = 0;
            var B = 0;
            for (var i = 0; i < a.Length; ++i) {
                if (i % 2 == 0) {
                    A += a[o[i]];
                } else {
                    B += b[o[i]];
                }
            }
            return A.CompareTo(B);
        }
    }
}
