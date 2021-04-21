using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class DungeonEscape {
        public int exitTime(string[] up, string[] down, string[] east, string[] west, int startLevel, int startEasting) {
            return exitTime(new int[4][][] {
                parse(up), parse(down), parse(east), parse(west)
            }, up.Length, up[0].Length, startLevel, startEasting);
        }

        private static int exitTime(int[][][] speed, int n, int m, int level, int easting) {
            var xpos = new Queue<int>();
            var ypos = new Queue<int>();
            var best = new int[n + 1, m];
            for (var x = 0; x <= n; ++x) {
                for (var y = 0; y < m; ++y) {
                    best[x, y] = +oo;
                }
            }
            best[level + 1, easting] = 0;
            var result = +oo;
            for (xpos.Enqueue(level + 1), ypos.Enqueue(easting); xpos.Count > 0;) {
                var x = xpos.Dequeue();
                var y = ypos.Dequeue();
                if (x > 0) {
                    for (var k = 0; k < 4; ++k) {
                        var xx = x + dx[k];
                        var yy = y + dy[k];
                        if (0 <= xx && xx <= n && 0 <= yy & yy < m) {
                            var time = best[x, y] + speed[k][x - 1][y];
                            if (possible(n, m, xx, time)) {
                                if (best[xx, yy] > time) {
                                    best[xx, yy] = time;
                                    xpos.Enqueue(xx);
                                    ypos.Enqueue(yy);
                                }
                            }
                        }
                    }
                }
                else {
                    result = Math.Min(result, best[0, y]);
                }
            }
            if (result < +oo)
                return result;
            return -1;
        }

        private static bool possible(int n, int m, int level, int time) {
            if (level == 0) {
                return true;
            }
            return time < (n - level + 1) * m;
        }

        private static int[][] parse(string[] s) {
            return Array.ConvertAll(s, item => {
                return Array.ConvertAll(item.ToCharArray(), c => {
                    if (c == 'x') {
                        return +oo;
                    }
                    return "0123456789".IndexOf(c);
                });
            });
        }

        private static readonly int[] dx = { -1, +1,  0,  0 };
        private static readonly int[] dy = {  0,  0, +1, -1 };
        private static readonly int oo = +1000000;
    }
}