package kickstart2017.practice;

import java.io.*;
import utils.io.*;

// Problem C
public class SherlockAndParentheses {
    public void process(int testCase, InputReader in, PrintWriter out) {
        int L = in.nextInt();
        int R = in.nextInt();
        long res = 0;
        if (L <= R) {
            for (int i = 0; i < L; ++i) {
                res += L - i;
            }
        } else {
            for (int i = 0; i < R; ++i) {
                res += R - i;
            }
        }
        out.format("Case #%d: %d\n", testCase, res);
    }
}
