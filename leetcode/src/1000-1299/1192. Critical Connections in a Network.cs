using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1192 {
        public IList<IList<int>> CriticalConnections(int n, IList<IList<int>> conn) {
            this.g = new List<int>[n];
            for (var i = 0; i < n; ++i) {
                g[i] = new List<int>();
            }
            foreach (var e in conn) {
                g[e[0]].Add(e[1]);
                g[e[1]].Add(e[0]);
            }
            this.t = new int[n];
            this.l = new int[n];
            return build(0, -1, new bool[n], new List<IList<int>>());
        }

        private IList<IList<int>> build(int v, int p, bool[] u, List<IList<int>> b) {
            u[v] = true;
            t[v] = l[v] = z++;
            foreach (var x in g[v]) {
                if (x == p) {
                    continue;
                }
                if (u[x]) {
                    l[v] = Math.Min(l[v], t[x]);
                } else {
                    build(x, v, u, b);
                    l[v] = Math.Min(l[v], l[x]);
                    if (l[x] > t[v]) {
                        b.Add(new[] { v, x });
                    }
                }
            }
            return b;
        }

        private List<int>[] g;
        private int[] t;
        private int[] l;
        private int z;
    }
}