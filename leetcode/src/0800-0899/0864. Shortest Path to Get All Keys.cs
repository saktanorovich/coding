using System;
using System.Collections.Generic;
using System.Text;

namespace coding.leetcode {
    public class Solution_0864 {
        public int ShortestPathAllKeys(string[] grid) {
            return ShortestPathAllKeys(grid, grid.Length, grid[0].Length);
        }

        private int ShortestPathAllKeys(string[] grid, int n, int m) {
            var keys = 0;
            var xpos = 0;
            var ypos = 0;
            for (var i = 0; i < n; ++i) {
                for (var j = 0; j < m; ++j) {
                    switch (grid[i][j]) {
                        case 'a': case 'b': case 'c': case 'd': case 'e': case 'f':
                            keys++;
                            break;
                        case '@':
                            xpos = i;
                            ypos = j;
                            break;
                    }
                }
            }
            var goal = (1 << keys) - 1;
            var best = new int[n, m, 1 << keys];
            for (var i = 0; i < n; ++i) {
                for (var j = 0; j < m; ++j) {
                    for (var s = 0; s < 1 << keys; ++s) {
                        best[i, j, s] = int.MaxValue;
                    }
                }
            }
            best[xpos, ypos, 0] = 0;
            var res = int.MaxValue;
            var que = new Queue<int>();
            que.Enqueue(xpos);
            que.Enqueue(ypos);
            que.Enqueue(0);
            while (que.Count > 0) {
                xpos = que.Dequeue();
                ypos = que.Dequeue();
                keys = que.Dequeue();
                if (keys == goal) {
                    if (res > best[xpos, ypos, keys]) {
                        res = best[xpos, ypos, keys];
                    }
                }
                void relax(int xnew, int ynew, int knew) {
                    if (best[xnew, ynew, knew] > best[xpos, ypos, keys] + 1) {
                        best[xnew, ynew, knew] = best[xpos, ypos, keys] + 1;
                        que.Enqueue(xnew);
                        que.Enqueue(ynew);
                        que.Enqueue(knew);
                    }
                }
                for (var k = 0; k < 4; ++k) {
                    var xnew = xpos + dx[k];
                    var ynew = ypos + dy[k];
                    if (0 <= xnew && xnew < n && 0 <= ynew && ynew < m) {
                        var c = grid[xnew][ynew];
                        if (c != '#') {
                            if ('a' <= c && c <= 'f') {
                                relax(xnew, ynew, keys | (1 << (c - 'a')));
                                continue;
                            }
                            if ('A' <= c && c <= 'F') {
                                if ((keys & (1 << (c - 'A'))) != 0) {
                                    relax(xnew, ynew, keys);
                                }
                                continue;
                            }
                            relax(xnew, ynew, keys);
                        }
                    }
                }
            }
            return res < int.MaxValue ? res : -1;
        }

        private static readonly int[] dx = { -1, 0, +1, 0 };
        private static readonly int[] dy = { 0, -1, 0, +1 };
    }
}
