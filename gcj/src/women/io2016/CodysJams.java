package women.io2016;

import utils.io.*;
import java.io.*;
import java.util.*;

// Problem A
public class CodysJams {
    public void process(int testCase, InputReader in, PrintWriter out) {
        Integer n = in.nextInt();
        Long ps[] = new Long[2 * n];
        List<Long> set = new ArrayList<>();
        for (int i = 0; i < 2 * n; ++i) {
            ps[i] = in.nextLong();
            set.add(ps[i]);
        }
        out.format("Case #%d:", testCase);
        for (int i = 0, k = 0; k < n; ++i) {
            if (ps[i] % 3 == 0) {
                if (set.contains(ps[i]) && set.contains(ps[i] * 4 / 3)) {
                    out.format(" %d", ps[i]);
                    k = k + 1;
                    set.remove(ps[i]);
                    set.remove(ps[i] * 4 / 3);
                }
            }
        }
        out.println();
    }
}
