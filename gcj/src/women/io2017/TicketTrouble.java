package women.io2017;

import java.io.*;
import utils.io.*;

// Problem A
public class TicketTrouble {
    public void process(int testCase, InputReader in, PrintWriter out) {
        int f = in.nextInt();
        int s = in.nextInt();
        boolean c[][] = new boolean[s][s];
        for (int i = 0; i < f; ++i) {
            int a = in.nextInt();
            int b = in.nextInt();
            c[a - 1][b - 1] = true;
            c[b - 1][a - 1] = true;
        }
        int res = 0;
        for (int i = 0; i < s; ++i) {
            int cnt = 0;
            for (int j = 0; j < s; ++j) {
                cnt += c[i][j] ? 1 : 0;
            }
            res = Math.max(res, cnt);
        }
        out.format("Case #%d: %d\n", testCase, res);
    }
}
