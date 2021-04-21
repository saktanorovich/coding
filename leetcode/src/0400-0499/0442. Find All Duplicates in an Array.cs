using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0442 {
        public IList<int> FindDuplicates(int[] nums) {
            var res = new List<int>();
            for (var i = 0; i < nums.Length; ++i) {
                var x = Math.Abs(nums[i]);
                if (nums[x - 1] > 0) {
                    nums[x - 1] *= -1;
                } else {
                    res.Add(x);
                }
            }
            return res;
            /*
            var res = new HashSet<int>();
            for (var i = 0; i < nums.Length; ++i) {
                while (nums[i] - 1 != i) {
                    var x = nums[i] - 1;
                    var t = nums[x] - 1;
                    if (t != x) {
                        nums[x] = x + 1;
                        nums[i] = t + 1;
                    } else {
                        res.Add(t + 1);
                        break;
                    }
                }
            }
            return res.ToList();
            */
        }
    }
}
