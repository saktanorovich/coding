package kickstart2018.rounda;

import utils.io.*;
import java.io.*;
import java.util.*;

// Problem A
public class EvenDigits {
    public void process(int testCase, InputReader in, PrintWriter out) {
        long n = in.nextLong();
        long min = min(n);
        long max = max(n);
        out.format("Case #%d: %d\n", testCase, Math.min(n - min, max - n));
    }

    private long min(long x) {
        List<Integer> ds = digits(x);
        List<Integer> rs = new ArrayList<>();
        int i = ds.size();
        for (--i; i >= 0; --i) {
            int d = ds.get(i);
            if (d % 2 == 1) {
                rs.add(d - 1);
                break;
            } else {
                rs.add(d);
            }
        }
        for (--i; i >= 0; --i) {
            rs.add(8);
        }
        return number(rs);
    }

    private long max(long x) {
        List<Integer> ds = digits(x);
        List<Integer> rs = new ArrayList<>();
        int i = ds.size();
        for (--i; i >= 0; --i) {
            int d = ds.get(i);
            if (d % 2 == 1) {
                if (d == 9) {
                    return max(x + POW10[i]);
                }
                rs.add(d + 1);
                break;
            } else {
                rs.add(d);
            }
        }
        for (--i; i >= 0; --i) {
            rs.add(0);
        }
        return number(rs);
    }

    private long number(List<Integer> d) {
        long res = 0;
        for (int x : d) {
            res *= 10;
            res += x;
        }
        return res;
    }

    private List<Integer> digits(long x) {
        List<Integer> res = new ArrayList<>();
        while (x > 0) {
            res.add((int)(x % 10));
            x /= 10;
        }
        return res;
    }

    private static final long[] POW10;
    static {
        POW10 = new long[18];
        POW10[0] = 1;
        for (int i = 1; i < POW10.length; ++i) {
            POW10[i] = 10 * POW10[i - 1];
        }
    };
}
