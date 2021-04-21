using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0547 {
        public int FindCircleNum(int[][] isConnected) {
            var use = new bool[isConnected.Length];
            var res = 0;
            for (var i = 0; i < isConnected.Length; ++i) {
                if (use[i] == false) {
                    dfs(isConnected, i, use);
                    res++;
                }
            }
            return res;
        }

        private void dfs(int[][] graph, int src, bool[] use) {
            use[src] = true;
            for (var dst = 0; dst < graph.Length; ++dst) {
                if (graph[src][dst] > 0) {
                    if (use[dst] == false) {
                        dfs(graph, dst, use);
                    }
                }
            }
        }
    }
}
