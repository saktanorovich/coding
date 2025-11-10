using System;
using System.Collections.Generic;
using System.Text;

namespace coding.leetcode {
    public class Solution_0802 {
        public IList<int> EventualSafeNodes(int[][] graph) {
            var mark = new int[graph.Length];
            var safe = new List<int>();
            for (var i = 0; i < graph.Length; ++i) {
                if (dfs(graph, i, mark)) {
                    safe.Add(i);
                }
            }
            return safe;
        }

        private bool dfs(int[][] graph, int curr, int[] mark) {
            if (mark[curr] > 1) {
                return true;
            }
            mark[curr] = 1;
            foreach (var next in graph[curr]) {
                if (mark[next] == 0) {
                    if (!dfs(graph, next, mark)) {
                        return false;
                    }
                }
                if (mark[next] == 1) {
                    return false;
                }
            }
            mark[curr] = 2;
            return true;
        }
    }
}
