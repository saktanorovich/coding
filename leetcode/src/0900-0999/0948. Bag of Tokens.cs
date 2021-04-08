using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0948 {
        public int BagOfTokensScore(int[] tokens, int P) {
            return BagOfTokensScore(tokens, P, 0);
        }

        private int BagOfTokensScore(int[] tokens, int P, int S) {
            Array.Sort(tokens);
            int L = 0, R = tokens.Length - 1, B = S;
            while (L <= R) {
                if (P >= tokens[L]) {
                    P -= tokens[L];
                    S += 1;
                    L += 1;
                    if (B < S) {
                        B = S;
                    }
                    continue;
                }
                if (S >= 1) {
                    P += tokens[R];
                    S -= 1;
                    R -= 1;
                    continue;
                }
                break;
            }
            return B;
        }
    }
}
