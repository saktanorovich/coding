using System;
using System.Linq;

namespace TopCoder.Algorithm {
    public class StringMult {
        public string times(string sFactor, int iFactor) {
            if (string.IsNullOrEmpty(sFactor) || iFactor == 0) {
                return string.Empty;
            }
            if (iFactor < 0) {
                return times(new string(sFactor.Reverse().ToArray()), -iFactor);
            }
            var result = string.Empty;
            for (var i = 0; i < iFactor; ++i) {
                result += sFactor;
            }
            return result;
        }
    }
}