using System;

namespace TopCoder.Algorithm {
    public class DigitMultiples {
        public int getLongest(string single, string multiple) {
            for (var len = single.Length; len > 0; --len)
                for (var i = 0; i + len <= single.Length; ++i)
                    for (var j = 0; j + len <= multiple.Length; ++j)
                        if (match(single.Substring(i, len), multiple.Substring(j, len))) {
                            return len;
                        }
            return 0;
        }

        private static bool match(string single, string multiple) {
            for (var f = 0; f < 10; ++f) {
                var found = true;
                for (var i = 0; i < single.Length; ++i) {
                    if ((single[i] - '0') * f != multiple[i] - '0') {
                        found = false;
                        break;
                    }
                }
                if (found)
                    return true;
            }
            return false;
        }
    }
}