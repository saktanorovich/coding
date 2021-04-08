package practice.contest2;

import java.io.*;
import utils.io.*;

// Problem B
public class AlwaysTurnLeft {
    public void process(int testCase, InputReader in, PrintWriter out) {
        String entrance_to_exit = in.next();
        String exit_to_entrance = in.next();
        byte[][] maze = build(entrance_to_exit, exit_to_entrance);
        out.format("Case #%d:\n", testCase);
        for (int i = 0; i < maze.length; ++i) {
            for (int j = 0; j < maze[i].length; ++j) {
                out.format("%x", maze[i][j]);
            }
            out.println();
        }
    }

    private static byte[][] build(String F, String B) {
        byte[][] grid = new byte[MAX][MAX + MAX];
        int[] pos = new int[] { 0, MAX, 1 };
        int[] dim = new int[] { 0, MAX, 0 };
        build(F, grid, pos, dim);
        build(B, grid, pos, dim);
        int xmin = 1;
        int xmax = dim[0];
        int ymin = dim[1];
        int ymax = dim[2];
        int R = xmax - xmin + 1;
        int C = ymax - ymin + 1;
        byte[][] res = new byte[R][C];
        for (int i = 0; i < R; ++i) {
            for (int j = 0; j < C; ++j) {
                res[i][j] = grid[xmin + i][ymin + j];
            }
        }
        return res;
    }

    private static void build(String s, byte[][] grid, int[] pos, int[] dim) {
        int x = pos[0];
        int y = pos[1];
        int d = pos[2];
        int xmax = dim[0];
        int ymin = dim[1];
        int ymax = dim[2];
        for (int i = 0; i < s.length(); ++i) {
            if (s.charAt(i) == 'W') {
                grid[x][y] |= 1 << (d);
                x += dx[d];
                y += dy[d];
                grid[x][y] |= 1 << (d ^ 1);
                if (i + 1 < s.length()) {
                    xmax = Math.max(xmax, x);
                    ymin = Math.min(ymin, y);
                    ymax = Math.max(ymax, y);
                }
            } else if (s.charAt(i) == 'R') {
                d = dR[d];
            } else if (s.charAt(i) == 'L') {
                d = dL[d];
            }
        }
        d = dL[d];
        d = dL[d];
        pos[0] = x;
        pos[1] = y;
        pos[2] = d;
        dim[0] = xmax;
        dim[1] = ymin;
        dim[2] = ymax;
    }

    private static final int MAX = 16536;

    private static final int dx[] = { -1, +1,  0,  0 };
    private static final int dy[] = {  0,  0, -1, +1 };

    private static final int dL[] = { 2, 3, 1, 0 };
    private static final int dR[] = { 3, 2, 0, 1 };
}
