using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class NumberPartition {
        public int[] kthPartition(int n, int k) {
            memo = new int[n + 1, n + 1];
            for (var i = 0; i <= n; ++i) {
                for (var j = 1; j <= n; ++j) {
                    memo[i, j] = -1;
                }
                memo[i, i] = 1;
            }
            var result = new List<int>();
            if (k <= count(n)) {
                for (var curr = 1; n > 0;) {
                    for (var next = curr; next <= n; ++next) {
                        if (count(n, next) >= k) {
                            curr = next;
                            break;
                        }
                        k -= count(n, next);
                    }
                    result.Add(curr);
                    n -= curr;
                }
            }
            return result.ToArray();
        }

        private int[,] memo;

        private int count(int n) {
            var result = 0;
            for (var last = 1; last <= n; ++last) {
                result += count(n, last);
            }
            return result;
        }

        private int count(int n, int last) {
            if (n < 0) {
                return 0;
            }
            if (memo[n, last] == -1) {
                memo[n, last] = 0;
                for (var next = last; next <= n; ++next) {
                    memo[n, last] += count(n - last, next);
                }
            }
            return memo[n, last];
        }
    }
}