using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1147 {
        public int LongestDecomposition(string text) {
            this.text = text;
            this.best = new int[text.Length, text.Length];
            for (var i = 0; i < text.Length; ++i) {
                best[i, i] = 1;
                for (var j = i + 1; j < text.Length; ++j) {
                    best[i, j] = -1;
                }
            }
            return doit(0, text.Length - 1);
        }

        private int doit(int i, int j) {
            if (best[i, j] == -1) {
                best[i, j] = 1;
                var len = j - i + 1;
                for (var k = 1; 2 * k <= len; ++k) {
                    if (text.Substring(i, k) == text.Substring(j - k + 1, k)) {
                        best[i, j] = Math.Max(best[i, j], doit(i + k, j - k) + 2);
                    }
                }
            }
            return best[i, j];
        }

        private string text;
        private int[,] best; 
    }
}
