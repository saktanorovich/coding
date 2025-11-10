package blitz2017.finals;

import java.io.*;
import java.util.*;
import utils.io.*;

public class ProblemA {
    public boolean process(int testCase, InputReader in, PrintWriter out) {
        int n = in.nextInt();
        int m = in.nextInt();
        int k = in.nextInt();
        int a[][] = new int[n][m];
        for (int i = 0; i < k; ++i) {
            int x = in.nextInt();
            int y = in.nextInt();
            a[x - 1][y - 1] = 1;
        }
        int moves = 0;
        for (int i = 0; i < n; ++i) {
            for (int j = 0; j < m; ++j) {
                if (a[i][j] == 0) {
                    a[i][j] = 1;
                    moves = moves + 1;
                    Queue<Integer> q = new LinkedList<>();
                    q.add(i);
                    q.add(j);
                    while (!q.isEmpty()) {
                        int x = q.poll();
                        int y = q.poll();
                        a[x][y] = 1;
                        for (int z = 0; z < 4; ++z) {
                            int xx = x + dx[z];
                            int yy = y + dy[z];
                            if (0 <= xx && xx < n && 0 <= yy && yy < m) {
                                if (a[xx][yy] == 0) {
                                    a[xx][yy] = 1;
                                    q.add(xx);
                                    q.add(yy);
                                }
                            }
                        }
                    }
                }
            }
        }
        out.println(moves);
        return true;
    }

    private static final int[] dx = new int[]{-1, 0, +1, 0};
    private static final int[] dy = new int[]{0, -1, 0, +1};
}
