using System;
using System.Collections.Generic;
using System.Text;

namespace coding.leetcode {
    public class Solution_0215 {
        public int FindKthLargest(int[] nums, int k) {
            return FindKthSmallest(nums, nums.Length - k + 1);
        }

        public int FindKthSmallest(IList<int> nums, int k) {
            var pivot = nums[nums.Count / 2];
            var a1 = new List<int>();
            var a2 = new List<int>();
            for (var i = 0; i < nums.Count; ++i) {
                if (nums[i] < pivot) a1.Add(nums[i]);
                if (nums[i] > pivot) a2.Add(nums[i]);
            }
            if (k <= a1.Count) {
                return FindKthSmallest(a1, k);
            }
            if (k > nums.Count - a2.Count) {
                return FindKthSmallest(a2, k - (nums.Count - a2.Count));
            }
            return pivot;
        }
    }
}
