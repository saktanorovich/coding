using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0790 {
        public int NumTilings(int n) {
            var f = new uint[n + 1, 3];
            f[0, 2] = 1;
            f[1, 2] = 1;
            for (var i = 2; i <= n; ++i) {
                f[i, 0] += f[i - 2, 2];
                f[i, 0] += f[i - 1, 1];
                if (f[i, 0] >= mod) {
                    f[i, 0] %= mod;
                }

                f[i, 1] += f[i - 2, 2];
                f[i, 1] += f[i - 1, 0];
                if (f[i, 1] >= mod) {
                    f[i, 1] %= mod;
                }

                f[i, 2] += f[i - 2, 2];
                f[i, 2] += f[i - 1, 2];
                f[i, 2] += f[i - 1, 1];
                f[i, 2] += f[i - 1, 0];
                if (f[i, 2] >= mod) {
                    f[i, 2] %= mod;
                }
            }
            return (int)f[n, 2];
        }

        private const uint mod = (uint)1e9 + 7;
    }
}
