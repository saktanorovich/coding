using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution {
        public int MaximumGood(int[][] statements) {
            var answ = 0;
            for (var set = 1; set < 1 << statements.Length; ++set) {
                answ = Math.Max(answ, count(statements, set));
            }
            return answ;
        }

        private static int count(int[][] statements, int set) {
            var res = 0;
            for (var i = 0; i < statements.Length; ++i) {
                if (has(set, i) == 1) {
                    res = res + 1;
                    for (var j = 0; j < statements.Length; ++j) {
                        if (statements[i][j] == 1 && has(set, j) == 0) return 0;
                        if (statements[i][j] == 0 && has(set, j) == 1) return 0;
                    }
                }
            }
            return res;
        }

        private static int has(int set, int x) {
            return (set & (1 << x)) > 0 ? 1 : 0;
        }
    }
}