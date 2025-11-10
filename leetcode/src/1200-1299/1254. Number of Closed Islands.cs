using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1254 {
        public int ClosedIsland(int[][] grid) {
            return ClosedIsland(grid, grid.Length, grid[0].Length);
        }

        private int ClosedIsland(int[][] grid, int n, int m) {
            var closed = 0;
            var marked = new bool[n, m];
            for (var i = 0; i < n; ++i) {
                for (var j = 0; j < m; ++j) {
                    if (grid[i][j] == 0 && !marked[i, j]) {
                        closed += bfs(grid, marked, n, m, i, j);
                    }
                }
            }
            return closed;
        }

        private int bfs(int[][] grid, bool[,] mark, int n, int m, int x, int y) {
            var queue = new Queue<int>();
            queue.Enqueue(x);
            queue.Enqueue(y);
            var closed = 1;
            while (queue.Count > 0) {
                x = queue.Dequeue();
                y = queue.Dequeue();
                if (x == 0 || x == n - 1) {
                    closed = 0;
                }
                if (y == 0 || y == m - 1) {
                    closed = 0;
                }
                for (var k = 0; k < 4; ++k) {
                    var a = x + dx[k];
                    var b = y + dy[k];
                    if (0 <= a && a < n && 0 <= b && b < m) {
                        if (grid[a][b] == 0) {
                            if (mark[a, b] == false) {
                                mark[a, b] = true;
                                queue.Enqueue(a);
                                queue.Enqueue(b);
                            }
                        }
                    }
                }
            }
            return closed;
        }

        private static readonly int[] dx = { -1, 0, +1, 0 };
        private static readonly int[] dy = { 0, -1, 0, +1 };
    }
}
