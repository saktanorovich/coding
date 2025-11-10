using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class TextEditorNavigation {
        public int keystrokes(string[] source, int[] start, int[] finish) {
            best = new int[50, 50];
            for (var x = 0; x < 50; ++x) {
                for (var y = 0; y < 50; ++y) {
                    best[x, y] = int.MaxValue;
                }
            }
            best[start[0], start[1]] = 0;
            queue = new Queue<int[]>();
            for (queue.Enqueue(start); queue.Count > 0; queue.Dequeue()) {
                var cx = queue.Peek()[0];
                var cy = queue.Peek()[1];

                // left, right, up, down
                relax(source, cx, cy, cx, cy - 1);
                relax(source, cx, cy, cx - 1, cy);
                relax(source, cx, cy, cx, cy + 1);
                relax(source, cx, cy, cx + 1, cy);

                // home, end
                relax(source, cx, cy, cx, 0);
                relax(source, cx, cy, cx, source[cx].Length - 1);

                // top, bottom
                relax(source, cx, cy, 0, cy);
                relax(source, cx, cy, source.Length - 1, cy);

                // word left, word right
                for (var ly = Math.Min(cy - 1, source[cx].Length - 1); ly >= 0; --ly) {
                    if (char.IsLetter(source[cx][ly]) && (ly == 0 || char.IsWhiteSpace(source[cx][ly - 1]))) {
                        relax(source, cx, cy, cx, ly);
                        break;
                    }
                }
                for (var ry = cy + 1; ry < source[cx].Length; ++ry) {
                    if (char.IsLetter(source[cx][ry]) && char.IsWhiteSpace(source[cx][ry - 1])) {
                        relax(source, cx, cy, cx, ry);
                        break;
                    }
                }
            }
            return best[finish[0], finish[1]];
        }

        private void relax(string[] source, int cx, int cy, int nx, int ny) {
            if (0 <= nx && nx < source.Length && 0 <= ny && ny < 50) {
                if (best[nx, ny] > best[cx, cy] + 1) {
                    best[nx, ny] = best[cx, cy] + 1;
                    queue.Enqueue(new[] { nx, ny });
                }
            }
        }

        private int[,] best;
        private Queue<int[]> queue;
    }
}
