using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0374 {
        public int GuessNumber(int n) {
            return GuessNumber(1, n);
        }

        private int GuessNumber(int a, int b) {
            if (a < b) {
                var x = a + (b - a) / 2;
                if (guess(x) < 0) {
                    return GuessNumber(a, x - 1);
                }
                if (guess(x) > 0) {
                    return GuessNumber(x + 1, b);
                }
                return x;
            }
            return a;
        }

        private int guess(int x) {
            return 0;
        }
    }
}
