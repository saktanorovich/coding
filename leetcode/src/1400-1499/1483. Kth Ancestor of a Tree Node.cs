using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1483 {
        public class TreeAncestor {
            private readonly int[,] p;
            private readonly int n;
            private readonly int m;

            public TreeAncestor(int n, int[] parent) {
                this.n = n;
                this.m = log2(n);
                this.p = new int[n, m];
                for (var i = 0; i < n; ++i) {
                    p[i, 0] = parent[i];
                }
                for (var k = 1; k < m; ++k) {
                    for (var i = 0; i < n; ++i) {
                        if (p[i, k - 1] >= 0) {
                            p[i, k] = p[p[i, k - 1], k - 1];
                        } else {
                            p[i, k] = -1;
                        }
                    }
                }
            }

            public int GetKthAncestor(int node, int k) {
                for (var j = m - 1; j >= 0; --j) {
                    if (k >= 1 << j) {
                        k -= 1 << j;
                        node = p[node, j];
                        if (node == -1) {
                            break;
                        }
                    }
                }
                return node;
            }

            private int log2(int n) {
                var m = 0;
                while (1 << m <= n) {
                    m = m + 1;
                }
                return m;
            }
        }
    }
}
