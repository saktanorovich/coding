using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_2521 {
        public int DistinctPrimeFactors(int[] nums) {
            var res = new HashSet<int>();
            foreach (var num in nums) {
                var x = num;
                for (var p = 2; p * p <= x; ++p) {
                    while (x % p == 0) {
                        res.Add(p);
                        x /= p;
                    }
                }
                if (x > 1) res.Add(x);
            }
            return res.Count;
        }
    }
}