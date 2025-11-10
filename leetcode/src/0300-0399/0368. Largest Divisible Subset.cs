using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0368 {
        public IList<int> LargestDivisibleSubset(int[] nums) {
            Array.Sort(nums);
            var best = new int[nums.Length];
            var prev = new int[nums.Length];
            for (var i = 0; i < nums.Length; ++i) {
                best[i] = +1;
                prev[i] = -1;
                for (var j = 0; j < i; ++j) {
                    if (nums[i] % nums[j] == 0) {
                        if (best[i] < best[j] + 1) {
                            best[i] = best[j] + 1;
                            prev[i] = j;
                        }
                    }
                }
            }
            var max = best.Max();
            for (var i = 0; i < nums.Length; ++i) {
                if (best[i] == max) {
                    var set = new List<int>();
                    for (var x = i; x >= 0;) {
                        set.Add(nums[x]);
                        x = prev[x];
                    }
                    return set;
                }
            }
            throw new InvalidOperationException();
        }
    }
}
