using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0996 {
        public int NumSquarefulPerms(int[] nums) {
            var freq = new Dictionary<int, int>();
            foreach (var x in nums) {
                freq.TryAdd(x, 0);
                freq[x] ++;
            }
            var memo = new int[1 << nums.Length, nums.Length];
            for (var i = 0; i < nums.Length; ++i) {
                memo[1 << i, i] = 1;
            }
            for (var set = 1; set < 1 << nums.Length; ++set) {
                for (var last = 0; last < nums.Length; ++last) {
                    if ((set & (1 << last)) != 0) {
                        for (var next = 0; next < nums.Length; ++next) {
                            if ((set & (1 << next)) == 0) {
                                if (last != next) {
                                    if (IsSquare(nums[last] + nums[next])) {
                                        memo[set | (1 << next), next] += memo[set, last];
                                    }
                                }
                            }
                        }
                    }
                }
            }
            var answ = 0;
            for (var i = 0; i < nums.Length; ++i) {
                answ += memo[(1 << nums.Length) - 1, i];
            }
            foreach (var t in freq.Values) {
                for (var k = 2; k <= t; ++k) {
                    answ /= k;
                }
            }
            return answ;
        }

        private bool IsSquare(int x) {
            var y = (int)Math.Sqrt(x);
            return y * y == x;
        }
    }
}
