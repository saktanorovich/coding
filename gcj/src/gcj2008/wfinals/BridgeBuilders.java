package gcj2008.wfinals;

import utils.io.*;
import java.io.*;
import java.util.*;

// Problem D
public class BridgeBuilders {
    public void process(int testCase, InputReader in, PrintWriter out) {
        this.n = in.nextInt();
        this.m = in.nextInt();
        this.a = new int[n + 2][m + 2];
        this.b = new int[n + 2][m + 2];
        this.w = new int[n + 2][m + 2];
        for (int i = 0; i <= n + 1; ++i) {
            Arrays.fill(b[i], +oo);
        }
        for (int i = 1; i <= n; ++i) {
            String s = in.next();
            for (int j = 1; j <= m; ++j) {
                a[i][j] = ".#T".indexOf(s.charAt(j - 1));
            }
        }
        List<Integer> tx = new ArrayList<>();
        List<Integer> ty = new ArrayList<>();
        tx.add(1);
        ty.add(1);
        b[1][1] = 0;
        // Prim's algorithm to build mst
        while (true) {
            int[][] d = new int[n + 2][m + 2];
            int[][] u = new int[n + 2][m + 2];
            int[][] v = new int[n + 2][m + 2];
            for (int i = 0; i <= n + 1; ++i) {
                Arrays.fill(d[i], +oo);
            }
            // let's run bfs from available trees
            Queue<Integer> q = new LinkedList<>();
            for (int i = 0; i < tx.size(); ++i) {
                int x = tx.get(i);
                int y = ty.get(i);
                q.add(x);
                q.add(y);
                d[x][y] = 0;
            }
            while (q.isEmpty() == false) {
                int x = q.poll();
                int y = q.poll();
                for (int k = 0; k < 4; ++k) {
                    int nx = x + dx[k];
                    int ny = y + dy[k];
                    if (a[nx][ny] > 0) {
                        if (d[nx][ny] > d[x][y] + 1) {
                            d[nx][ny] = d[x][y] + 1;
                            u[nx][ny] = x;
                            v[nx][ny] = y;
                            q.add(nx);
                            q.add(ny);
                        }
                    }
                }
            }
            int x = -1;
            int y = -1;
            for (int i = 1; i <= n; ++i) {
                for (int j = 1; j <= m; ++j) {
                    if (w[i][j] == 0) {
                        b[i][j] = Math.min(b[i][j], d[i][j]);
                        if (a[i][j] == 2) {
                            if (x == -1 || d[x][y] > d[i][j]) {
                                x = i;
                                y = j;
                            }
                        }
                    }
                }
            }
            if (x == -1 && y == -1) {
                break;
            }
            tx.add(x);
            ty.add(y);
            while (x != 0 && y != 0) {
                w[x][y] = 1;
                int px = u[x][y];
                int py = v[x][y];
                x = px;
                y = py;
            }
        }
        int res = 0;
        for (int x = 1; x <= n; ++x) {
            for (int y = 1; y <= m; ++y) {
                if (a[x][y] > 0) {
                    res += b[x][y];
                }
            }
        }
        out.format("Case #%d: %d\n", testCase, res);
    }

    private int a[][];
    private int b[][];
    private int w[][];
    private int n;
    private int m;

    private static final int dx[] = { -1, 0, +1, 0 };
    private static final int dy[] = { 0, -1, 0, +1 };
    private static final int oo = (int)1e6;
}
