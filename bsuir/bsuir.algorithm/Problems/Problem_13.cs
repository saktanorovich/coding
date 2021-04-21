using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bsuir.algorithm {
    public class Problem_13 {
        public bool process(int testCase, InputReader reader, OutputWriter writer) {
            this.n = reader.NextInt();
            this.m = reader.NextInt();
            this.g = new List<int>[n];
            for (var i = 0; i < n; ++i) {
                g[i] = new List<int>();
            }
            for (var i = 0; i < m; ++i) {
                var a = reader.NextInt() - 1;
                var b = reader.NextInt() - 1;
                g[a].Add(b);
                g[b].Add(a);
            }
            var c = 0;
            var u = new bool[n + 1];
            for (var i = 0; i < n; ++i) {
                if (!u[i]) {
                    c += dfs(i, u);
                }
            }
            if (c == 1) {
                writer.WriteLine(m - n + 1);
            } else {
                writer.WriteLine(-1);
            }
            return true;
        }

        private int dfs(int a, bool[] u) {
            u[a] = true;
            foreach (var b in g[a]) {
                if (!u[b]) {
                    dfs(b, u);
                }
            }
            return 1;
        }

        private List<int>[] g;
        private int n;
        private int m;
    }
}
