using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0078 {
        public IList<IList<int>> Subsets(int[] nums) {
            var res = new IList<int>[1 << nums.Length];
            res[0] = new List<int>();
            for (var i = 0; i < nums.Length; ++i) {
                res[1 << i] = new List<int> { nums[i] };
            }
            for (var set = 1; set < 1 << nums.Length; ++set) {
                var last = set & (-set);
                if (last > 0) {
                    var has = new List<int>(res[last]);
                    has.AddRange(res[set ^ last]);
                    res[set] = has;
                }
            }
            return res;
        }
    }
}
