using System;

namespace TopCoder.Algorithm {
    public class MagicSquare {
        public int missing(int[] square) {
            return missing(new[,] {
                { square[0], square[1], square[2] },
                { square[3], square[4], square[5] },
                { square[6], square[7], square[8] },
            });
        }

        private static int missing(int[,] square) {
            for (var i = 0; i < 3; ++i)
                for (var j = 0; j < 3; ++j) {
                    if (square[i, j] == -1) {
                        var sum = 0;
                        for (var k = 0; k < 3; ++k) {
                            sum += square[(i + 1) % 3, k];
                        }
                        return sum - square[i, (j + 2) % 3] - square[i, (j + 1) % 3];
                    }
                }
            throw new Exception();
        }
    }
}