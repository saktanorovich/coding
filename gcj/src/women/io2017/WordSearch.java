package women.io2017;

import java.io.*;
import utils.io.*;

// Problem C
public class WordSearch {
    public void process(int testCase, InputReader in, PrintWriter out) {
        int d = in.nextInt();
        int n = in.nextInt();
        if (d > 15) {
            d = 15;
        }
        if (d < 15) {
            d = 15;
        }
        char[][] g = new char[d][d];
        for (int i = 0; i < d; ++i) {
            g[i] = "I/O/I/O/I/O/I/O".toCharArray();
        }
        int m = MAX - n;
        for (int i = 1; i < d - 1; i += 1) {
            for (int j = 1; j < d; j += 2) {
                if (m > 2) {
                    g[i][j] = 'O';
                    m -= 3;
                }
            }
        }
        for (int i = 0; i < d; i += d - 1) {
            for (int j = 1; j < d; j += 2) {
                if (m > 0) {
                    g[i][j] = 'O';
                    m -= 1;
                }
            }
        }
        assert m == 0 && count(g, d) == n : "incorrect grid";
        out.format("Case #%d:\n", testCase);
        for (int i = 0; i < d; ++i) {
            out.println(new String(g[i]));
        }
    }

    private static int count(char[][] g, int d) {
        int res = 0;
        for (int x = 0; x < d; ++x) {
            for (int y = 0; y < d; ++y) {
                if (g[x][y] == 'I') {
                   for (int dx = -1; dx < 2; ++dx) {
                       for (int dy = -1; dy < 2; ++dy) {
                           if (dx != 0 || dy != 0) {
                               String word = "";
                               for (int k = 0; k < 3; ++k) {
                                   int xx = x + k * dx;
                                   int yy = y + k * dy;
                                   if (0 <= xx && xx < d && 0 <= yy && yy < d) {
                                       word += g[xx][yy];
                                   } else {
                                       break;
                                   }
                               }
                               if (word.equals("I/O")) {
                                   res = res + 1;
                               }
                           }
                       }
                   }
                }
            }
        }
        return res;
    }

    private static final int MAX = 287;
}
