package blitz2017.qual;

import utils.io.*;
import java.io.*;
import java.util.*;

// Split number
public class Problem07 {
    public boolean process(int testCase, InputReader in, PrintWriter out) {
        long n = in.nextLong();
        long d[] = digitize(n);
        long a[] = null;
        for (int set = 0; set < 1 << (d.length - 1); ++set) {
            long[] b = get(d, set);
            if (b != null) {
                if (a == null || a.length < b.length) {
                    a = b;
                }
            }
        }
        for (int i = 0; i < a.length; ++i) {
            out.print(a[i]);
            if (i + 1 < a.length) {
                out.print("-");
            }
        }
        out.println();
        return true;
    }

    private long[] get(long[] d, int set) {
        StringBuilder s = new StringBuilder();
        for (int i = 0; i < d.length; ++i) {
            s.append(d[i]);
            if ((set & (1 << i)) != 0) {
                s.append("-");
            }
        }
        return parse(s.toString().split("-"));
    }

    private long[] parse(String[] tokens) {
        long[] res = new long[tokens.length];
        for (int i = 0; i < tokens.length; ++i) {
            if (tokens[i].length() > 1 && tokens[i].charAt(0) == '0') {
                return null;
            }
            long x = Long.parseLong(tokens[i]);
            for (int j = 0; j < i; ++j) {
                if (res[j] == x) {
                    return null;
                }
            }
            res[i] = x;
        }
        return res;
    }

    private long[] digitize(long x) {
        if (x == 0) {
            return new long[]{0};
        }
        Stack<Long> s = new Stack<>();
        for (; x > 0; x /= 10) {
            s.push(x % 10);
        }
        long d[] = new long[s.size()];
        for (int i = 0; i < d.length; ++i) {
            d[i] = s.pop();
        }
        return d;
    }
}
