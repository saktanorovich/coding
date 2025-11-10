using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class MinePut {
        public int getMines(string[] partialBoard) {
            return getMines(partialBoard, partialBoard.Length, partialBoard[0].Length);
        }

        private static int getMines(string[] board, int n, int m) {
            var res = 0;
            for (var mask = 0; mask < 1 << (n * m); ++mask) {
                var mines = bits(mask);
                if (mines > res) {
                    if (valid(board, mask, n, m)) {
                        res = mines;
                    }
                }
            }
            return res;
        }

        private static bool valid(string[] board, int mask, int n, int m) {
            var surround = new int[n, m];
            var contains = new int[n, m]; 
            for (var i = 0; i < n; ++i) {
                for (var j = 0; j < m; ++j) {
                    surround[i, j] = "0123456789.".IndexOf(board[i][j]);
                    if ((mask & (1 << (i * m + j))) != 0) {
                        if (board[i][j] == '.') {
                            contains[i, j] = 1;
                        }
                        else {
                            return false;
                        }
                    }
                }
            }
            for (var i = 0; i < n; ++i) {
                for (var j = 0; j < m; ++j) {
                    var at = 0;
                    for (var k = 0; k < 8; ++k) {
                        var x = i + dx[k];
                        var y = j + dy[k];
                        if (0 <= x && x < n && 0 <= y && y < m) {
                            at += contains[x, y];
                        }
                    }
                    if (at > surround[i, j]) {
                        return false;
                    }
                }
            }
            return true;
        }

        private static int bits(int set) {
            if (set > 0) {
                return 1 + bits(set & (set - 1));
            }
            return 0;
        }

        private static readonly int[] dx = { -1, -1, -1, 0, +1, +1, +1, 0 };
        private static readonly int[] dy = { -1, 0, +1, +1, +1, 0, -1, -1 };
    }
}
