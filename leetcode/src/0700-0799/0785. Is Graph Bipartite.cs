using System;
using System.Collections.Generic;
using System.Text;

namespace coding.leetcode {
    public class Solution_0785 {
        public bool IsBipartite(int[][] graph) {
            var mark = new int[graph.Length];
            for (var i = 0; i < graph.Length; ++i) {
                mark[i] = -1;
            }
            for (var i = 0; i < graph.Length; ++i) {
                if (mark[i] == -1) {
                    if (!dfs(graph, i, mark, 0)) {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool dfs(int[][] graph, int curr, int[] mark, int val) {
            mark[curr] = val;
            foreach (var next in graph[curr]) {
                if (mark[next] == -1) {
                    if (!dfs(graph, next, mark, 1 - val)) {
                        return false;
                    }
                } else if (mark[next] != 1 - val) {
                    return false;
                }
            }
            return true;
        }
    }
}
