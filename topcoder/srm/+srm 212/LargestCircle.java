import java.util.*;

    public class LargestCircle {
        public int radius(String[] grid) {
            int n = grid.length;
            int m = grid[0].length();
            for (int r = Math.min(n, m); r > 0; --r) {
                for (int x = 0; x < n; ++x) {
                    for (int y = 0; y < m; ++y) {
                        if (possible(grid, n, m, r, x, y)) {
                            return r;
                        }
                    }
                }
            }
            return 0;
        }

        private static boolean possible(String[] grid, int n, int m, int r, int cx, int cy) {
            if (cx - r < 0 || cx + r > n || cy - r < 0 || cy + r > m) {
                return false;
            }
            for (int x = 0; x < n; ++x) {
                for (int y = 0; y < m; ++y) {
                    if (grid[x].charAt(y) == '#') {
                        int x1 = sqr(x - cx);
                        int y1 = sqr(y - cy);
                        int x2 = sqr(x - cx + 1);
                        int y2 = sqr(y - cy + 1);

                        if (x1 + y1 < r * r && x2 + y2 > r * r) return false;
                        if (x1 + y1 > r * r && x2 + y2 < r * r) return false;
                        if (x1 + y2 < r * r && x2 + y1 > r * r) return false;
                        if (x1 + y2 > r * r && x2 + y1 < r * r) return false;
                    }
                }
            }
            return true;
        }

        private static int sqr(int x) {
            return x * x;
        }
    }