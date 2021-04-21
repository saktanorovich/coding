using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0632 {
        public int[] SmallestRange(IList<IList<int>> nums) {
            var indx = new int[nums.Count];
            var heap = new SortedSet<int>(
                Comparer<int>.Create((a, b) => {
                    var x = nums[a][indx[a]];
                    var y = nums[b][indx[b]];
                    if (x != y) {
                        return x - y;
                    } else {
                        return a - b;
                    }
                }));
            var rmin = int.MaxValue;
            var rmax = int.MinValue;
            for (var i = 0; i < nums.Count; ++i) {
                heap.Add(i);
                rmin = Math.Min(rmin, nums[i][0]);
                rmax = Math.Max(rmax, nums[i][0]);
            }
            var cmin = rmin;
            var cmax = rmax;
            while (true) {
                var imin = heap.Min;
                cmin = nums[imin][indx[imin]];
                if (rmax - rmin > cmax - cmin) {
                    rmax = cmax;
                    rmin = cmin;
                }
                heap.Remove(imin);
                indx[imin]++;
                if (indx[imin] < nums[imin].Count) {
                    heap.Add(imin);
                    if (cmax < nums[imin][indx[imin]]) {
                        cmax = nums[imin][indx[imin]];
                    }
                } else {
                    // no further processing needed because remaining
                    // elements are larger than the current minimum
                    break;
                }
            }
            return new[] { rmin, rmax };
        }
    }
}
