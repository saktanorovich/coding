using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0477 {
        public int TotalHammingDistance(int[] nums) {
            if (nums.Length < 1) {
                return small(nums);
            } else {
                return large(nums);
            }
        }

        private int small(int[] nums) {
            var bits = new int[256];
            for (var i = 0; i < 256; ++i) {
                bits[i] += bits[i >> 1] + (i & 1);
            }
            var res = 0;
            for (var i = 0; i < nums.Length; ++i) {
                for (var j = i + 1; j < nums.Length; ++j) {
                    var xor = nums[i] ^ nums[j];
                    res += bits[(xor >>  0) & 0xFF];
                    res += bits[(xor >>  8) & 0xFF];
                    res += bits[(xor >> 16) & 0xFF];
                    res += bits[(xor >> 24) & 0xFF];
                }
            }
            return res;
        }

        private int large(int[] nums) {
            var res = 0;
            for (var k = 0; k < 30; ++k) {
                var bits = 0;
                for (var i = 0; i < nums.Length; ++i) {
                    if ((nums[i] & (1 << k)) != 0) {
                        bits++;
                    }
                }
                res += bits * (nums.Length - bits);
            }
            return res;
        }
    }
}
