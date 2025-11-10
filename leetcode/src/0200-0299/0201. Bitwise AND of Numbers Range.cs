using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0201 {
        public int RangeBitwiseAnd(int m, int n) {
            var and = 0;
            for (var bit = 0; bit < 32; ++bit) {
                and |= has(m, n, 1 << bit) << bit;
            }
            return and;
        }

        private int has(int m, int n, int bits) {
            var bit = (m / bits) & 1;
            var off = (m % bits);
            if (bit == 0) {
                return 0;
            }
            if (m - off > n - bits) {
                return 1;
            } else {
                return 0;
            }
        }
    }
}
