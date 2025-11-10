using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class KiloMan {
        public int hitsTaken(int[] pattern, string jumps) {
            var result = 0;
            for (var i = 0; i < pattern.Length; ++i) {
                switch (jumps[i]) {
                    case 'S':
                        if (0 < pattern[i] && pattern[i] < 3)
                            result = result + 1;
                        break;
                    case 'J':
                        if (2 < pattern[i])
                            result = result + 1;
                        break;
                }
            }
            return result;
        }
    }
}