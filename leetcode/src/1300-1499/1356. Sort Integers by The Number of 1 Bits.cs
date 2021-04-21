using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1356 {
        public int[] SortByBits(int[] arr) {
            Array.Sort(arr, (a, b) => {
                var x = MathUtils.Bits(a);
                var y = MathUtils.Bits(b);
                if (x != y) {
                    return x - y; 
                } else {
                    return a - b;
                }
            });
            return arr;
        }

        private static class MathUtils {
            private static readonly int[] bits;

            static MathUtils() {
                bits = new int[256];
                for (var i = 0; i < bits.Length; ++i) {
                    bits[i] = bits[i >> 1] + (i & 1);
                }
            }

            public static int Bits(int set) {
                var res = 0;
                res += bits[(set & 0xff)];
                res += bits[(set >> 8) & 0xff];
                return res;
            }
        }
    }
}
