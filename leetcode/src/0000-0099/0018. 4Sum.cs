using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0018 {
        public IList<IList<int>> FourSum(int[] nums, int target) {
            Array.Sort(nums);
            var set = new HashSet<(int a, int b, int c, int d)>();
            foreach (var res in FourSum(nums, nums.Length, target)) {
                set.Add(res);
            }
            return set.Select(t => (IList<int>)new[] { t.a, t.b, t.c }).ToList();
        }

        private IEnumerable<(int, int, int, int)> FourSum(int[] nums, int n, int target) {
            for (var i = 0; i + 3 < n; ++i) {
                for (var j = i + 1; j + 2 < n; ++j) {
                    var l = j + 1;
                    var r = n - 1;
                    while (l < r) {
                        var sum = nums[i] + nums[j] + nums[l] + nums[r];
                        if (sum == target) {
                            yield return (nums[i], nums[j], nums[l], nums[r]);
                            ++l;
                            --r;
                        } else if (sum < target) {
                            ++l;
                        } else if (sum > target) {
                            --r;
                        }
                    }
                }
            }
        }
    }
}
