using System;
using System.Collections.Generic;
using System.Text;

namespace coding.leetcode {
    public class Solution_0312 {
        public int MaxCoins(int[] nums) {
            if (nums.Length > 0) {
                var balloons = new List<int>();
                balloons.Add(1);
                balloons.AddRange(nums);
                balloons.Add(1);
                return MaxCoins(balloons, 1, nums.Length);
            }
            return 0;
        }

        private int MaxCoins(List<int> nums, int L, int R) {
            var best = new int[nums.Count, nums.Count];
            for (var k = L; k <= R; ++k) {
                for (var j = k; j <= R; ++j) {
                    var i = j - k + 1;
                    for (var x = i; x <= j; ++x) {
                        var eval = best[i, x - 1] + nums[i - 1] * nums[x] * nums[j + 1] + best[x + 1, j];
                        if (best[i, j] < eval) {
                            best[i, j] = eval;
                        }
                    }
                }
            }
            return best[L, R];
        }
    }
}
