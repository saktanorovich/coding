package gcj2008.wfinals;

import utils.io.*;
import java.io.*;
import java.util.*;
import java.util.function.*;

// Problem B
public class PingPongBalls {
    public void process(int testCase, InputReader in, PrintWriter out) {
        int X = in.nextInt();
        int Y = in.nextInt();
        int dx[] = new int[2];
        int dy[] = new int[2];
        dx[0] = in.nextInt();
        dy[0] = in.nextInt();
        dx[1] = in.nextInt();
        dy[1] = in.nextInt();
        int x = in.nextInt();
        int y = in.nextInt();
        long res;
        if (dx[0] * dy[1] - dy[0] * dx[1] == 0) {
            res = collinear(X, Y, dx, dy, x, y);
        } else {
            res = rectangle(X, Y, dx, dy, x, y);
        }
        out.format("Case #%d: %d\n", testCase, res);
    }

    private long collinear(int X, int Y, int[] dx, int[] dy, int x, int y) {
        if (dx[0] == 0 && dy[0] == 0) {
            return collinear(Y, X, dy, dx, y, x);
        }
        boolean u[] = new boolean[X];
        Queue<Integer> q = new LinkedList<>();
        q.add(x);
        q.add(y);
        u[x] = true;
        int res = 0;
        while (q.isEmpty() == false) {
            int cx = q.poll();
            int cy = q.poll();
            res++;
            for (int k = 0; k < 2; ++k) {
                int nx = cx + dx[k];
                int ny = cy + dy[k];
                if (0 <= nx && nx < X && 0 <= ny && ny < Y) {
                    if (u[nx] == false) {
                        u[nx] = true;
                        q.add(nx);
                        q.add(ny);
                    }

                }
            }
        }
        return res;
    }

    private long rectangle(int X, int Y, int[] dx, int[] dy, int x, int y) {
        // We can transform rect coordinates to skew coordinates
        // using the following rule
        //   x+a1*X1+a2*X2 -> a1
        //   y+a1*Y1+a2*Y2 -> a2
        // where (x,y)->(0,0), a1,a2 >= 0.
        // So in order to find the required number of points we
        // can iterate over a1, fix a2 at min and max possible
        // values and add to result (a2-a1+1) as a number of
        // points per a1. We iterate until a1 > a2.
        int min = 0, max = (int)2e7;
        BiFunction<Integer, Integer, Boolean> okay = (a1, a2) -> {
            int nx = x + a1 * dx[0] + a2 * dx[1];
            int ny = y + a1 * dy[0] + a2 * dy[1];
            return 0 <= nx && nx < X && 0 <= ny && ny < Y;
        };
        long res = 0;
        for (int fix = 0; min <= max; ++fix) {
            while (min <= max && okay.apply(fix, max) == true) {
                max = max + 1;
            }
            while (min <= max && okay.apply(fix, max) == false) {
                max = max - 1;
            }
            while (min <= max && okay.apply(fix, min) == false) {
                min = min + 1;
            }
            if (min <= max) {
                res += max - min + 1;
            }
        }
        return res;
    }
}
