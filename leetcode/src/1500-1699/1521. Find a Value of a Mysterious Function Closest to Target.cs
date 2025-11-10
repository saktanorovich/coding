using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1521 {
        public int ClosestToTarget(int[] a, int target) {
            var st = new int[a.Length, 18];
            var lg = new int[a.Length + 1];
            lg[1] = 0;
            for (var i = 2; i <= a.Length; ++i) {
                lg[i] = lg[i >> 1] + 1;
            }
            for (var i = 0; i < a.Length; ++i) {
                st[i, 0] = a[i];
            }
            for (var k = 1; k < 18; ++k) {
                for (var i = 0; i + (1 << k) <= a.Length; ++i) {
                    st[i, k] = st[i, k - 1] & st[i + (1 << (k - 1)), k - 1];
                }
            }
            int and(int l, int r) {
                var k = lg[r - l + 1];
                return st[l, k] & st[r - (1 << k) + 1, k];
            }
            var opt = int.MaxValue;
            for (var i = 0; i < a.Length; ++i) {
                int lo = i, hi = a.Length - 1;
                while (lo <= hi) {
                    var xx = (lo + hi) / 2;
                    var an = and(i, xx);
                    if (an > target) {
                        lo = xx + 1;
                    } else {
                        hi = xx - 1;
                    }
                    opt = Math.Min(opt, Math.Abs(target - an));
                }
            }
            return opt;
        }
    }
}
