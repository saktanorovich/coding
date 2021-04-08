using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0940 {
        public int DistinctSubseqII(string S) {
            var f = new int[S.Length + 1];
            var z = new int[26];
            f[0] = 1;
            for (var i = 1; i <= S.Length; ++i) {
                f[i] += f[i - 1]; // take the ith char
                f[i] %= mod;
                f[i] += f[i - 1]; // skip the ith char
                f[i] %= mod;
                var j = z[S[i - 1] - 'a'];
                if (j > 0) {
                    f[i] -= f[j - 1];
                    f[i] += mod;
                    f[i] %= mod;
                }
                z[S[i - 1] - 'a'] = i;
            }
            f[S.Length] -= 1;
            f[S.Length] += mod;
            f[S.Length] %= mod;
            return f[S.Length];
        }
        
        private static readonly int mod = (int)1e9 + 7;
    }
}
