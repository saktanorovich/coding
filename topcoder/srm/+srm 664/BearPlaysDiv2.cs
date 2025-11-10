using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class BearPlaysDiv2 {
        public string equalPiles(int a, int b, int c) {
            return equalPiles(a, b, c, a + b + c);
        }

        private static string equalPiles(int a, int b, int c, int s) {
            if (s % 3 == 0) {
                var can = new bool[s + 1, s + 1];
                doit(new[] { a, b, c }, can);
                if (can[s / 3, s / 3])
                    return "possible";
            }
            return "impossible";
        }

        private static void doit(int[] piles, bool[,] can) {
            var stack = new Stack<int[]>();
            for (stack.Push(piles); stack.Count > 0;) {
                piles = stack.Pop();

                if (can[piles[0], piles[1]]) continue;
                if (can[piles[0], piles[2]]) continue;
                if (can[piles[1], piles[2]]) continue;

                can[piles[0], piles[1]] = true;
                can[piles[0], piles[2]] = true;
                can[piles[1], piles[2]] = true;
                for (var i = 0; i < 3; ++i) {
                    for (var j = 0; j < 3; ++j) {
                        if (piles[i] < piles[j]) {
                            stack.Push(new[] { 2 * piles[i], piles[j] - piles[i], piles[3 - i - j] });
                        }
                    }
                }
            }
        }
    }
}
