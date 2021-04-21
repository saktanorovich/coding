using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class UnfixedArrangements {
        public long count(int n, int k) {
            memo = new long[1 << n];
            for (var i = 1; i < 1 << n; ++i) {
                memo[i] = -1;
            }
            return run(n, k, (1 << n) - 1, 0);
        }

        private long run(int n, int k, int set, int x) {
            if (x == k) {
                return 1;
            }
            if (memo[set] == -1) {
                memo[set] = 0;
                for (var i = 0; i < n; ++i) {
                    if (i != x && (set & (1 << i)) != 0) {
                        memo[set] += run(n, k, set ^ (1 << i), x + 1);
                    }
                }
            }
            return memo[set];
        }

        private long[] memo;
    }
}
