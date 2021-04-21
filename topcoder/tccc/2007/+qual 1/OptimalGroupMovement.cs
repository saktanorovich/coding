using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class OptimalGroupMovement {
        public int minimumCost(string board) {
            return minimumCost(board, board.Length);
        }

        // the optimal solution supposes to move only original groups not the
        // merged ones (can be proved by Cauchy–Bunyakovsky inequality)
        private static int minimumCost(string board, int n) {
            var res = int.MaxValue;
            for (var i = 0; i < n; ++i) {
                res = Math.Min(res, moves(board, x => 0 <= x && x < n, x => x - 1, i) + moves(board, x => 0 <= x && x < n, x => x + 1, i));
            }
            return res;
        }

        private static int moves(string board, Func<int, bool> valid, Func<int, int> step, int x) {
            while (valid(x) && board[x] == 'X') {
                x = step(x);
            }
            var res = 0;
            for (var sp = 0; valid(x);) {
                while (valid(x) && board[x] == '.') {
                    x = step(x);
                    sp = sp + 1;
                }
                var gr = 0;
                while (valid(x) && board[x] == 'X') {
                    x = step(x);
                    gr = gr + 1;
                }
                res += sp * gr * gr;
            }
            return res;
        }
    }
}
