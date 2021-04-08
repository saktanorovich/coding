using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0278 : VersionControl {
        public int FirstBadVersion(int n) {
            int lo = 1, hi = n;
            while (lo < hi) {
                var x = lo + (hi - lo) / 2;
                if (IsBadVersion(x)) {
                    hi = x;
                } else {
                    lo = x + 1;
                }
            }
            return lo;
        }

        public bool IsBadVersion(int x) {
            throw new NotImplementedException();
        }
    }

    public interface VersionControl {
        bool IsBadVersion(int x);
    }
}
