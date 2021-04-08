package kickstart2017.rounda;

import java.io.*;
import java.util.*;
import utils.io.*;

// Problem B
public class PatternsOverlap {
    public void process(int testCase, InputReader in, PrintWriter out) {
        String a = in.next();
        String b = in.next();
        boolean res = process(a, a.length(), b, b.length());
        out.format("Case #%d: %s\n", testCase, res ? "TRUE" : "FALSE");
    }

    private boolean process(String a, int n, String b, int m) {
        // we need to build NFA for both strings and determine if
        // there is exist a path from start to end under the same
        // set of signals but instead of building NFA let's simulate
        queue = new LinkedList<>();
        color = new boolean[n + 1][m + 1];
        for (add(n, m, 0, 0); queue.isEmpty() == false;) {
            int x = queue.poll();
            int y = queue.poll();

            char ac = x < n ? a.charAt(x) : '\0';
            char bc = y < m ? b.charAt(y) : '\0';

            if (ac != '*' && bc != '*') {
                if (ac == bc) {
                    add(n, m, x + 1, y + 1);
                }
                continue;
            }
            if (ac == '*') {
                add(n, m, x + 1, y);
                for (int c = 0, i = y; i < m && c < 4; ++i) {
                    if (b.charAt(i) != '*') {
                        add(n, m, x + 1, i + 1);
                        c = c + 1;
                    } else {
                        add(n, m, x + 1, i + 1);
                    }
                }
            }
            if (bc == '*') {
                add(n, m, x, y + 1);
                for (int c = 0, i = x; i < n && c < 4; ++i) {
                    if (a.charAt(i) != '*') {
                        add(n, m, i + 1, y + 1);
                        c = c + 1;
                    } else {
                        add(n, m, i + 1, y + 1);
                    }
                }
            }
        }
        return color[n][m];
    }

    private void add(int n, int m, int x, int y) {
        if (x <= n && y <= m) {
            if (color[x][y] == false) {
                color[x][y] = true;
                queue.add(x);
                queue.add(y);
            }
        }
    }

    private Queue<Integer> queue;
    boolean[][] color;
}
