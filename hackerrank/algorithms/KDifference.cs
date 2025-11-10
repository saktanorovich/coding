using System;

namespace interview.hackerrank {
    public class KDifference {
        public int count(int n, int k, int[] a) {
            Array.Sort(a);
            var result = 0;
            for (int lo = 0, hi = 0; lo <= hi;) {
                var diff = a[hi] - a[lo];
                if (diff == k) {
                    result = result + 1;
                    lo = lo + 1;
                }
                else if (diff < k) {
                    if (hi + 1 < n) {
                        hi = hi + 1;
                        continue;
                    }
                    lo = lo + 1;
                }
                else if (diff > k) {
                    lo = lo + 1;
                }
            }
            return result;
        }
    }
}
