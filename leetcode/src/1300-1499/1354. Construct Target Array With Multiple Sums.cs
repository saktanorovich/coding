using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1354 {
        public bool IsPossible(int[] target) {
            var h = new SortedSet<long>(Comparer<long>.Create((a, b) => b.CompareTo(a)));
            var s = 0L;
            foreach (var x in target) {
                h.Add(x);
                s += x;
            }
            while (true) {
                var x = h.First();
                s -= x; 
                if (s == 1 || x == 1) {
                    return true;
                }
                if (s == 0 || x <= s) {
                   return false;
                }
                h.Remove(x);
                x %= s;
                s += x;
                h.Add(x);
            }
        }
    }
}
