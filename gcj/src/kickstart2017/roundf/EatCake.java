package kickstart2017.roundf;

import utils.io.*;
import java.io.*;

// Problem D
public class EatCake {
    public void process(int testCase, InputReader in, PrintWriter out) {
        int n = in.nextInt();
        int f[] = new int[n + 1];
        for (int i = 1; i <= n; ++i) {
            f[i] = i;
            for (int j = 1; j * j <= i; ++j) {
                f[i] = Math.min(f[i], f[i - j * j] + 1);
            }
        }
        out.format("Case #%d: %d\n", testCase, f[n]);
    }
}
