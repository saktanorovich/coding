using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bsuir.algorithm {
    public class Problem_01 {
        public bool process(int testCase, InputReader reader, OutputWriter writer) {
            var n = reader.NextInt();
            var m = reader.NextInt();
            var g = new List<int>[n];
            for (var i = 0; i < n; ++i) {
                g[i] = new List<int>();
            }
            for (var i = 0; i < m; ++i) {
                var a = reader.NextInt() - 1;
                var b = reader.NextInt() - 1;
                g[a].Add(b);
                g[b].Add(a);
            }
            var v = new bool[n];
            var c = 0;
            for (var i = 0; i < n; ++i) {
                if (!v[i]) {
                    dfs(g, i, v);
                    c = c + 1;
                }
            }
            if (c > 0) {
                writer.WriteLine(c - 1);
            } else {
                writer.WriteLine(-1);
            }
            return true;
        }

        private void dfs(List<int>[] g, int a, bool[] v) {
            v[a] = true;
            foreach (var b in g[a]) {
                if (!v[b]) {
                    dfs(g, b, v);
                }
            }
        }
    }
}
