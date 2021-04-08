using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0678 {
        public bool CheckValidString(string s) {
            var minOpened = 0;
            var maxOpened = 0;
            foreach (var x in s) {
                switch (x) {
                    case '(':
                        minOpened++;
                        maxOpened++;
                        break;
                    case ')':
                        minOpened--;
                        maxOpened--;
                        break;
                    case '*':
                        minOpened--;
                        maxOpened++;
                        break;
                }
                if (maxOpened < 0) {
                    return false;
                }
                if (minOpened < 0) {
                    minOpened = 0;
                }
            }
            return minOpened == 0;
        }
    }
}
