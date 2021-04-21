using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1542 {
        public int LongestAwesome(string s) {
            var pos = new int[1 << 10];
            for (var k = 1; k < pos.Length; ++k) {
                pos[k] = -2;
            }
            pos[0] = -1;
            var set = 0;
            var res = 1;
            for (var i = 0; i < s.Length; ++i) {
                set ^= (1 << (s[i] - '0'));
                if (pos[set] == -2) {
                    pos[set] = i;
                }
                for (var k = 0; k < 10; ++k) {
                    var msk = set ^ (1 << k);
                    if (pos[msk] != -2) {
                        res = Math.Max(res, i - pos[msk]);
                    }
                }
                res = Math.Max(res, i - pos[set]);
            }
            return res;
        }
    }
}
