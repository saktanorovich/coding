using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1515 {
        public double GetMinDistSum(int[][] ps) {
            return GetMinDistSum(ps.Select(p => p[0]).ToArray(), ps.Select(p => p[1]).ToArray(), ps.Length);
        }

        private double GetMinDistSum(int[] xs, int[] ys, int n) {
            return TernarySearch(0, 100, x => {
                return TernarySearch(0, 100, y => {
                    var s = 0.0;
                    for (var i = 0; i < n; ++i) {
                        s += Math.Sqrt((x - xs[i]) * (x - xs[i]) + (y - ys[i]) * (y - ys[i]));
                    }
                    return s;
                });
            });
        }

        private double TernarySearch(double lo, double hi, Func<double, double> f) {
            while (lo + 1e-6 < hi) {
                var lo3 = lo + (hi - lo) / 3;
                var hi3 = hi - (hi - lo) / 3;
                if (f(lo3) > f(hi3) + 1e-6) {
                    lo = lo3;
                } else {
                    hi = hi3;
                }
            }
            return f((lo + hi) / 2);
        }
    }
}
