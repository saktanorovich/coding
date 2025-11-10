using System;

namespace TopCoder.Algorithm {
    public class GardenHose {
        public int countDead(int n, int rowDist, int colDist, int hoseDist) {
            var result = 0;
            for (var row = 1; row <= n; ++row)
                for (var col = 1; col <= n; ++col) {
                    var drow = Math.Min(row, n + 1 - row) * rowDist;
                    var dcol = Math.Min(col, n + 1 - col) * colDist;
                    if (Math.Min(drow, dcol) > hoseDist) {
                        ++result;
                    }
                }
            return result;
        }
    }
}