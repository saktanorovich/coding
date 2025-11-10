import java.io.*;
import java.util.*;

public class Solution_0675 {
    public int cutOffTree(List<List<Integer>> forest) {
        if (forest == null || forest.size() == 0) {
            return 0;
        }
        int[][] a = new int[forest.size()][];
        List<Point> p = new ArrayList<>();
        for (int i = 0; i < a.length; ++i) {
            a[i] = new int[forest.get(i).size()];
            for (int j = 0; j < a[i].length; ++j) {
                a[i][j] = forest.get(i).get(j);
                int w = a[i][j];
                if (w > 1) {
                    p.add(new Point(i, j, w));
                }
            }
        }
        return cutOffTree(a, a.length, a[0].length, p, new Point(0, 0));
    }

    private int cutOffTree(int[][] a, int n, int m, List<Point> p, Point source) {
        p.sort(Comparator.naturalOrder());
        int res = 0;
        for (Point target : p) {
            int dist = bfs(a, n, m, source, target);
            if (dist >= 0) {
                res += dist;
                a[target.x][target.y] = 1;
                source = target;
            } else {
                return -1;
            }
        }
        return res;
    }

    private int bfs(int[][] a, int n, int m, Point source, Point target) {
        Queue<Point> q = new LinkedList<>();
        int[][] dist = new int[n][m];
        for (int i = 0; i < n; ++i) {
            Arrays.fill(dist[i], Integer.MAX_VALUE);
        }
        dist[source.x][source.y] = 0;
        for (q.add(source); q.isEmpty() == false; q.poll()) {
            int cx = q.peek().x;
            int cy = q.peek().y;
            if (cx == target.x && cy == target.y) {
                return dist[target.x][target.y];   
            }
            for (int k = 0; k < 4; ++k) {
                int nx = cx + dx[k];
                int ny = cy + dy[k];
                if (0 <= nx && nx < n && 0 <= ny && ny < m) {
                    if (dist[nx][ny] > dist[cx][cy] + 1) {
                        if (a[nx][ny] > 0) {
                            dist[nx][ny] = dist[cx][cy] + 1;
                            q.add(new Point(nx, ny));
                        }
                    }
                }
            }

        }
        return -1;
    }

    private static final int[] dx = { -1, 0, +1, 0 };
    private static final int[] dy = { 0, -1, 0, +1 };

    private class Point implements Comparable<Point> {
        public final Integer x;
        public final Integer y;
        public final Integer w;

        public Point(int x, int y) {
            this.x = x;
            this.y = y;
            this.w = 0;
        }

        public Point(int x, int y, int w) {
            this.x = x;
            this.y = y;
            this.w = w;
        }

        @Override
        public int compareTo(Point o) {
            return w.compareTo(o.w);
        }
    }
}
