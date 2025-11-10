using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1835 {
        public int GetXORSum(int[] arr1, int[] arr2) {
            var xor = 0;
            foreach (var x in arr2) {
                xor ^= x;
            }
            var res = 0;
            foreach (var x in arr1) {
                res ^= xor & x;
            }
            return res;
        }
    }
}
