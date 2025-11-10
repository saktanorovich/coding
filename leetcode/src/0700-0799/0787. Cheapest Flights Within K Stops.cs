using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0787 {
        public int FindCheapestPrice(int n, int[][] flights, int src, int dst, int k) {
            var g = new int[n, n];
            for (var i = 0; i < n; ++i) {
                for (var j = 0; j < n; ++j) {
                    g[i, j] = int.MaxValue;
                }
            }
            foreach (var e in flights) {
                g[e[0], e[1]] = e[2];
            }
            var best = new int[n, k + 2];
            for (var i = 0; i < n; ++i) {
                for (var j = 0; j <= k + 1; ++j) {
                    best[i, j] = int.MaxValue;
                }
            }
            best[src, 0] = 0;
            var q = new Queue<int>();
            q.Enqueue(src);
            q.Enqueue(0);
            while (q.Count > 0) {
                var curr = q.Dequeue();
                var have = q.Dequeue();
                if (have == k + 1) {
                    continue;
                }
                for (var next = 0; next < n; ++next) {
                    if (g[curr, next] < int.MaxValue) {
                        if (best[next, have + 1] > best[curr, have] + g[curr, next]) {
                            best[next, have + 1] = best[curr, have] + g[curr, next];
                            q.Enqueue(next);
                            q.Enqueue(have + 1);
                        }
                    }
                }
            }
            var res = int.MaxValue;
            for (var t = 0; t <= k + 1; ++t) {
                res = Math.Min(res, best[dst, t]);
            }
            return res < int.MaxValue ? res : -1;
        }
    }
}
