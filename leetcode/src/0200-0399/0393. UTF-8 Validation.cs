using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0393 {
        public bool ValidUtf8(int[] data) {
            var count = 0;
            for (var i = 0; i < data.Length; ++i) {
                var octet = data[i] & 0xFF;
                if (count == 0) {
                         if (octet >> 7 == 0)       count = 0;
                    else if (octet >> 5 == 0b00110) count = 1;
                    else if (octet >> 4 == 0b01110) count = 2;
                    else if (octet >> 3 == 0b11110) count = 3;
                    else return false;
                } else {
                    if (octet >> 6 != 0b10) {
                        return false;
                    }
                    count--;
                }
            }
            return count == 0;
        }
    }
}
