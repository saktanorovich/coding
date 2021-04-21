using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1879 {
        public int MinimumXORSum(int[] nums1, int[] nums2) {
            return MinimumXORSum(nums1, nums2, nums2.Length);
        }

        private int MinimumXORSum(int[] nums1, int[] nums2, int n) {
            var best = new int[n, 1 << n];
            for (var i = 0; i < n; ++i) {
                best[0, 1 << i] = nums1[0] ^ nums2[i];
            }
            for (var i = 1; i < n; ++i) {
                for (var set = 0; set < 1 << n; ++set) {
                    best[i, set] = int.MaxValue;
                    if (cnt(set) == i + 1) {
                        for (var j = 0; j < n; ++j) {
                            if (has(set, j) == false) {
                                var eval = best[i - 1, set ^ (1 << j)] + (nums1[i] ^ nums2[j]);
                                if (best[i, set] > eval) {
                                    best[i, set] = eval;
                                }
                            }
                        }
                    }
                }
            }
            return best[n - 1, (1 << n) - 1];
        }

        private static bool has(int set, int x) {
            return (set & (1 << x)) == 0;
        }

        private static int cnt(int set) {
            return set == 0 ? 0 : 1 + cnt(set & (set - 1));
        }
    }
}
