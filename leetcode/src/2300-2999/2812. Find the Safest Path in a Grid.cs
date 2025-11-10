public class Solution {
    public int MaximumSafenessFactor(IList<IList<int>> grid) {
        return MaximumSafenessFactor(grid, grid.Count, grid[0].Count);
    }

    private int MaximumSafenessFactor(IList<IList<int>> grid, int n, int m) {
        var cost = build(grid, n, m);
        int lo = 0, hi = 1_000;
        while (lo < hi) {
            var d = (lo + hi + 1) / 2;
            if (reach(grid, cost, n, m, d)) {
                lo = d;
            } else {
                hi = d - 1;
            }
        }
        return lo;
    }

    private bool reach(IList<IList<int>> grid, int[,] cost, int n, int m, int d) {
        if (cost[0, 0] < d) {
            return false;
        }
        var here = new bool[n, m];
        var queu = new Queue<int>();
        here[0, 0] = true;
        queu.Enqueue(0);
        queu.Enqueue(0);
        while (queu.Count > 0) {
            var currX = queu.Dequeue();
            var currY = queu.Dequeue();
            if (currX == n - 1 && currY == m - 1) {
                return true;
            }
            for (var k = 0; k < 4; ++k) {
                var nextX = currX + dx[k];
                var nextY = currY + dy[k];
                if (0 <= nextX && nextX < n && 0 <= nextY && nextY < m) {
                    if (here[nextX, nextY] == false && cost[nextX, nextY] >= d) {
                        here[nextX, nextY] = true;
                        queu.Enqueue(nextX);
                        queu.Enqueue(nextY);
                    }
                }
            }
        }
        return false;
    }

    private int[,] build(IList<IList<int>> grid, int n, int m) {
        var cost = new int[n, m];
        var queu = new Queue<int>();
        for (var i = 0; i < n; ++i) {
            for (var j = 0; j < m; ++j) {
                cost[i, j] = 1_000;
                if (grid[i][j] == 1) {
                    cost[i, j] = 0;
                    queu.Enqueue(i);
                    queu.Enqueue(j);
                }
            }
        }
        while (queu.Count > 0) {
            var currX = queu.Dequeue();
            var currY = queu.Dequeue();
            for (var k = 0; k < 4; ++k) {
                var nextX = currX + dx[k];
                var nextY = currY + dy[k];
                if (0 <= nextX && nextX < n && 0 <= nextY && nextY < m) {
                    if (cost[nextX, nextY] > cost[currX, currY] + 1) {
                        cost[nextX, nextY] = cost[currX, currY] + 1;
                        queu.Enqueue(nextX);
                        queu.Enqueue(nextY);
                    }
                }
            }
        }
        return cost;
    }

    private static readonly int[] dx = { -1,  0, +1,  0 };
    private static readonly int[] dy = {  0, -1,  0, +1 };
}