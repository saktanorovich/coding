using System;
using System.Collections.Generic;
using System.Text;

namespace coding.leetcode {
    public class Solution_0560 {
        public int SubarraySum(int[] nums, int k) {
            var cumul = new Dictionary<int, int>();
            var summa = 0;
            var total = 0;
            cumul.Add(0, 1);
            foreach (var x in nums) {
                summa += x;
                if (cumul.TryGetValue(summa - k, out var count)) {
                    total += count;
                }
                if (cumul.ContainsKey(summa) == false) {
                    cumul[summa] = 0;
                }
                cumul[summa]++;
            }
            return total;
        }
    }
}
