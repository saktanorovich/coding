using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class GravityBomb {
        public string[] aftermath(string[] board) {
            return aftermath(Array.ConvertAll(board, item => item.ToCharArray()), board.Length, board[0].Length);
        }

        private static string[] aftermath(char[][] board, int n, int m) {
            for (var cont = true; cont;) {
                cont = false;
                for (var r = n - 1; r > 0; --r) {
                    for (var c = 0; c < m; ++c) {
                        if (board[r][c] == '.') {
                            if (board[r - 1][c] == 'X') {
                                board[r - 1][c] = '.';
                                board[r][c] = 'X';
                                cont = true;
                            }
                        }
                    }
                }
            }
            for (var r = 0; r < n; ++r) {
                for (var c = 0; c < m; ++c) {
                    if (board[r][c] == '.') {
                        goto next;
                    }
                }
                for (var c = 0; c < m; ++c) {
                    for (var k = r; k > 0; --k) {
                        board[k][c] = board[k - 1][c];
                    }
                    board[0][c] = '.';
                }
                next:;
            }
            var result = new string[n];
            for (var i = 0; i < n; ++i) {
                result[i] = new string(board[i]);
            }
            return result;
        }
    }
}