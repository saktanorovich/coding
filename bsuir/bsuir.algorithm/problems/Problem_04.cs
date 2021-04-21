using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bsuir.algorithm {
    public class Problem_04 {
        public bool process(int testCase, InputReader reader, OutputWriter writer) {
            var n = reader.NextInt();
            var m = reader.NextInt();
            var X = reader.NextInt() - 1;
            var Y = reader.NextInt() - 1;
            var d = new int[n, m];
            for (var i = 0; i < n; ++i) {
                for (var j = 0; j < m; ++j) {
                    d[i, j] = int.MaxValue;
                }
            }
            d[0, 0] = 0;
            var q = new Queue<int>();
            q.Enqueue(0);
            q.Enqueue(0);
            while (q.Count > 0) {
                var x = q.Dequeue();
                var y = q.Dequeue();
                if (x == X && y == Y) {
                    writer.WriteLine(d[x, y]);
                    return true;
                }
                for (var k = 0; k < 8; ++k) {
                    var nx = x + dx[k];
                    var ny = y + dy[k];
                    if (0 <= nx && nx < n && 0 <= ny && ny < m) {
                        if (d[nx, ny] > d[x, y] + 1) {
                            d[nx, ny] = d[x, y] + 1;
                            q.Enqueue(nx);
                            q.Enqueue(ny);
                        }
                    }
                }
            }
            writer.WriteLine("NEVAR");
            return true;
        }

        private readonly int[] dx = { -2, -2, -1, -1, +1, +1, +2, +2 };
        private readonly int[] dy = { -1, +1, -2, +2, -2, +2, -1, +1 };
    }
}
