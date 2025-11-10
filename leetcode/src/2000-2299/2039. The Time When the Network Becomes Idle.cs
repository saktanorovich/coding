using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_2039 {
        public int NetworkBecomesIdle(int[][] edges, int[] patience) {
            return NetworkBecomesIdle(edges, patience, patience.Length);
        }
        
        public int NetworkBecomesIdle(int[][] edges, int[] p, int n) {
            var g = new List<int>[n];
            for (var i = 0; i < n; ++i) {
                g[i] = new List<int>();
            }
            foreach (var e in edges) {
                g[e[0]].Add(e[1]);
                g[e[1]].Add(e[0]);
            }
            var t = new int[n];
            for (var i = 1; i < n; ++i) {
                t[i] = int.MaxValue;
            }
            var q = new Queue<int>();
            for (q.Enqueue(0); q.Count > 0;) {
                var u = q.Dequeue();
                foreach (var v in g[u]) {
                    if (t[v] > t[u] + 1) {
                        t[v] = t[u] + 1;
                        q.Enqueue(v);
                    }
                }
            }
            var res = 0;
            for (var i = 1; i < n; ++i) {
                res = Math.Max(res, 2 * t[i] + (2 * t[i] - 1) / p[i] * p[i]);   
            }
            return res + 1;
        }
    }
}