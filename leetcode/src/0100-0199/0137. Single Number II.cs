using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0137 {
        public int SingleNumber(int[] nums) {
            var bits = new int[32];
            foreach (var x in nums) {
                for (var i = 0; i < 32; ++i) {
                    if ((x & (1 <<i)) != 0) {
                        bits[i]++;
                    }
                }
            }
            var res = 0;
            for (var i = 0; i < 32; ++i) {
                if (bits[i] % 3 != 0) {
                    res |= 1 << i;
                }
            }
            return res;
        }
    }
}
