package gcj2008.rd1c;

import utils.io.*;
import java.io.*;
import java.util.*;

// Problem A
public class TextMessagingOutrage {
    public void process(int testCase, InputReader in, PrintWriter out) {
        Integer P = in.nextInt();
        Integer K = in.nextInt();
        Integer L = in.nextInt();
        Integer F[] = new Integer[L];
        for (int i = 0; i < L; ++i) {
            F[i] = in.nextInt();
        }
        Arrays.sort(F, Collections.reverseOrder());
        long res = 0;
        for (int i = 0; i < L; ++i) {
            res += (i / K + 1) * F[i];
        }
        out.format("Case #%d: %d\n", testCase, res);
    }
}
