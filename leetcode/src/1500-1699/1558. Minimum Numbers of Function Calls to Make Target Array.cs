using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1558 {
        public int MinOperations(int[] nums) {
            var res = 0;
            while (true) {
                var cn1 = op1(nums);
                if (cn1 > 0) {
                    res += cn1;
                    continue;
                }
                var cn2 = op2(nums);
                if (cn2 > 0) {
                    res += cn2;
                    continue;
                }
                break;
            }
            return res;
        }

        private int op1(int[] nums) {
            var res = 0;
            for (var i = 0; i < nums.Length; ++i) {
                if (nums[i] % 2 == 1) {
                    nums[i]--;
                    res++;
                }
            }
            return res;
        }

        private int op2(int[] nums) {
            var res = 0;
            for (var i = 0; i < nums.Length; ++i) {
                if (nums[i] >= 2) {
                    nums[i] /= 2;
                    res = 1;
                }
            }
            return res;
        }
    }
}
