using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class ComplementaryDNAChains {
        public int minReplaces(string first, string second) {
            var result = 0;
            for (var i = 0; i < first.Length; ++i) {
                if (complementary(first[i], second[i])) {
                    continue;
                }
                ++result;
            }
            return result;
        }

        private bool complementary(char a, char b) {
            if ((a == 'A' && b == 'T') ||
                (a == 'T' && b == 'A') ||
                (a == 'C' && b == 'G') ||
                (a == 'G' && b == 'C')) {
                return true;
            }
            return false;
        }
    }
}
