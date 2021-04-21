using System;
using System.Collections.Generic;
using System.Text;

namespace coding.leetcode {
    public class Solution_0847 {
        public int ShortestPathLength(int[][] graph) {
            return ShortestPathLength(graph, graph.Length);
        }

        private int ShortestPathLength(int[][] graph, int n) {
            var best = new int[n, 1 << n];
            for (var i = 0; i < n; ++i) {
                for (var s = 0; s < 1 << n; ++s) {
                    best[i, s] = int.MaxValue;
                }
                best[i, 1 << i] = 0;
            }
            var queue = new Queue<int>();
            for (var i = 0; i < n; ++i) {
                queue.Enqueue(i);
                queue.Enqueue(1 << i);
            }
            while (queue.Count > 0) {
                var node = queue.Dequeue();
                var mask = queue.Dequeue();
                foreach (var next in graph[node]) {
                    if (best[next, mask | (1 << next)] > best[node, mask] + 1) {
                        best[next, mask | (1 << next)] = best[node, mask] + 1;
                        queue.Enqueue(next);
                        queue.Enqueue(mask | (1 << next));
                    }
                }
            }
            var res = int.MaxValue;
            for (var i = 0; i < n; ++i) {
                if (res > best[i, (1 << n) - 1]) {
                    res = best[i, (1 << n) - 1];
                }
            }
            return res;
        }
    }
}
