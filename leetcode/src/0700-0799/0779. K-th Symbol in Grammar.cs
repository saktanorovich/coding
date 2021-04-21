using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0779 {
        public int KthGrammar(int N, int K) {
            return KthGrammar(N - 1, K - 1, 0);
        }

        private int KthGrammar(int n, int k, int c) {
            if (n == 0) {
                return c;
            }
            var h = 1 << n - 1;
            if (k < h) {
                return KthGrammar(n - 1, k, c);
            } else {
                return KthGrammar(n - 1, k - h, c ^ 1);
            }
        }
    }
}
