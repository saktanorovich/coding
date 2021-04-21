using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1655 {
        public bool CanDistribute(int[] nums, int[] quantity) {
            var freq = new Dictionary<int, int>();
            foreach (var x in nums) {
                freq.TryAdd(x, 0);
                freq[x] ++;
            }
            return distribute(freq.Values.ToArray(), quantity);
        }

        private bool distribute(int[] freq, int[] quantity) {
            var need = new int[1 << quantity.Length];
            for (var set = 1; set < 1 << quantity.Length; ++set) {
                for (var i = 0; i < quantity.Length; ++i) {
                    if ((set & (1 << i)) != 0) {
                        need[set] += quantity[i];
                    }
                }
            }
            var memo = new bool[1 << quantity.Length, freq.Length];
            for (var set = 1; set < 1 << quantity.Length; ++set) {
                if (need[set] <= freq[0]) {
                    memo[set, 0] = true;
                }
            }
            for (var i = 0; i < freq.Length; ++i) {
                memo[0, i] = true;
            }
            for (var i = 1; i < freq.Length; ++i) {
                for (var set = 1; set < 1 << quantity.Length; ++set) {
                    if (memo[set, i - 1]) {
                        memo[set, i] = true;
                        continue;
                    }
                    for (var subset = set; subset > 0; subset = (subset - 1) & set) {
                        if (need[subset] <= freq[i]) {
                            memo[set, i] |= memo[set ^ subset, i - 1];
                        }
                    }
                }
            }
            for (var i = 0; i < freq.Length; ++i) {
                if (memo[(1 << quantity.Length) - 1, i]) {
                    return true;
                }
            }
            return false;
        }
    }
}
