using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_995 {
        public int MinKBitFlips(int[] a, int K) {
            var ahead = new int[a.Length];
            var state = 0;
            var flips = 0;
            for (var i = 0; i < a.Length; ++i) {
                if ((a[i] ^ state) == 0) {
                    if (i + K > a.Length) {
                        return -1;
                    }
                    flips += 1;
                    state ^= 1;
                    ahead[i + K - 1] = 1;
                }
                state ^= ahead[i];
            }
            return flips;
        }
    }
}
