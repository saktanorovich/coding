using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1296 {
        public bool IsPossibleDivide(int[] nums, int k) {
            if (nums.Length % k != 0) {
                return false;
            }
            var dict = new SortedDictionary<int, int>();
            foreach (var x in nums) {
                dict.TryAdd(x, 0);
                dict[x]++;
            }
            var res = 0;
            foreach (var x in dict.Keys.ToArray()) {
                while (dict[x] > 0) {
                    for (var i = 0; i < k; ++i) {
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
            return res == nums.Length / k;
        }
    }
}
