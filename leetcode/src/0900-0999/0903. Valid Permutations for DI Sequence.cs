using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0903 {
        public int NumPermsDISequence(string S) {
            return NumPermsDISequence(S, S.Length + 1);
        }

        // Assume we have P={a1,a2...,an}. To build Q={a1,a2...,an,x} from P
        // we need to scan P and add 1 to any element ai >= x.
        private int NumPermsDISequence(string S, int N) {
            var f = new int[N, N];
            f[0, 0] = 1;
            for (var i = 1; i < N; ++i) {
                for (var j = 0; j <= i; ++j) {
                    if (S[i - 1] == 'D') {
                        for (var k = j; k < i; ++k) {
                            f[i, j] += f[i - 1, k];
                            f[i, j] %= MOD;
                        }
                    } else {
                        for (var k = 0; k < j; ++k) {
                            f[i, j] += f[i - 1, k];
                            f[i, j] %= MOD;
                        }
                    }
                }
            }
            var res = 0;
            for (var j = 0; j < N; ++j) {
                res += f[N - 1, j];
                res %= MOD;
            }
            return res;
        }

        private static readonly int MOD = (int)1e9 + 7;
    }
}
