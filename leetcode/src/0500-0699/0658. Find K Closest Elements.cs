using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0658 {
        public IList<int> FindClosestElements(int[] arr, int k, int x) {
            return FindClosestElements(arr, arr.Length - 1, k, x).ToList();
        }

        private IEnumerable<int> FindClosestElements(int[] a, int n, int k, int x) {
            if (x <= a[0]) return Take(a, +k);
            if (x >= a[n]) return Take(a, -k);

            int lo = 0, hi = n;
            while (lo + 1 < hi) {
                var t = (lo + hi) / 2;
                if (a[t] <= x) {
                    lo = t;
                } else {
                    hi = t;
                }
            }
            var list = new LinkedList<int>();
            for (var i = 0; i < k; ++i) {
                if (lo < 0) { list.AddLast (a[hi++]); continue; }
                if (hi > n) { list.AddFirst(a[lo--]); continue; }

                if (x - a[lo] <= a[hi] - x) {
                    list.AddFirst(a[lo--]);
                } else {
                    list.AddLast (a[hi++]);
                }
            }
            return list;
        }

        private IEnumerable<int> Take(int[] a, int k) {
            if (k >= 0) {
                return a.Take(k);
            } else {
                return a.Skip(k + a.Length);
            }
        }
    }
}
