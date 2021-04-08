using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0698 {
        public bool CanPartitionKSubsets(int[] nums, int k) {
            var sum = nums.Sum();
            if (sum % k != 0) {
                return false;
            }
            return okay2(nums, sum / k);
        }

        // O(n*2^n)
        private bool okay2(int[] nums, int sum) {
            var t = new int[1 << nums.Length];
            for (var i = 0; i < nums.Length; ++i) {
                t[1 << i] = nums[i];
            }
            for (var set = 1; set < 1 << nums.Length; ++set) {
                var last = set & (-set);
                if (last > 0) {
                    t[set] = t[set ^ last] + t[last];
                }
            }
            var f = new bool[1 << nums.Length];
            f[0] = true;
            for (var set = 1; set < 1 << nums.Length; ++set) {
                var have = t[set] % sum;
                if (have == 0) {
                    have = sum;
                }
                for (var i = 0; i < nums.Length; ++i) {
                    if ((set & (1 << i)) != 0) {
                        if (nums[i] <= have) {
                            f[set] |= f[set ^ (1 << i)];
                        }
                    }
                }
            }
            return f[(1 << nums.Length) - 1];
        }

        // O(3^n)
        private bool okay3(int[] nums, int sum) {
            var t = new int[1 << nums.Length];
            for (var i = 0; i < nums.Length; ++i) {
                t[1 << i] = nums[i];
            }
            var f = new bool[1 << nums.Length];
            for (var set = 1; set < 1 << nums.Length; ++set) {
                var last = set & (-set);
                if (last > 0) {
                    t[set] = t[set ^ last] + t[last];
                }
                if (t[set] == sum) {
                    f[set] = true;
                }
            }
            for (var set = 1; set < 1 << nums.Length; ++set) {
                var sub = set & (set - 1);
                while (sub > 0) {
                    f[set] |= f[set ^ sub] & f[sub];
                    if (f[set]) {
                        break;
                    }
                    sub = set & (sub - 1);
                }
            }
            return f[(1 << nums.Length) - 1];
        }
    }
}
