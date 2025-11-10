import java.util.*;

    public class SimplePath {
        public int trouble(String direction, int[] length) {
            int[] xs = new int[length.length + 1];
            int[] ys = new int[length.length + 1];
            for (int i = 0, x = 0, y = 0; i < direction.length(); ++i) {
                xs[i + 1] = x + dx["NESW".indexOf(direction.charAt(i))] * length[i];
                ys[i + 1] = y + dy["NESW".indexOf(direction.charAt(i))] * length[i];
                x = xs[i + 1];
                y = ys[i + 1];
            }
            for (int i = 0; i < direction.length(); ++i) {
                for (int j = i + 2; j < direction.length(); ++j) {
                    if (collide(xs[i], ys[i], xs[i + 1], ys[i + 1], xs[j], ys[j], xs[j + 1], ys[j + 1])) {
                        return i;
                    }
                }
                if (i + 1 < direction.length()) {
                    if (direction.charAt(i) == 'N' && direction.charAt(i + 1) == 'S') return i;
                    if (direction.charAt(i) == 'S' && direction.charAt(i + 1) == 'N') return i;
                    if (direction.charAt(i) == 'E' && direction.charAt(i + 1) == 'W') return i;
                    if (direction.charAt(i) == 'W' && direction.charAt(i + 1) == 'E') return i;
                }
            }
            return -1;
        }

        private static boolean collide(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4) {
            if (x1 > x2) return collide(x2, y1, x1, y2, x3, y3, x4, y4);
            if (y1 > y2) return collide(x1, y2, x2, y1, x3, y3, x4, y4);
            if (x3 > x4) return collide(x1, y1, x2, y2, x4, y3, x3, y4);
            if (y3 > y4) return collide(x1, y1, x2, y2, x3, y4, x4, y3);

            return collide(x1, x2, x3, x4) && collide(y1, y2, y3, y4);
        }

        private static boolean collide(int x1, int x2, int x3, int x4) {
            if (x2 > x4) {
                return collide(x3, x4, x1, x2);
            }
            return x1 <= x4 && x3 <= x2;
        }

        private static final int[] dx = {-1, 0, +1, 0};
        private static final int[] dy = {0, +1, 0, -1};
    }
