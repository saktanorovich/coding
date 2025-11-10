using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1319 {
        public int MakeConnected(int n, int[][] conn) {
            return MakeConnected(conn, conn.Length, n);
        }

        public int MakeConnected(int[][] conn, int m, int n) {
            if (m < n - 1) {
                return -1;
            }
            var g = new List<int>[n];
            for (var i = 0; i < n; ++i) {
                g[i] = new List<int>();
            }
            foreach (var e in conn) {
                g[e[0]].Add(e[1]);
                g[e[1]].Add(e[0]);
            }
            var c = 0;
            var u = new bool[n];
            for (var i = 0; i < n; ++i) {
                if (u[i] == false) {
                    dfs(g, i, u);
                    c = c + 1;
                }
            }
            return c - 1;
        }

        private void dfs(List<int>[] g, int v, bool[] u) {
            u[v] = true;
            foreach (var x in g[v]) {
                if (u[x] == false) {
                    dfs(g, x, u);
                }
            }
        }
    }
}