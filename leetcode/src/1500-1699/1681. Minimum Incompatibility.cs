using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1681 {
        public int MinimumIncompatibility(int[] nums, int k) {
            return MinimumIncompatibility(nums, nums.Length, nums.Length / k, k);
        }

        private int MinimumIncompatibility(int[] nums, int n, int m, int k) {
            var sets = new List<int>();
            var cost = new List<int>();
            for (var set = 1; set < 1 << n; ++set) {
                if (bits(set) == m) {
                    var vals = new HashSet<int>();
                    for (var i = 0; i < n; ++i) {
                        if ((set & (1 << i)) != 0) {
                            vals.Add(nums[i]);
                        }
                    }
                    if (vals.Count == m) {
                        sets.Add(set);
                        cost.Add(vals.Max() - vals.Min());
                    }
                }
            }
            var best = new int[1 << n];
            for (var set = 1; set < 1 << n; ++set) {
                best[set] = int.MaxValue / 2;
            }
            for (var set = 1; set < 1 << n; ++set) {
                for (var i = 0; i < sets.Count; ++i) {
                    if ((set & sets[i]) == sets[i]) {
                        if (best[set] > best[set ^ sets[i]] + cost[i]) {
                            best[set] = best[set ^ sets[i]] + cost[i];
                        }
                    }
                }
            }
            return best[(1 << n) - 1] < int.MaxValue / 2 ? best[(1 << n) - 1] : -1;
        }

        private static int bits(int set) {
            return set > 0 ? 1 + bits(set & (set - 1)) : 0;
        }
    }
}
