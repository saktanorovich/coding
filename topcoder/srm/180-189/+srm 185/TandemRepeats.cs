using System;

namespace TopCoder.Algorithm {
    public class TandemRepeats {
        public int maxLength(string dna, int k) {
            for (var len = dna.Length / 2; len > 0; --len) {
                for (var i = 0; i + 2 * len <= dna.Length; ++i) {
                    var diff = 0;
                    for (var j = 0; j < len; ++j) {
                        if (dna[i + j] != dna[i + len + j]) {
                            ++diff;
                        }
                    }
                    if (diff <= k) return len;
                }
            }
            return 0;
        }
    }
}
