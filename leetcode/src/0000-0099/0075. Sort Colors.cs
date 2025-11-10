using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0075 {
        public void SortColors(int[] nums) {
            var i0 = 0;
            var i1 = 0;
            var i2 = nums.Length - 1;
            while (i0 <= i2) {
                if (nums[i0] == 2) {
                    nums[i0] = nums[i2];
                    nums[i2] = 2;
                    i2--;
                } else if (nums[i0] == 1) {
                    i0++;
                } else {
                    nums[i0] = nums[i1];
                    nums[i1] = 0;
                    i0++;
                    i1++;
                }
            }
        }
    }
}
