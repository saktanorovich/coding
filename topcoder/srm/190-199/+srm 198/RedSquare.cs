using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class RedSquare {
        public int countTheEmptyReds(int maxRank, int maxFile, int[] rank, int[] file) {
            var result = (maxRank * maxFile) / 2;
            for (var i = 0; i < rank.Length; ++i) {
                if (isRed(maxFile, rank[i], file[i])) {
                    result = result - 1;
                }
            }
            return result;
        }

        private static bool isRed(int maxFile, int rank, int file) {
            var offset = maxFile - file + 1;
            if (rank % 2 == 1) {
                return offset % 2 == 0;
            }
            return offset % 2 == 1;
        }
    }
}