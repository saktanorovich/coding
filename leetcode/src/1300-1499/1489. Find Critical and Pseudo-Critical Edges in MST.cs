using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1489 {
        public IList<IList<int>> FindCriticalAndPseudoCriticalEdges(int n, int[][] edges) {
            var ord = new int[edges.Length];
            for (var i = 0; i < edges.Length; ++i) {
                ord[i] = i;
            }
            Array.Sort(ord, (a, b) => {
                return edges[a][2] - edges[b][2];
            });
            var res = new List<IList<int>> {
                new List<int>(),
                new List<int>()
            };
            var opt = mst(edges, ord, n, -1, -1);
            for (var i = 0; i < edges.Length; ++i) {
                if (mst(edges, ord, n, -1, i) > opt) {
                    res[0].Add(i);
                    continue;
                }
                if (mst(edges, ord, n, i, i) == opt) {
                    res[1].Add(i);
                }
            }
            return res;
        }

        private int mst(int[][] edges, int[] ord, int n, int pick, int skip) {
            var dsu = new DSU(n);
            var res = 0;
            var edg = 0;
            if (pick != -1) {
                var a = edges[pick][0];
                var b = edges[pick][1];
                var w = edges[pick][2];
                dsu.union(a, b);
                res += w;
                edg ++;
            }
            for (var i = 0; i < edges.Length; ++i) {
                if (ord[i] != skip) {
                    var a = edges[ord[i]][0];
                    var b = edges[ord[i]][1];
                    var w = edges[ord[i]][2];
                    if (dsu.union(a, b)) {
                        res += w;
                        edg ++;
                    }
                }
            }
            if (edg == n - 1) {
                return res;
            } else {
                return int.MaxValue;
            }
        }

        private sealed class DSU {
            private int[] leader;
            private int[] weight;

            public DSU(int n) {
                leader = new int[n];
                weight = new int[n];
                for (var i = 0; i < n; ++i) {
                    leader[i] = i;
                    weight[i] = 1;
                }
            }

            public bool union(int a, int b) {
                a = find(a);
                b = find(b);
                if (a != b) {
                    join(a, b);
                    return true;
                }
                return false;
            }

            private int find(int a) {
                if (a == leader[a]) {
                    return a;
                }
                leader[a] = find(leader[a]);
                return leader[a];
            }

            private void join(int a, int b) {
                if (weight[a] < weight[b]) {
                    leader[a] = b;
                    weight[b] += weight[a];
                } else {
                    leader[b] = a;
                    weight[a] += weight[b];
                }
            }
        }
    }
}
