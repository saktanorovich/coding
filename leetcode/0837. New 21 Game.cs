using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0837 {
        public double New21Game(int n, int k, int maxPts) {
            var prob = new double[n + 1];
            prob[0] = 1;
            for (var have = 0; have < k; ++have) {
                for (var take = 1; take <= maxPts; ++take) {
                    var summ = have + take;
                    if (summ <= n) {
                        prob[summ] += prob[have] * 1.0 / maxPts;
                    }
                    else break;
                }
            }
            return prob.Skip(k).Sum();
        }
    }
}
