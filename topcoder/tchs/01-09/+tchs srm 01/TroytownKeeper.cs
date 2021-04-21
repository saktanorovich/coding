using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class TroytownKeeper {
        public int limeLiters(string[] maze) {
            return limeLitersImpl(extend(maze).ToArray(), maze.Length + 2, maze[0].Length + 2);
        }

        private int limeLitersImpl(string[] maze, int n, int m) {
            var result = 0;
            var analyzed = new bool[n, m];
            for (var i = 0; i < n; ++i) {
                for (var j = 0; j < m; ++j) {
                    if (i == 0 || i == n - 1 ||
                        j == 0 || j == m - 1) {
                        if (analyzed[i, j]) {
                            continue;
                        }
                        var queue = new Queue<int>();
                        analyzed[i, j] = true;
                        for (queue.Enqueue(i * m + j); queue.Count > 0; queue.Dequeue()) {
                            var x = queue.Peek() / m;
                            var y = queue.Peek() % m;
                            for (var k = 0; k < 4; ++k) {
                                var nx = x + dx[k];
                                var ny = y + dy[k];
                                if (0 <= nx && nx < n && 0 <= ny && ny < m) {
                                    if (analyzed[nx, ny] == false) {
                                        if (maze[nx][ny] == '#') {
                                            ++result;
                                        }
                                        else {
                                            analyzed[nx, ny] = true;
                                            queue.Enqueue(nx * m + ny);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        private static string[] extend(string[] maze) {
            var res = new List<string>();
            res.Add(".".PadLeft(maze[0].Length + 2));
            res.AddRange(maze.Select(row => "." + row + "."));
            res.Add(".".PadLeft(maze[0].Length + 2));
            return res.ToArray();
        }

        private static int[] dx = { -1, 0, +1, 0 };
        private static int[] dy = { 0, -1, 0, +1 };
    }
}
