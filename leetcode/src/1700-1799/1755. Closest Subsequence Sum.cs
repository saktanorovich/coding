using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1755 {
        public int MinAbsDifference(int[] nums, int goal) {
            var n = nums.Length;
            if (n > 1) {
                return MinAbsDifference(nums.Take(n / 2).ToArray(), nums.Skip(n / 2).ToArray(), goal);
            }
            var min = Math.Abs(goal);
            if (min > Math.Abs(goal - nums[0])) {
                min = Math.Abs(goal - nums[0]);
            }
            return min;
        }

        private int MinAbsDifference(int[] xs, int[] ys, int goal) {
            var x = build(xs);
            var y = build(ys);
            var res = Math.Abs(goal);
            for (int i = 0, j = y.Length - 1; i < x.Length && j >= 0;) {
                var sum = x[i] + y[j];
                if (sum > goal) {
                    res = Math.Min(res, sum - goal);
                    j = j - 1;
                    continue;
                }
                if (sum < goal) {
                    res = Math.Min(res, goal - sum);
                    i = i + 1;
                    continue;
                }
                return 0;
            }
            return res;
        }

        private int[] build(int[] xs) {
            var x = new int[1 << xs.Length];
            for (var i = 0; i < xs.Length; ++i) {
                x[1 << i] = xs[i];
            }
            for (var set = 1; set < x.Length; ++set) {
                var subset = set & (set - 1);
                if (subset > 0) {
                    x[set] = x[subset] + x[set ^ subset];
                }
            }
            Array.Sort(x);
            return x;
        }
    }
}
