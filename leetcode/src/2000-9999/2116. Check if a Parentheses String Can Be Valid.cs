using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_2116 {
        public bool CanBeValid(string s, string locked) {
            if (s.Length % 2 == 1) {
                return false;
            }
            var minScore = 0;
            var maxScore = 0;
            for (var i = 0; i < s.Length; ++i) {
                if (locked[i] == '1') {
                    if (s[i] == '(') {
                        minScore ++;
                        maxScore ++;
                    } else {
                        minScore --;
                        maxScore --;
                    }
                } else {
                    minScore --;
                    maxScore ++;
                }
                if (maxScore < 0) {
                    return false;
                }
                if (minScore < 0) {
                    minScore = 1;
                }
            }
            return minScore == 0;
        }
    }
}
