using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0289 {
        public void GameOfLife(int[][] board) {
            for (var i = 0; i < board.Length; ++i) {
                for (var j = 0; j < board[i].Length; ++j) {
                    var cnt = 0;
                    for (var di = -1; di <= +1; ++di) {
                        for (var dj = -1; dj <= +1; ++dj) {
                            var x = i + di;
                            var y = j + dj;
                            if (x != i || y != j) {
                                if (0 <= x && x < board.Length && 0 <= y && y < board[i].Length) {
                                    cnt += board[x][y] & 1;
                                }
                            }
                        }
                    }
                    if (board[i][j] > 0) {
                        if (cnt == 2) board[i][j] = 3;
                        if (cnt == 3) board[i][j] = 3;
                    } else {
                        if (cnt == 3) board[i][j] = 2;
                    }
                }
            }
            for (var i = 0; i < board.Length; ++i) {
                for (var j = 0; j < board[i].Length; ++j) {
                    board[i][j] >>= 1;
                }
            }
        }
    }
}
