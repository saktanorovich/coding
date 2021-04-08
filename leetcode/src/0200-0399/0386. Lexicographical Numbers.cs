using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0386 {
        public IList<int> LexicalOrder(int n) {
            var res = new List<int>();
            for (var x = 1; res.Count < n; ) {
                res.Add(x);
                if (x * 10 <= n) {
                    x = 10 * x;
                    continue;
                }
                if (x % 10 != 9) {
                    if (x + 1 <= n) {
                        x = 1 + x;
                        continue;
                    }
                }
                while (true) {
                    x /= 10;
                    if (x % 10 != 9) {
                        break;
                    }
                }
                x = x + 1;
            }
            return res;
        }
    }
}
