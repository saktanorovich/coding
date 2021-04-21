using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0079 {
        public bool Exist(char[][] board, string word) {
            var used = new bool[board.Length, board[0].Length];
            for (var i = 0; i < board.Length; ++i) {
                for (var j = 0; j < board[0].Length; ++j) {
                    if (board[i][j] == word[0]) {
                        if (Exist(board, i, j, word, 1, used)) {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool Exist(char[][] board, int cx, int cy, string word, int p, bool[,] used) {
            if (p == word.Length) {
                return true;
            }
            used[cx, cy] = true;
            for (var k = 0; k < 4; ++k) {
                var nx = cx + dx[k];
                var ny = cy + dy[k];
                if (0 <= nx && nx < board.Length && 0 <= ny && ny < board[0].Length) {
                    if (!used[nx, ny]) {
                        if (board[nx][ny] == word[p]) {
                            if (Exist(board, nx, ny, word, p + 1, used)) {
                                return true;
                            }
                        }
                    }
                }
            }
            used[cx, cy] = false;
            return false;
        }

        private readonly int[] dx = { -1, 0, 1, 0 };
        private readonly int[] dy = { 0, -1, 0, 1 };
    }
}
