using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0927 {
        public int[] ThreeEqualParts(int[] a) {
            var sum = a.Sum();
            if (sum % 3 == 0) {
                if (sum == 0) {
                    return new[] { 0, 2 };
                }
                return ThreeEqualParts(a, a.Length, sum / 3);
            }
            return new[] { -1, -1 };
        }

        private int[] ThreeEqualParts(int[] a, int n, int m) {
            var b = new List<int>();
            for (var i = 0; i < n; ++i) {
                if (a[i] == 1) {
                    b.Add(i);
                }
            }
            var L = n - b[2 * m];
            var x = b[0];
            var y = b[m];
            var z = b[m * 2];
            for (var i = 0; i < L; ++i) {
                if (a[x + i] != a[y + i] || a[y + i] != a[z + i]) {
                    return new[] { -1, -1 };
                }
            }
            return new[] { x + L - 1, y + L };
        }
    }
}
