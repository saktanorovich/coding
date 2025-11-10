import java.util.*;

    public class Arcs {
        public int numArcs(String[] grid) {
            this.grid = grid;
            done = new boolean[grid.length + 1][grid[0].length() + 1];
            done[0][0] = true;
            queue = new LinkedList<>();
            queue.add(0);
            queue.add(0);
            queue.add(0);
            while (queue.isEmpty() == false) {
                int x = queue.poll();
                int y = queue.poll();
                int d = queue.poll();
                if (x == grid.length && y == grid[0].length()) {
                    return d;
                }
                for (int k = 1; k <= Math.min(grid.length, grid[0].length()); ++k) {
                    move(x, y, x - k, y + k, d + 1);
                    move(x, y, x + k, y - k, d + 1);
                    move(x, y, x + k, y + k, d + 1);
                    move(x, y, x - k, y - k, d + 1);
                }
            }
            return -1;
        }

        private void move(int x1, int y1, int x2, int y2, int dd) {
            if (x2 < 0 || x2 > grid.length || y2 < 0 || y2 > grid[0].length()) {
                return;
            }
            if (done[x2][y2] == false) {
                int r = Math.abs(x2 - x1);
                boolean one = canMove(r, x1, y2, sgn(x2 - x1), sgn(y1 - y2));
                boolean two = canMove(r, x2, y1, sgn(x1 - x2), sgn(y2 - y1));
                if (one || two) {
                    done[x2][y2] = true;
                    queue.add(x2);
                    queue.add(y2);
                    queue.add(dd);
                }
            }
        }

        private boolean canMove(int r, int cx, int cy, int dx, int dy) {
            int xmin = Math.min(cx, cx + r * dx);
            int xmax = Math.max(cx, cx + r * dx);
            int ymin = Math.min(cy, cy + r * dy);
            int ymax = Math.max(cy, cy + r * dy);
            for (int x = xmin; x < xmax; ++x)
                for (int y = ymin; y < ymax; ++y) {
                    if (cover(r, cx, cy, x, y)) {
                        return false;
                    }
                }
            return true;
        }

        // return true if the arc with radius r at center (cx, cy)
        // covers block with upper-left corner at (x, y)
        private boolean cover(int r, int cx, int cy, int x, int y) {
            if (grid[x].charAt(y) == '#') {
                int x1 = sqr(x - cx);
                int y1 = sqr(y - cy);
                int x2 = sqr(x - cx + 1);
                int y2 = sqr(y - cy + 1);

                if (x1 + y1 < r * r && x2 + y2 > r * r) return true;
                if (x1 + y1 > r * r && x2 + y2 < r * r) return true;
                if (x1 + y2 < r * r && x2 + y1 > r * r) return true;
                if (x1 + y2 > r * r && x2 + y1 < r * r) return true;
            }
            return false;
        }

        private static int sgn(int x) {
            if (x < 0) {
                return -1;
            }
            if (x > 0) {
                return +1;
            }
            return 0;
        }

        private static int sqr(int x) {
            return x * x;
        }

        private String[] grid;
        private boolean[][] done;
        private Queue<Integer> queue;
    }