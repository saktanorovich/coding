using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1617 {
        public int[] CountSubgraphsForEachDiameter(int n, int[][] edges) {
            var graph = new int[n, n];
            for (var i = 0; i < n; ++i) {
                for (var j = i + 1; j < n; ++j) {
                    graph[i, j] = n;
                    graph[j, i] = n;
                }
            }
            for (var i = 0; i < n - 1; ++i) {
                graph[edges[i][0] - 1, edges[i][1] - 1] = 1;
                graph[edges[i][1] - 1, edges[i][0] - 1] = 1;
            }
            for (var k = 0; k < n; ++k) {
                for (var i = 0; i < n; ++i) {
                    for (var j = 0; j < n; ++j) {
                        graph[i, j] = Math.Min(graph[i, j], graph[i, k] + graph[k, j]);
                    }
                }
            }
            var res = new int[n - 1];
            for (var set = 1; set < 1 << n; ++set) {
                var edgcount = bits(set) - 1;
                var diameter = 0;
                for (var i = 0; i < n; ++i) {
                    if (has(set, i)) {
                        for (var j = i + 1; j < n; ++j) {
                            if (has(set, j)) {
                                edgcount -= graph[i, j] == 1 ? 1 : 0;
                                if (diameter < graph[i, j]) {
                                    diameter = graph[i, j];
                                }
                            }
                        }
                    }
                }
                if (edgcount == 0 && diameter > 0) {
                    res[diameter - 1] += 1;
                }
            }
            return res;
        }

        private static bool has(int set, int x) {
            return (set & (1 << x)) != 0;
        }

        private static int bits(int set) {
            return set == 0 ? 0 : 1 + bits(set & (set - 1));
        }
    }
}
