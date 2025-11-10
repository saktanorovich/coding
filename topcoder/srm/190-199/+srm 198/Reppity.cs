using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class Reppity {
        public int longestRep(string input) {
            for (var len = input.Length / 2; len > 0; --len) {
                for (var i = 0; i + 2 * len <= input.Length; ++i) {
                    if (input.LastIndexOf(input.Substring(i, len)) >= i + len) {
                        return len;
                    }
                }
            }
            return 0;
        }
    }
}