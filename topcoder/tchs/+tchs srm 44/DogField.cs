using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class DogField {
        public int saveDogs(string[] field) {
            return saveDogs(field, field.Length, field[0].Length);
        }

        private int saveDogs(string[] field, int n, int m) {
            hp = new List<Point>();
            dp = new List<Point>();
            for (var i = 0; i < n; ++i) {
                for (var j = 0; j < m; ++j) {
                    if (field[i][j].Equals('H')) {
                        hp.Add(new Point(i, j));
                    }
                    if (field[i][j].Equals('D')) {
                        dp.Add(new Point(i, j));
                    }
                }
            }
            dist = new int[hp.Count][,];
            for (var h = 0; h < hp.Count; ++h) {
                dist[h] = bfs(field, n, m, h);
            }
            memo = new int[hp.Count, 1 << dp.Count];
            for (var h = 0; h < hp.Count; ++h) {
                for (var set = 0; set < 1 << dp.Count; ++set) {
                    memo[h, set] = -1;
                }
            }
            var res = run(hp.Count, (1 << dp.Count) - 1);
            if (res < int.MaxValue / 2) {
                return res;
            }
            return -1;
        }

        private int[,] bfs(string[] field, int n, int m, int h) {
            var res = new int[n, m];
            for (var i = 0; i < n; ++i) {
                for (var j = 0; j < m; ++j) {
                    res[i, j] = -1;
                }
            }
            res[hp[h].x, hp[h].y] = 0;
            var queue = new Queue<Point>();
            for (queue.Enqueue(hp[h]); queue.Count > 0;) {
                var pt = queue.Dequeue();
                for (var k = 0; k < 4; ++k) {
                    var nx = pt.x + dx[k];
                    var ny = pt.y + dy[k];
                    if (0 <= nx && nx < n && 0 <= ny && ny < m) {
                        if (field[nx][ny] != 'R' && field[nx][ny] != 'H') {
                            if (res[nx, ny] == -1) {
                                res[nx, ny] = res[pt.x, pt.y] + 1;
                                queue.Enqueue(new Point(nx, ny));
                            }
                        }
                    }
                }
            }
            return res;
        }

        private int run(int houses, int dogs) {
            if (houses == 0) {
                return 0;
            }
            if (memo[houses - 1, dogs] == -1) {
                memo[houses - 1, dogs] = int.MaxValue / 2;
                for (var i = 0; i < dp.Count; ++i) {
                    if ((dogs & (1 << i)) != 0) {
                        if (dist[houses - 1][dp[i].x, dp[i].y] == -1) {
                            continue;
                        }
                        memo[houses - 1, dogs] = Math.Min(memo[houses - 1, dogs], run(houses - 1, dogs ^ (1 << i)) + dist[houses - 1][dp[i].x, dp[i].y]);
                    }
                }
            }
            return memo[houses - 1, dogs];
        }

        private static int[] dx = { 0, -1, 0, +1 };
        private static int[] dy = { +1, 0, -1, 0 };
        private List<Point> hp;
        private List<Point> dp;
        private int[][,] dist;
        private int[,] memo;

        private class Point {
            public readonly int x;
            public readonly int y;

            public Point(int x, int y) {
                this.x = x;
                this.y = y;
            }
        }
    }
}
