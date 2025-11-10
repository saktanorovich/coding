using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1248 {
        public int NumberOfSubarrays(int[] nums, int k) {
            var cnt = new int[nums.Length + 1];
            var sum = new int[nums.Length + 1];
            var res = 0;
            cnt[0]  = 1;
            for (var i = 1; i <= nums.Length; ++i) {
                sum[i] = sum[i - 1] + (nums[i - 1] & 1);
                cnt[sum[i]] ++;
                if (sum[i] >= k) {
                    res += cnt[sum[i] - k];
                }
            }
            return res;
        }
    }
}
