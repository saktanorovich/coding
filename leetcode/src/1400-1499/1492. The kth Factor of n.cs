using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1492 {
        public int KthFactor(int n, int k) {
            var factors = GetFactors(n);
            factors = factors.Skip(k - 1);
            if (factors.Any()) {
                return factors.First();
            }
            return -1;
        }

        private IEnumerable<int> GetFactors(int n) {
            for (var i = 1; i <= n; ++i) {
                if (n % i == 0) {
                    yield return i;
                }
            }
        }
    }
}
