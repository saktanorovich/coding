using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1275 {
        public string Tictactoe(int[][] moves) {
            h = new int[2, N];
            v = new int[2, N];
            d = new int[2, 2];
            var player = 0;
            foreach (var move in moves) {
                var verdict = Tictac(move[0], move[1], player);
                if (verdict) {
                    return "AB"[player].ToString();
                }
                player ^= 1;
            }
            return moves.Length < 9 ? "Pending" : "Draw";
        }

        private bool Tictac(int x, int y, int p) {
            h[p, x]++;
            v[p, y]++;
            if (x - y == 0) {
                d[p, 0]++;
            }
            if (x + y == N - 1) {
                d[p, 1]++;
            }
            return h[p, x] == N || v[p, y] == N || d[p, 0] == N || d[p, 1] == N;
        }

        private readonly int N = 3;
        private int[,] h;
        private int[,] v;
        private int[,] d;
    }
}
