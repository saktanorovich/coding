using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace coding.leetcode {
    public class Solution_1462 {
        public IList<bool> CheckIfPrerequisite(int n, int[][] prerequisites, int[][] queries) {
            var graph = new bool[n, n];
            foreach (var prerequisite in prerequisites) {
                graph[prerequisite[0], prerequisite[1]] = true;
            }
            for (var k = 0; k < n; ++k) {
                for (var i = 0; i < n; ++i) {
                    for (var j = 0; j < n; ++j) {
                        graph[i, j] |= graph[i, k] && graph[k, j];
                    }
                }
            }
            var result = new List<bool>();
            foreach (var query in queries) {
                result.Add(graph[query[0], query[1]]);
            }
            return result;
        }
    }
}
