using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1780 {
        public bool CheckPowersOfThree(int n) {
            while (n > 0) {
                if (n % 3 > 1) {
                    return false;
                }
                n /= 3;
            }
            return true;
        }
    }
}
