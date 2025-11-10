using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_2608 {
        public int FindShortestCycle(int n, int[][] edges) {
            var graph = new List<int>[n];
            for (var i = 0; i < n; ++i) {
                graph[i] = new List<int>();
            }
            foreach (var e in edges) {
                graph[e[0]].Add(e[1]);
                graph[e[1]].Add(e[0]);
            }
            var res = int.MaxValue;
            for (var u = 0; u < n; ++u) {
                var dist = new int[n];
                var from = new int[n];
                for (var i = 0; i < n; ++i) {
                    dist[i] = -1;
                    from[i] = -1;
                }
                dist[u] = 0;
                var q = new Queue<int>();
                for (q.Enqueue(u); q.Count > 0;) {
                    var curr = q.Dequeue();
                    foreach (var next in graph[curr]) {
                        if (dist[next] == -1) {
                            dist[next] = dist[curr] + 1;
                            from[next] = curr;
                            q.Enqueue(next);
                        }
                        else if (from[curr] != next) {
                            res = Math.Min(res, dist[curr] + dist[next] + 1);
                        }
                    }
                }
            }
            return res < int.MaxValue ? res : -1;
        }
    }
}
