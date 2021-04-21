using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0575 {
        public int DistributeCandies(int[] candyType) {
            var candies = new HashSet<int>();
            foreach (var candy in candyType) {
                candies.Add(candy);
            }
            return Math.Min(candies.Count, candyType.Length / 2);
        }
    }
}
