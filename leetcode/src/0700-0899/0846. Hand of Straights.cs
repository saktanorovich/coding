using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0846 {
        public bool IsNStraightHand(int[] hand, int W) {
            if (hand.Length % W != 0) {
                return false;
            }
            var dict = new SortedDictionary<int, int>();
            foreach (var x in hand) {
                dict.TryAdd(x, 0);
                dict[x]++;
            }
            var res = 0;
            foreach (var x in dict.Keys.ToArray()) {
                while (dict[x] > 0) {
                    for (var i = 0; i < W; ++i) {
                        if (dict.ContainsKey(x + i)) {
                            if (dict[x + i] > 0) {
                                dict[x + i]--;
                                continue;
                            }
                        }
                        return false;
                    }
                    res++;
                }
            }
            return res == hand.Length / W;
        }
    }
}
