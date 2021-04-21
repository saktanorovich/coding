using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0052 {
        public int TotalNQueens(int n) {
            return TotalNQueens(n, 0, Enumerable.Empty<(int, int)>());
        }

        private int TotalNQueens(int n, int col, IEnumerable<(int, int)> ps) {
            if (col == n) {
                return 1;
            }
            var res = 0;
            for (var row = 0; row < n; ++row) {
                if (attack(row, col, ps) == false) {
                    res += TotalNQueens(n, col + 1, ps.Concat(new (int, int)[] { (row, col) }));
                }
            }
            return res;
        }

        private bool attack(int x, int y, IEnumerable<(int x, int y)> ps) {
            foreach (var p in ps) {
                if (Math.Abs(p.x - x) == 0) return true;
                if (Math.Abs(p.y - y) == 0) return true;
                if (Math.Abs(p.x - x) == Math.Abs(p.y - y)) {
                    return true;
                }
            }
            return false;
        }
    }
}
