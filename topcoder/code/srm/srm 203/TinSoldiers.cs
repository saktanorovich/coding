using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopCoder.Algorithm {
    public class TinSoldiers {
        public int lineUps(int[] ranks) {
            // all lineUps can be splitted into two different classes
            //   (*) lineUps like abcde (call them r-lineUps)
            //   (*) lineUps like abcba (call them p-lineUps)
            // in order to find p-lineUps let's count the number of odd elements
            var rLineUps = a(ranks);
            var pLineUps = 0L;
            if (ranks.Select(r => r & 1).Sum() < 2) {
                pLineUps = a(ranks.Select(r => r / 2).ToArray());
            }
            rLineUps -= pLineUps;
            rLineUps /= 2;

            return (int)(rLineUps + pLineUps);
        }

        private static long a(int[] n) {
            var tot = n.Sum();
            var res = 1L;
            foreach (var ni in n) {
                res *= c(tot, ni);
                tot -= ni;
            }
            return res;
        }

        private static long c(int n, int k) {
            var res = 1L;
            for (var i = 1; i <= k; ++i) {
                res *= (n - i + 1);
                res /= i;
            }
            return res;
        }
    }
}
