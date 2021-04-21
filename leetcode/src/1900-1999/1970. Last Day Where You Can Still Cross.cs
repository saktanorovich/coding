using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1970 {
        public int LatestDayToCross(int row, int col, int[][] cells) {
            int lo = 0, hi = cells.Length;
            while (lo < hi) {
                var days = (lo + hi + 1) / 2;
                if (okay(days, row, col, cells)) {
                    lo = days;
                } else {
                    hi = days - 1;
                }
            }
            return lo;
        }

        private static bool okay(int days, int row, int col, int[][] cells) {
            var mat = new int[row, col];
            for (var day = 0; day < days; ++day) {
                var r = cells[day][0] - 1;
                var c = cells[day][1] - 1;
                mat[r, c] = 1;
            }
            var que = new Queue<int>();
            for (var c = 0; c < col; ++c) {
                if (mat[0, c] == 0) {
                    que.Enqueue(0);
                    que.Enqueue(c);
                    mat[0, c] = -1;
                }
            }
            while (que.Count > 0) {
                var cr = que.Dequeue();
                var cc = que.Dequeue();
                if (cr == row - 1) {
                    return true;
                }
                for (var i = 0; i < 4; ++i) {
                    var nr = cr + dx[i];
                    var nc = cc + dy[i];
                    if (0 <= nr && nr < row && 0 <= nc && nc < col) {
                        if (mat[nr, nc] == 0) {
                            que.Enqueue(nr);
                            que.Enqueue(nc);
                            mat[nr, nc] = -1;
                        }
                    }
                }
            }
            return false;
        }

        private static readonly int[] dx = { -1, 0, +1, 0 };
        private static readonly int[] dy = { 0, -1, 0, +1 };
    }
}
