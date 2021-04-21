using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0834 {
        public int[] SumOfDistancesInTree(int N, int[][] edges) {
            tr = new List<int>[N];
            for (var i = 0; i < N; ++i) {
                tr[i] = new List<int>();
            }
            foreach (var e in edges) {
                tr[e[0]].Add(e[1]);
                tr[e[1]].Add(e[0]);
            }
            sum = new int[N];
            res = new int[N];
            dfs1(0, -1);
            dfs2(0, -1);
            return res;
        }

        // sum of distances from the root to internal nodes
        private void dfs1(int root, int prev) {
            sum[root] = 1;
            foreach (var next in tr[root]) {
                if (next != prev) {
                    dfs1(next, root);
                    sum[root] += sum[next];
                    res[root] += res[next] + sum[next];
                }
            }
        }

        // we know total sum for the root, adjust internals
        private void dfs2(int root, int prev) {
            foreach (var next in tr[root]) {
                if (next != prev) {
                    res[next] += res[root] - res[next] - sum[next];
                    res[next] += tr.Length - sum[next];
                    dfs2(next, root);
                }
            }
        }

        private List<int>[] tr;
        private int[] sum;
        private int[] res;
    }
}
