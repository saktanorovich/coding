package gcj2008.qual;

import utils.io.*;
import java.io.*;
import java.util.*;

// Problem B
public class TrainTimetable {
    public void process(int testCase, InputReader in, PrintWriter out) {
        int depo[][] = new int[2][TIME];
        int line[] = new int[2];
        int need[] = new int[2];
        List<Integer>[][] stat = new ArrayList[2][TIME];
        for (int d = 0; d < 2; ++d) {
            for (int t = 0; t < TIME; ++t) {
                stat[d][t] = new ArrayList<>();
            }
        }
        int T = in.nextInt();
        line[0] = in.nextInt();
        line[1] = in.nextInt();
        for (int d = 0; d < 2; ++d) {
            for (int i = 0; i < line[d]; ++i) {
                String p[] = in.nextLine().split("\\s|:");
                int h0 = Integer.parseInt(p[0]);
                int m0 = Integer.parseInt(p[1]);
                int h1 = Integer.parseInt(p[2]);
                int m1 = Integer.parseInt(p[3]);
                stat[d][h0 * 60 + m0].add(h1 * 60 + m1 + T);
            }
        }
        for (int t = 0; t + 1 < TIME; ++t) {
            for (int d = 0; d < 2; ++d) {
                List<Integer> q = stat[d][t];
                for (int i = 0; i < q.size(); ++i) {
                    if (depo[d][t] > 0) {
                        depo[d][t]--;
                    } else {
                        need[d]++;
                    }
                    int at = q.get(i);
                    if (at < TIME) {
                        depo[1 - d][at]++;
                    }
                }
                depo[d][t + 1] += depo[d][t];
            }
        }
        out.format("Case #%d: %d %d\n", testCase, need[0], need[1]);
    }

    private static final int TIME = 24 * 60;
}
