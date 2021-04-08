using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0637 {
        public int FindNumberOfLIS(int[] nums) {
            var lis = new int[nums.Length];
            var cnt = new int[nums.Length];
            var max = 0;
            for (var i = 0; i < nums.Length; ++i) {
                lis[i] = 1;
                cnt[i] = 1;
                for (var j = i - 1; j >= 0; --j) {
                    if (nums[i] > nums[j]) {
                        if (lis[i] > lis[j] + 1) {
                            continue;
                        }
                        if (lis[i] < lis[j] + 1) {
                            lis[i] = lis[j] + 1;
                            cnt[i] = cnt[j];
                        } else {
                            cnt[i] = cnt[i] + cnt[j];
                        }
                    }
                }
                max = Math.Max(max, lis[i]);
            }
            var res = 0;
            for (var i = 0; i < nums.Length; ++i) {
                if (lis[i] == max) {
                    res += cnt[i];
                }
            }
            return res;
        }
    }
}
