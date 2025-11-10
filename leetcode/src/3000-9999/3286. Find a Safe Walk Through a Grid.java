class Solution {
    public boolean findSafeWalk(List<List<Integer>> grid, int health) {
        var n = grid.size();
        var m = grid.get(0).size();
        var f = new int[n][m];
        f[0][0] = health;
        var q = new LinkedList<Integer>();
        q.add(0);
        q.add(0);
        while (q.isEmpty() == false) {
            var cx = q.poll();
            var cy = q.poll();
            for (var k = 0; k < 4; ++k) {
                var nx = cx + dx[k];
                var ny = cy + dy[k];
                if (0 <= nx && nx < n && 0 <= ny && ny < m) {
                    var eat = grid.get(cx).get(cy);
                    if (f[nx][ny] < f[cx][cy] - eat) {
                        f[nx][ny] = f[cx][cy] - eat;
                        q.add(nx);
                        q.add(ny);
                    }
                }
            }
        }
        f[n - 1][m - 1] -= grid.get(n - 1).get(m - 1);
        return f[n - 1][m - 1] >= 1;
    }

    private final int[] dx = { -1,  0, +1,  0 };
    private final int[] dy = {  0, +1,  0, -1 };
}