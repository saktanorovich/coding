using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0015 {
        public IList<IList<int>> ThreeSum(int[] nums) {
            Array.Sort(nums);
            var set = new HashSet<(int a, int b, int c)>();
            foreach (var res in ThreeSum(nums, nums.Length)) {
                set.Add(res);
            }
            return set.Select(t => (IList<int>)new[] { t.a, t.b, t.c }).ToList();
        }

        private IEnumerable<(int, int, int)> ThreeSum(int[] nums, int n) {
            for (var i = 0; i + 2 < n; ++i) {
                var l = i + 1;
                var r = n - 1;
                while (l < r) {
                    var sum = nums[i] + nums[l] + nums[r];
                    if (sum == 0) {
                        yield return (nums[i], nums[l], nums[r]);
                        ++l;
                        --r;
                    } else if (sum < 0) {
                        ++l;
                    } else if (sum > 0) {
                        --r;
                    }
                }
            }
        }
    }
}
