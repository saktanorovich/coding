using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bsuir.algorithm {
    public class Problem_20 {
        public bool process(int testCase, InputReader reader, OutputWriter writer) {
            var n = reader.NextInt();
            var m = reader.NextInt();
            var a = new int[n + 1, m + 1];
            var b = new int[n + 1, m + 1];
            for (var i = 1; i <= n; ++i) {
                for (var j = 1; j <= m; ++j) {
                    a[i, j] = reader.NextInt();
                    b[i, j] = (int)1e+6;
                }
            }
            var q = new Queue<int>();
            // boundary elements do not have any water
            for (var i = 1; i <= n; ++i) {
                b[i, 1] = a[i, 1];
                b[i, m] = a[i, m];
                q.Enqueue(i);
                q.Enqueue(1);
                q.Enqueue(i);
                q.Enqueue(m);
            }
            // boundary elements do not have any water
            for (var j = 1; j <= m; ++j) {
                b[1, j] = a[1, j];
                b[n, j] = a[n, j];
                q.Enqueue(1);
                q.Enqueue(j);
                q.Enqueue(n);
                q.Enqueue(j);
            }
            while (q.Count > 0) {
                var x1 = q.Dequeue();
                var y1 = q.Dequeue();
                for (var k = 0; k < 4; ++k) {
                    var x2 = x1 + dx[k];
                    var y2 = y1 + dy[k];
                    if (1 <= x2 && x2 <= n && 1 <= y2 && y2 <= m) {
                        // if we have a waterfall
                        if (b[x2, y2] > b[x1, y1]) {
                            b[x2, y2] = Math.Max(a[x2, y2], b[x1, y1]);
                            q.Enqueue(x2);
                            q.Enqueue(y2);
                        }
                    }
                }
            }
            var res = 0;
            for (var i = 1; i <= n; ++i) {
                for (var j = 1; j <= m; ++j) {
                    res += b[i, j] - a[i, j];
                }
            }
            writer.WriteLine(res);
            return true;
        }

        private readonly int[] dx = { -1, 0, +1, 0 };
        private readonly int[] dy = { 0, -1, 0, +1 };
    }
}
