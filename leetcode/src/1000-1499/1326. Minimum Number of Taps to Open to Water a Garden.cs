using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1326 {
        public int MinTaps(int n, int[] ranges) {
            var far = new int[n + 1];
            for (var i = 0; i <= n; ++i) {
                var a = i - ranges[i];
                var b = i + ranges[i];
                if (a < 0) {
                    a = 0;
                }
                far[a] = Math.Max(far[a], b);
            }
            var res = 0;
            var cur = 0;
            var nxt = 0;
            for (var i = 0; i <= n; ++i) {
                if (nxt < i) {
                    return -1;
                }
                nxt = Math.Max(nxt, far[i]);
                if (cur == i) {
                    if (cur < n) {
                        cur = nxt;
                        res = res + 1;
                    }
                }
            }
            return res;
        }
    }
}
