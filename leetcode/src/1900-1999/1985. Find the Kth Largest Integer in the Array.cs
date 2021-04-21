using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1985 {
        public string KthLargestNumber(string[] nums, int k) {
            var set = new SortedSet<(int, string)>(Comparer<(int, string)>.Create(Cmp));
            for (var i = 0; i < nums.Length; ++i) {
                set.Add((i, nums[i]));
                if (set.Count > k) {
                    set.Remove(set.Max);
                }
            }
            return set.Max.Item2;
        }

        private int Cmp((int, string) x, (int, string) y) {
            var a = x.Item2.Length;
            var b = y.Item2.Length;
            if (a.CompareTo(b) != 0) {
                return b.CompareTo(a);
            }
            if (x.Item2.CompareTo(y.Item2) != 0)
                return y.Item2.CompareTo(x.Item2);
            else
                return x.Item1.CompareTo(y.Item1);
        }
    }
}