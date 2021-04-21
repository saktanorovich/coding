using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1282 {
        public IList<IList<int>> GroupThePeople(int[] groupSizes) {
            var order = Enumerable.Range(0, groupSizes.Length).ToArray();
            Array.Sort(order, (a, b) => groupSizes[a] - groupSizes[b]);
            var group = new List<IList<int>>();
            for (IEnumerable<int> list = order; list.Any();) {
                var size = groupSizes[list.First()];
                group.Add(list.Take(size).ToArray());
                list = list.Skip(size);
            }
            return group;
        }
    }
}
