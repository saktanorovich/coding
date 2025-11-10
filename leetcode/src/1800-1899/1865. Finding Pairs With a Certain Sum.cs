using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1865 {
        public class FindSumPairs {
            private readonly Dictionary<int, int> nums;
            private readonly int[] nums1;
            private readonly int[] nums2;

            public FindSumPairs(int[] nums1, int[] nums2) {
                this.nums1 = nums1;
                this.nums2 = nums2;
                this.nums = new Dictionary<int, int>();
                foreach (var num in nums2) {
                    this.nums.TryAdd(num, 0);
                    this.nums[num]++;
                }
            }

            public void Add(int index, int val) {
                var have = nums2[index];
                if (nums.ContainsKey(have)) {
                    nums[have]--;
                }
                nums2[index] += val;
                have += val; 
                nums.TryAdd(have, 0);
                nums[have]++;
            }

            public int Count(int tot) {
                var res = 0;
                foreach (var num in nums1) {
                    if (nums.TryGetValue(tot - num, out var cnt)) {
                        res += cnt;
                    }
                }
                return res;
            }
        }
    }
}
