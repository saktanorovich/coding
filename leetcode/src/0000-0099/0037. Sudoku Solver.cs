using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0037 {
        public void SolveSudoku(char[][] board) {
            var chars = new int[9, 9];
            for (var i = 0; i < 9; ++i) {
                for (var j = 0; j < 9; ++j) {
                    if (board[i][j] == '.') {
                        chars[i, j] = 0x3FF;
                    }
                }
            }
            for (var i = 0; i < 9; ++i) {
                for (var j = 0; j < 9; ++j) {
                    if (board[i][j] != '.') {
                        omit(chars, i, j, board[i][j] - '0');
                    }
                }
            }
            doit(board, chars, 0);
        }

        private bool doit(char[][] board, int[,] chars, int p) {
            if (p < 81) {
                var r = p / 9;
                var c = p % 9;
                if (board[r][c] != '.') {
                    return doit(board, chars, p + 1);
                }
                for (var d = 1; d <= 9; ++d) {
                    if ((chars[r, c] & (1 << d)) != 0) {
                        board[r][c] = (char)(d + '0');
                        if (doit(board, omit((int[,])chars.Clone(), r, c, d), p + 1)) {
                            return true;
                        }
                        board[r][c] = '.';
                    }
                }
                return false;
            }
            return true;
        }

        private int[,] omit(int[,] chars, int i, int j, int d) {
            var r = i / 3 * 3;
            var c = j / 3 * 3;
            for (var k = 0; k < 9; ++k) {
                var x = r + k / 3;
                var y = c + k % 3;
                chars[i, k] &= ~(1 << d);
                chars[k, j] &= ~(1 << d);
                chars[x, y] &= ~(1 << d);
            }
            return chars;
        }
    }
}
