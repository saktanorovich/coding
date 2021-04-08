package gcj2008.rd3;

import utils.io.*;
import java.io.*;
import java.util.*;

// Problem A
public class HowBigAreThePockets {
    public void process(int testCase, InputReader in, PrintWriter out) {
        int L = in.nextInt();
        List<Point> ps = new ArrayList<>();
        Point a = new Point(0, 0);
        Point d = new Point(0, 1);
        ps.add(a);
        for (int i = 0; i < L; ++i) {
            String s = in.next();
            for (int t = in.nextInt(); t > 0; --t) {
                for (int k = 0; k < s.length(); ++k) {
                    switch (s.charAt(k)) {
                        case 'F':
                            a = new Point(a.x + d.x, a.y + d.y);
                            ps.add(a);
                            break;
                        case 'L':
                            d = new Point(-d.y, +d.x);
                            break;
                        case 'R':
                            d = new Point(+d.y, -d.x);
                            break;
                    }
                }
            }
        }
        ps.add(a);
        int xmin = 0;
        int ymin = 0;
        int xmax = 0;
        int ymax = 0;
        for (Point p : ps) {
            xmin = Math.min(xmin, p.x);
            ymin = Math.min(ymin, p.y);
            xmax = Math.max(xmax, p.x);
            ymax = Math.max(ymax, p.y);
        }
        int n = xmax - xmin;
        int m = ymax - ymin;
        int map[][] = new int[n + 1][m + 1];
        for (int i = 0; i + 1 < ps.size(); ++i) {
            Point p1 = ps.get(i);
            Point p2 = ps.get(i + 1);
            int x1 = Math.min(p1.x, p2.x);
            int x2 = Math.max(p1.x, p2.x);
            for (int x = x1; x < x2; ++x) {
                map[x - xmin][p1.y - ymin] = 1;
            }
        }
        int[][] grid = new int[n][m];
        for (int x = 0; x + 1 <= n; ++x) {
            int hitsCount = 0;
            for (int y = 0; y < m; ++y) {
                hitsCount ^= map[x][y];
                grid[x][y] = hitsCount;
            }
        }
        out.format("Case #%d: %d\n", testCase, square(hull(grid)) - square(grid));
    }

    private static int[][] hull(int[][] grid) {
        int n = grid.length;
        int m = grid[0].length;
        int[] xmin = new int[m];
        int[] xmax = new int[m];
        Arrays.fill(xmin, Integer.MAX_VALUE);
        Arrays.fill(xmax, Integer.MIN_VALUE);
        int[][] hull = new int[n][m];
        for (int x = 0; x < n; ++x) {
            int ymin = Integer.MAX_VALUE;
            int ymax = Integer.MIN_VALUE;
            for (int y = 0; y < m; ++y) {
                if (grid[x][y] > 0) {
                    xmin[y] = Math.min(xmin[y], x);
                    xmax[y] = Math.max(xmax[y], x);
                    ymin = Math.min(ymin, y);
                    ymax = Math.max(ymax, y);
                }
            }
            for (int y = ymin; y <= ymax; ++y) {
                hull[x][y] = 1;
            }
        }
        for (int y = 0; y < m; ++y) {
            for (int x = xmin[y]; x <= xmax[y]; ++x) {
                hull[x][y] = 1;
            }
        }
        return hull;
    }

    private static int square(int[][] grid) {
        int s = 0;
        for (int[] row : grid) {
            for (int c : row) {
                s += c;
            }
        }
        return s;
    }

    private static class Point {
        public final int x;
        public final int y;

        public Point(int x, int y) {
            this.x = x;
            this.y = y;
        }
    }
}
