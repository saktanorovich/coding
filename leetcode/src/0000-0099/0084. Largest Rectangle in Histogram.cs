using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0084 {
        public int LargestRectangleArea(int[] heights) {
            var h = new List<int>();
            h.Add(0);
            h.AddRange(heights);
            h.Add(0);
            var res = 0;
            var sta = new Stack<int>();
            for (var ri = 0; ri < h.Count; ++ri) {
                while (sta.Count > 0) {
                    var le = sta.Pop();
                    if (h[le] > h[ri]) {
                        res = Math.Max(res, h[le] * (ri - (sta.Peek() + 1)));
                    } else {
                        sta.Push(le);
                        break;
                    }
                }
                sta.Push(ri);
            }
            return res;
        }
    }
}
