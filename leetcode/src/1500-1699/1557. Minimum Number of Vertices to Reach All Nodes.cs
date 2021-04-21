using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1557 {
        public IList<int> FindSmallestSetOfVertices(int n, IList<IList<int>> edges) {
            var deg = new int[n];
            foreach (var edge in edges) {
                deg[edge[1]]++;
            }
            var res = new List<int>();
            for (var i = 0; i < n; ++i) {
                if (deg[i] == 0) {
                    res.Add(i);
                }
            }
            return res;
        }
    }
}
