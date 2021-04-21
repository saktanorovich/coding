using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bsuir.algorithm {
    public class Problem_21 {
        public bool process(int testCase, InputReader reader, OutputWriter writer) {
            this.n = reader.NextInt();
            this.tree = new List<int>[n];
            for (var i = 0; i < n; ++i) {
                tree[i] = new List<int>(0);
            }
            for (var e = 0; e + 1 < n; ++e) {
                var a = reader.NextInt() - 1;
                var b = reader.NextInt() - 1;
                tree[a].Add(b);
                tree[b].Add(a);
            }
            this.best = new int[n];
            this.take = new int[n];
            this.skip = new int[n];
            writer.WriteLine(dfs(0, 0));
            return true;
        }

        private int dfs(int curr, int prev) {
            var summ = 0;
            foreach (var next in tree[curr]) {
                if (next != prev) {
                    summ += dfs(next, curr);
                }
            }
            skip[curr] = summ;
            foreach (var next in tree[curr]) {
                if (next != prev) {
                    take[curr] = Math.Max(take[curr], 1 + summ - best[next] + skip[next]);
                }
            }
            best[curr] = Math.Max(take[curr], skip[curr]);
            return best[curr];
        }

        private List<int>[] tree;
        private int[] best;
        private int[] take;
        private int[] skip;
        private int n;
    }
}
