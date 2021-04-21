class Solution {
  public int getSecondsRequired(int R, int C, char[,] G) {
    var P = new List<int>[26];
    for (var c = 0; c < 26; ++c) {
      P[c] = new List<int>();
    }
    for (var x = 0; x < R; ++x) {
      for (var y = 0; y < C; ++y) {
        if ('a' <= G[x, y] && G[x, y] <= 'z') {
          P[G[x, y] - 'a'].Add(x * C + y);
        }
      }
    }
    for (var x = 0; x < R; ++x) {
      for (var y = 0; y < C; ++y) {
        if (G[x, y] == 'S') {
          return solve(G, P, R, C, x, y);
        }
      }
    }
    return -1;
  }

  private int solve(char[,] G, List<int>[] P, int R, int C, int X, int Y) {
    var time = new int[R, C];
    for (var x = 0; x < R; ++x) {
      for (var y = 0; y < C; ++y) {
        time[x, y] = int.MaxValue / 2;
      }
    }
    time[X, Y] = 0;
    var queue = new Queue<int>();
    queue.Enqueue(X);
    queue.Enqueue(Y);
    while (queue.Count > 0) {
      var cx = queue.Dequeue();
      var cy = queue.Dequeue();
      if (G[cx, cy] == 'E') {
        return time[cx, cy];
      }
      for (var k = 0; k < 4; ++k) {
        var nx = cx + dx[k];
        var ny = cy + dy[k];
        if (0 <= nx && nx < R && 0 <= ny && ny < C) {
          if (G[nx, ny] == '#') {
            continue;
          }
          if (time[nx, ny] > time[cx, cy] + 1) {
              time[nx, ny] = time[cx, cy] + 1;
              queue.Enqueue(nx);
              queue.Enqueue(ny);
          }
        }
      }
      var symb = G[cx, cy];
      if ('a' <= symb && symb <= 'z') {
        var c = symb - 'a';
        foreach (var p in P[c]) {
          var px = p / C;
          var py = p % C;
          foreach (var to in P[c]) {
            var nx = to / C;
            var ny = to % C;
            if (time[nx, ny] > time[px, py] + 1) {
                time[nx, ny] = time[px, py] + 1;
                queue.Enqueue(nx);
                queue.Enqueue(ny);
            }
          }
        }
        P[c].Clear();
      }
    }
    return -1;
  }

  private static readonly int[] dx = { -1,  0, +1,  0 };
  private static readonly int[] dy = {  0, -1,  0, +1 };
}
