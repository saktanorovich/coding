public class Solution {
    public int LargestIsland(int[][] grid) {
        return LargestIsland(grid, grid.Length);
    }

    private int LargestIsland(int[][] grid, int n) {
        var comp = new List<HashSet<int>>();
        var used = new bool[n * n];
        for (var cx = 0; cx < n; ++cx) {
            for (var cy = 0; cy < n; ++cy) {
                if (grid[cx][cy] == 1) {
                    var curr = cx * n + cy;
                    if (used[curr] == false) {
                        var have = new HashSet<int>();
                        dfs(grid, have, used, n, curr);
                        comp.Add(have);
                    }
                }
            }
        }
        var indx = new int[n, n];
        for (var c = 0; c < comp.Count; ++c) {
            foreach (var coord in comp[c]) {
                var cx = coord / n;
                var cy = coord % n;
                indx[cx, cy] = c;
            }
        }
        var answ = Utils.Max(comp.Select(c => c.Count));
        for (var cx = 0; cx < n; ++cx) {
            for (var cy = 0; cy < n; ++cy) {
                if (grid[cx][cy] == 0) {
                    var have = new HashSet<int>();
                    for (var k = 0; k < 4; ++k) {
                        var nx = cx + dx[k];
                        var ny = cy + dy[k];
                        if (0 <= nx && nx < n && 0 <= ny && ny < n) {
                            if (grid[nx][ny] == 1) {
                                have.Add(indx[nx, ny]);
                            }
                        }
                    }
                    var summ = 1 + Utils.Sum(have.Select(c => comp[c].Count));
                    if (answ < summ) {
                        answ = summ;
                    }
                }
            }
        }
        return answ;
    }

    private void dfs(int[][] grid, HashSet<int> comp, bool[] used, int n, int curr) {
        if (used[curr]) {
            return;
        }
        used[curr] = true;
        comp.Add(curr);
        for (var k = 0; k < 4; ++k) {
            var cx = curr / n;
            var cy = curr % n;
            var nx = cx + dx[k];
            var ny = cy + dy[k];
            if (0 <= nx && nx < n && 0 <= ny && ny < n) {
                if (grid[nx][ny] == 1) {
                    var next = nx * n + ny;
                    dfs(grid, comp, used, n, next);
                }
            }
        }
    }

    private static class Utils {
        public static int Max(IEnumerable<int> val) {
            var res = 0;
            foreach (var x in val) {
                if (res < x) {
                    res = x;
                }
            }
            return res;
        }

        public static int Sum(IEnumerable<int> val) {
            var res = 0;
            foreach (var x in val) {
                res += x;
            }
            return res;
        }
    }

    private readonly int[] dx = { -1, 0, +1, 0 };
    private readonly int[] dy = { 0, -1, 0, +1 };
}