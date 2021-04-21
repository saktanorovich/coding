using System;

namespace TopCoder.Algorithm {
    public class MiniPaint {
        public int leastBad(string[] picture, int maxStrokes) {
            return leastBad(Array.ConvertAll(picture, paint), picture.Length, picture[0].Length, maxStrokes);
        }

        private static int leastBad(int[][] paint, int numOfRows, int numOfCols, int maxStrokes) {
            var best = new int[numOfRows + 1, maxStrokes + 1];
            for (var row = 1; row <= numOfRows; ++row) {
                for (var strokes = 1; strokes <= maxStrokes; ++strokes) {
                    for (var taken = 0; taken <= Math.Min(strokes, numOfCols); ++taken) {
                        best[row, strokes] = Math.Max(best[row, strokes], paint[row - 1][taken] + best[row - 1, strokes - taken]);
                    }
                }
            }
            return numOfRows * numOfCols - best[numOfRows, maxStrokes];
        }

        private static int[] paint(string row) {
            var best = Array.ConvertAll(new int[row.Length + 1], x => new int[row.Length + 1]);
            for (var pos = 1; pos <= row.Length; ++pos) {
                for (var strokes = 1; strokes <= row.Length; ++strokes) {
                    var color = new int[2];
                    best[pos][strokes] = best[pos][strokes - 1];
                    for (var cut = pos; cut > 0; --cut) {
                        ++color["WB".IndexOf(row[cut - 1])];
                        best[pos][strokes] = Math.Max(best[pos][strokes], best[cut - 1][strokes - 1] + Math.Max(color[0], color[1]));
                    }
                }
            }
            return best[row.Length];
        }
    }
}