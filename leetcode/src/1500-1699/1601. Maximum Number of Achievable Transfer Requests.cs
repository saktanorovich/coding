using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1601 {
        public int MaximumRequests(int n, int[][] requests) {
            var bits = new int[1 << requests.Length];
            for (var set = 0; set < bits.Length; ++set) {
                bits[set] = bits[set >> 1] + (set & 1);
            }
            var answ = 0;
            for (var set = 1; set < bits.Length; ++set) {
                var balance = new int[n];
                for (var i = 0; i < requests.Length; ++i) {
                    if ((set & (1 << i)) != 0) {
                        var a = requests[i][0];
                        var b = requests[i][1];
                        balance[a]--;
                        balance[b]++;
                    }
                }
                if (balance.All(x => x == 0)) {
                    answ = Math.Max(answ, bits[set]);
                }
            }
            return answ;
        }
    }
}