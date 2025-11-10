using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1415 {
        public string GetHappyString(int n, int k) {
            var count = new int[n + 1, 3];
            count[1, 0] = 1;
            count[1, 1] = 1;
            count[1, 2] = 1;
            for (var i = 2; i <= n; ++i) {
                count[i, 0] += count[i - 1, 1] + count[i - 1, 2];
                count[i, 1] += count[i - 1, 0] + count[i - 1, 2];
                count[i, 2] += count[i - 1, 0] + count[i - 1, 1];
            }
            var total = count[n, 0] + count[n, 1] + count[n, 2];
            if (total >= k) {
                var result = new StringBuilder();
                var symbol = -1;
                for (var i = n; i > 0; --i) {
                    for (var x = 0; x < 3; ++x) {
                        if (x != symbol) {
                            if (k >  count[i, x]) {
                                k -= count[i, x];
                            } else {
                                result.Append("abc"[x]);
                                symbol = x;
                                break;
                            }
                        }
                    }
                }
                return result.ToString();
            }
            return String.Empty;
        }
    }
}
