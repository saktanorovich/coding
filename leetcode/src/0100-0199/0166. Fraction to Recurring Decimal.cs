using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0166 {
        public string FractionToDecimal(int num, int den) {
            return FractionToDecimal(1L * num, 1L * den);
        }

        private string FractionToDecimal(long num, long den) {
            if (num % den == 0) {
                return (num / den).ToString();
            }
            if (den < 0) {
                num = -num;
                den = -den;
            }
            var builder = new StringBuilder();
            if (num < 0) {
                num = -num;
                builder.Append("-");
            }
            builder.Append(num / den);
            builder.Append(".");
            var indx = new Dictionary<long, int>();
            for (num %= den; num > 0; ) {
                if (indx.ContainsKey(num) == false) {
                    indx.Add(num, builder.Length);
                    num *= 10;
                    builder.Append(num / den);
                    num %= den;
                } else {
                    builder.Insert(indx[num], '(');
                    builder.Append(')');
                    break;
                }
            }
            return builder.ToString();
        }
    }
}
