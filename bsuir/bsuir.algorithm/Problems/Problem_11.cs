using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bsuir.algorithm {
    public class Problem_11 {
        public bool process(int testCase, InputReader reader, OutputWriter writer) {
            this.n = reader.NextInt();
            this.head = new int[2 * n];
            this.next = new int[2 * n];
            for (var i = 0; i < 2 * n; ++i) {
                head[i] = -1;
            }
            this.src = new int[2 * n];
            this.dst = new int[2 * n];
            this.wgt = new int[2 * n];
            this.cnt = new int[2 * n];
            for (var e = 0; e + 1 < n; ++e) {
                var a = reader.NextInt() - 1;
                var b = reader.NextInt() - 1;
                var c = reader.NextInt();
                add(a, b, c);
                add(b, a, c);
            }
            this.deg = new int[n];
            for (var a = 0; a < n; ++a) {
                for (var e = head[a]; e >= 0; e = next[e]) {
                    deg[a] = deg[a] + 1;
                }
            }
            var que = new Queue<int>();
            for (var a = 0; a < n; ++a) {
                if (deg[a] == 1) {
                    que.Enqueue(a);
                }
            }
            var res = 0L;
            while (que.Count > 0) {
                var a = que.Dequeue();
                cnt[a]++;
                for (var e = head[a]; e >= 0; e = next[e]) {
                    var b = dst[e];
                    if (b != a) {
                        res += sum(a, b, e);
                        if (res >= MOD) {
                            res -= MOD;
                        }
                        deg[b]--;
                        if (deg[b] == 1) {
                            que.Enqueue(b);
                        }
                    }
                }
            }
            writer.WriteLine(res);
            return true;
        }

        private long sum(int a, int b, int e) {
            cnt[a] += cnt[b];
            var res = 2L * wgt[e];
            res *= cnt[b];
            res %= MOD;
            res *= n - cnt[b];
            res %= MOD;
            return res;
        }

        private void add(int a, int b, int c) {
            src[m] = a;
            dst[m] = b;
            wgt[m] = c;
            next[m] = head[a];
            head[a] = m;
            m = m + 1;
        }

        private int[] next;
        private int[] head;
        private int[] src;
        private int[] dst;
        private int[] wgt;
        private int[] cnt;
        private int[] deg;
        private int n;
        private int m;

        private readonly long MOD = (long)1e7 + 7;
    }
}
