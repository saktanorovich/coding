package kickstart2017.roundc;

import utils.io.*;
import java.io.*;

// Problem A
public class AmbiguousCipher {
    public void process(int testCase, InputReader in, PrintWriter out) {
        out.format("Case #%d: %s\n", testCase, get(in.next()));
    }

    private static String get(String s) {
        int r[] = get(toInteger(s));
        if (r == null) {
            return "AMBIGUOUS";
        }
        return toString(r);
    }

    private static int[] get(int[] c) {
        int n = c.length;
        if (n > 2) {
            int[] res = null;
            for (int x = 0; x < 26; ++x) {
                int now[] = new int[n];
                now[0] = x;
                now[1] = c[0];
                for (int i = 1; i + 1 < n; ++i) {
                    now[i + 1] += c[i] - now[i - 1];
                    now[i + 1] += 26;
                    now[i + 1] %= 26;
                }
                if (now[n - 2] == c[n - 1]) {
                    if (res == null) {
                        res = now;
                    } else {
                        return null;
                    }
                }
            }
            return res;
        }
        return new int[] { c[1], c[0] };
    }

    private static int[] toInteger(String s) {
        int[] c = new int[s.length()];
        for (int i = 0; i < s.length(); ++i) {
            c[i] = s.charAt(i) - 'A';
        }
        return c;
    }

    private static String toString(int[] c) {
        String s = "";
        for (int x : c) {
            s += (char)(x + 'A');
        }
        return s;
    }
}
