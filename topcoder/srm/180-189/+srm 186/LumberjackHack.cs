using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class LumberjackHack {
        public int timeToShore(string[] riverMap) {
            return timeToShore(riverMap, riverMap.Length - 1, riverMap[0].Length - 1);
        }

        private int timeToShore(string[] riverMap, int n, int m) {
            var best = new int[n + 1, m + 1, 2];
            for (var i = 0; i <= n; ++i)
                for (var j = 0; j <= m; ++j) {
                    best[i, j, 0] = int.MaxValue;
                    best[i, j, 1] = int.MaxValue;
                }
            var queue = new Queue<int>();
            for (var i = 0; i <= n; ++i)
                for (var j = 0; j <= m; ++j)
                    if (riverMap[i][j] == '+') {
                        best[i, j, 0] = 0;
                        queue.Enqueue(encode(i, j, 0));
                        break;
                    }
            while (queue.Count > 0) {
                var entry = queue.Dequeue();
                var xcurr = (entry >> 9) & 0xFF;
                var ycurr = (entry >> 1) & 0xFF;
                var water = (entry >> 0) & 0x01;
                for (var k = 0; k < 4; ++k) {
                    var xnext = xcurr + dx[k];
                    var ynext = ycurr + dy[k];
                    if (0 <= xnext && xnext <= n && 0 <= ynext && ynext <= m) {
                        if (riverMap[xnext][ynext] == '|') {
                            var cost = (k / 2) + 1;
                            if (best[xnext, ynext, water] > best[xcurr, ycurr, water] + cost) {
                                best[xnext, ynext, water] = best[xcurr, ycurr, water] + cost;
                                queue.Enqueue(encode(xnext, ynext, water));
                            }
                        }
                        else if (riverMap[xnext][ynext] == '.' && water == 0) {
                            if (best[xnext, ynext, 1] > best[xcurr, ycurr, water] + 3) {
                                best[xnext, ynext, 1] = best[xcurr, ycurr, water] + 3;
                                queue.Enqueue(encode(xnext, ynext, 1));
                            }
                        }
                    }
                }
            }
            var result = int.MaxValue;
            for (var i = 0; i <= n; ++i) {
                result = Math.Min(result, best[i, 0, 0]);
                result = Math.Min(result, best[i, 0, 1]);
                result = Math.Min(result, best[i, m, 0]);
                result = Math.Min(result, best[i, m, 1]);
            }
            if (result < int.MaxValue) {
                return result;
            }
            return -1;
        }

        private static int encode(int x, int y, int w) {
            return (x << 9) | (y << 1) | w;
        }

        private static readonly int[] dx = { -1, +1,  0,  0 };
        private static readonly int[] dy = {  0,  0, -1, +1 };
    }
}