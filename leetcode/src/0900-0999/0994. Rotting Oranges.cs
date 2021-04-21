using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_994 {
        public int OrangesRotting(int[][] grid) {
            if (grid == null || grid.Length == 0) {
                return 0;
            }
            return OrangesRotting(grid, grid.Length, grid[0].Length);
        }

        private int OrangesRotting(int[][] grid, int n, int m) {
            var que = new Queue<int>();
            for (var i = 0; i < n; ++i) {
                for (var j = 0; j < m; ++j) {
                    if (grid[i][j] == 2) {
                        que.Enqueue(i);
                        que.Enqueue(j);
                    }
                }
            }
            var time = 0;
            while (que.Count > 0) {
                var size = que.Count / 2;
                while (size-- > 0) {
                    var x = que.Dequeue();
                    var y = que.Dequeue();
                    for (var k = 0; k < 4; ++k) {
                        var nx = x + dx[k];
                        var ny = y + dy[k];
                        if (0 <= nx && nx < n && 0 <= ny && ny < m) {
                            if (grid[nx][ny] == 1) {
                                grid[nx][ny] = 2;
                                que.Enqueue(nx);
                                que.Enqueue(ny);
                            }
                        }
                    }
                }
                time++;
            }
            for (var i = 0; i < n; ++i) {
                for (var j = 0; j < m; ++j) {
                    if (grid[i][j] == 1) {
                        return -1;
                    }
                }
            }
            return Math.Max(0, time - 1);
        }

        private static readonly int[] dx = new[] { -1, 0, 1, 0 };
        private static readonly int[] dy = new[] { 0, -1, 0, 1 };
    }
}
