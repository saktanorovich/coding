using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1590 {
        public int MinSubarray(int[] nums, int p) {
            var rem = 0;
            foreach (var x in nums) {
                rem += x;
                rem %= p;
            }
            if (rem == 0) {
                return 0;
            }
            var ind = new Dictionary<int, int> {
                [0] = -1
            };
            var sum =  0;
            var res = -1;
            for (var i = 0; i < nums.Length; ++i) {
                sum += nums[i];
                sum %= p;
                var exp = sum - rem;
                exp += p;
                exp %= p;
                if (ind.TryGetValue(exp, out var j)) {
                    if (res == -1 || i - j < res) {
                        res = i - j;
                    }
                }
                ind[sum] = i;
            }
            return res < nums.Length ? res : -1;
        }
    }
}
