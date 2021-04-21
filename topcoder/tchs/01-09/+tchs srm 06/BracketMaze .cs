using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class BracketMaze {
        public int properPaths(string[] maze, int n) {
            return properPaths(string.Join(string.Empty, maze), n);
        }

        private static int properPaths(string maze, int n) {
            var cube = new int[n + 1, n + 1, n + 1];
            for (var x = 1; x <= n; ++x)
                for (var y = 1; y <= n; ++y)
                    for (var z = 1; z <= n; ++z) {
                        cube[x, y, z] = map[maze[(x - 1) * n * n + (y - 1) * n + z - 1]];
                    }
            return properPaths(cube, n);
        }

        private static int properPaths(int[,,] maze, int n) {
            var cnt = new long[n + 1, n + 1, n + 1, 3 * n + 1];
            if (maze[1, 1, 1] >= 0) {
                cnt[1, 1, 1, maze[1, 1, 1]] = 1;
            }
            for (var x = 1; x <= n; ++x)
                for (var y = 1; y <= n; ++y)
                    for (var z = 1; z <= n; ++z)
                        for (int k = 0; k <= 2 * n; ++k) {
                            if (x + 1 <= n) relax(cnt, maze, x + 1, y, z, k, cnt[x, y, z, k]);
                            if (y + 1 <= n) relax(cnt, maze, x, y + 1, z, k, cnt[x, y, z, k]);
                            if (z + 1 <= n) relax(cnt, maze, x, y, z + 1, k, cnt[x, y, z, k]);
                        }
            if (cnt[n, n, n, 0] > (int)1e9) {
                return -1;
            }
            return (int)cnt[n, n, n, 0];
        }

        private static void relax(long[,,,] cnt, int[,,] maze, int x, int y, int z, int k, long now) {
            k += maze[x, y, z];
            if (k >= 0) {
                cnt[x, y, z, k] += now;
                if (cnt[x, y, z, k] > (int)1e9) {
                    cnt[x, y, z, k] = (int)1e9 + 1;
                }
            }
        }

        private static readonly IDictionary<char, int> map =
            new Dictionary<char, int> {
                { '.', +0 },
                { '(', +1 },
                { ')', -1 }};
    }
}