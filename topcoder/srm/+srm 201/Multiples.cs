using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
 public class Multiples {
        public int number(int min, int max, int factor) {
            if (min != max) {
                if (max < 0)
                    return number(-max, -min, factor);
                if (min < 0)
                    return number(max, factor) + number(1, -min, factor);
                if (min > 0)
                    return number(max, factor) - number(min - 1, factor);
                return number(max, factor);
            }
            return min % factor == 0 ? 1 : 0;
        }

        private static int number(int max, int factor) {
            return max / factor + 1;
        }
    }
}