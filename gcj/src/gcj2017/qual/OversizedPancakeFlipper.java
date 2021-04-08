package gcj2017.qual;

import utils.io.*;
import java.io.*;

// Problem A
public class  OversizedPancakeFlipper {
    public void process(int testCase, InputReader in, PrintWriter out) {
        String s = in.next();
        int k = in.nextInt();
        int res = get(s.toCharArray(), k);
        if (res < Integer.MAX_VALUE) {
            out.format("Case #%d: %d\n", testCase, res);
        } else {
            out.format("Case #%d: IMPOSSIBLE\n", testCase);
        }
    }

    private int get(char[] s, int k) {
        int res = 0;
        for (int i = 0; i < s.length; ++i) {
            if (s[i] == '-') {
                res = res + 1;
                if (i + k <= s.length) {
                    for (int j = 0; j < k; ++j) {
                        s[i + j] = s[i + j] == '-' ? '+' : '-';
                    }
                } else {
                    return Integer.MAX_VALUE;
                }
            }
        }
        return res;
    }
}
