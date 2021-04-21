using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopCoder.Algorithm {
    public class BinaryCardinality {
        public int[] arrange(int[] numbers) {
            Array.Sort(numbers, (a, b) => {
                if (cardinality(a) != cardinality(b)) {
                    if (cardinality(a) < cardinality(b)) {
                        return -1;
                    }
                    return +1;
                }
                return a.CompareTo(b);
            });
            return numbers;
        }

        private static int cardinality(int x) {
            var result = 0;
            while (x > 0) {
                ++result;
                x = x & (x - 1);
            }
            return result;
        }
    }
}