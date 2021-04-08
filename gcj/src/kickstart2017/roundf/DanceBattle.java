package kickstart2017.roundf;

import utils.io.*;
import java.io.*;
import java.util.*;

// Problem B
public class DanceBattle {
    public void process(int testCase, InputReader in, PrintWriter out) {
        int E = in.nextInt();
        int n = in.nextInt();
        int S[] = new int[n];
        for (int i = 0; i < n; ++i) {
            S[i] = in.nextInt();
        }
        Arrays.sort(S);
        int H = 0;
        int R = 0;
        for (int i = 0, j = n - 1; i <= j;) {
            if (E > S[i]) {
                E = E - S[i];
                H = H + 1;
                i = i + 1;
            } else if (H > 0) {
                E = E + S[j];
                H = H - 1;
                j = j - 1;
            } else {
                break;
            }
            R = Math.max(R, H);
        }
        out.format("Case #%d: %d\n", testCase, R);
    }
}
