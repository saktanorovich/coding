using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_2258 {
        public int MaximumMinutes(int[][] grid) {
            return MaximumMinutes(grid, grid.Length, grid[0].Length);
        }

        private int MaximumMinutes(int[][] grid, int n, int m) {
            return MaximumMinutes(grid, fire(grid, n, m), n, m);
        }
        /**/
        private int MaximumMinutes(int[][] grid, int[,] fire, int n, int m) {
            return spfa(grid, fire, n, m);
        }

        private static int spfa(int[][] grid, int[,] fire, int n, int m) {
            var land = new int[n, m];
            for (var i = 0; i < n; ++i) {
                for (var j = 0; j < m; ++j) {
                    land[i, j] = -1;
                }
            }
            land[n - 1, m - 1] = Math.Min(fire[n - 1, m - 1], (int)1e+9);
            var have = new HashSet<(int, int)>();
            var queu = new Queue<(int, int)>();
            queu.Enqueue((n - 1, m - 1));
            while (queu.Count > 0) {
                have.Remove(queu.Peek());
                var cx = queu.Peek().Item1;
                var cy = queu.Peek().Item2;
                queu.Dequeue();
                for (var k = 0; k < 4; ++k) {
                    var nx = cx + dx[k];
                    var ny = cy + dy[k];
                    if (0 <= nx && nx < n && 0 <= ny && ny < m) {
                        if (grid[nx][ny] == 0) {
                            var time = Math.Min(land[cx, cy], fire[nx, ny]);
                            if (time < (int)1e+9) {
                                time = time - 1;
                            }
                            if (land[nx, ny] < time) {
                                land[nx, ny] = time;
                                if (have.Contains((nx, ny)) == false) {
                                    have.Add((nx, ny));
                                    queu.Enqueue((nx, ny));
                                }
                            }
                        }
                    }
                }    
            }
            return land[0, 0];
        }
        /**
        private int MaximumMinutes(int[][] grid, int[,] fire, int n, int m) {
            int lo = 0, hi = (int)1e+9;
            while (lo < hi) {
                var wait = (lo + hi + 1) / 2;
                if (okay(grid, fire, n, m, wait)) {
                    lo = wait;
                } else {
                    hi = wait - 1;
                }
            }
            return okay(grid, fire, n, m, lo) ? lo : -1;
        }

        private static bool okay(int[][] grid, int[,] fire, int n, int m, int wait) {
            var land = new int[n, m];
            for (var i = 0; i < n; ++i) {
                for (var j = 0; j < m; ++j) {
                    land[i, j] = (int)2e+9;
                }
            }
            land[0, 0] = wait;
            var queu = new Queue<(int, int)>();
            queu.Enqueue((0, 0));
            while (queu.Count > 0) {
                var cx = queu.Peek().Item1;
                var cy = queu.Peek().Item2;
                queu.Dequeue();
                for (var k = 0; k < 4; ++k) {
                    var nx = cx + dx[k];
                    var ny = cy + dy[k];
                    if (0 <= nx && nx < n && 0 <= ny && ny < m) {
                        if (grid[nx][ny] == 0) {
                            if (land[cx, cy] + 1 > fire[nx, ny]) {
                                continue;
                            }
                            if (land[cx, cy] + 1 < fire[nx, ny] || nx == n - 1 && ny == m - 1) {
                                if (land[nx, ny] > land[cx, cy] + 1) {
                                    land[nx, ny] = land[cx, cy] + 1;
                                    queu.Enqueue((nx, ny));
                                }
                            }
                        }
                    }
                }
            }
            return land[n - 1, m - 1] < (int)2e+9;
        }
        /**/
        private static int[,] fire(int[][] grid, int n, int m) {
            var queu = new Queue<(int, int)>();
            var land = new int[n, m];
            for (var i = 0; i < n; ++i) {
                for (var j = 0; j < m; ++j) {
                    land[i, j] = (int)2e+9;
                    if (grid[i][j] == 1) {
                        land[i, j] = 0;
                        queu.Enqueue((i, j));
                    }
                }
            }
            while (queu.Count > 0) {
                var cx = queu.Peek().Item1;
                var cy = queu.Peek().Item2;
                queu.Dequeue();
                for (var k = 0; k < 4; ++k) {
                    var nx = cx + dx[k];
                    var ny = cy + dy[k];
                    if (0 <= nx && nx < n && 0 <= ny && ny < m) {
                        if (grid[nx][ny] == 0) {
                            if (land[nx, ny] > land[cx, cy] + 1) {
                                land[nx, ny] = land[cx, cy] + 1;
                                queu.Enqueue((nx, ny));
                            }
                        }
                    }
                }
            }
            return land;
        }

        private static readonly int[] dx = { -1, 0, +1, 0 };
        private static readonly int[] dy = { 0, -1, 0, +1 };
    }
}