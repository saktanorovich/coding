package blitz2017.qual;

import utils.io.*;
import java.io.*;
import java.util.*;

// Max damage
public class Problem08 {
    public boolean process(int testCase, InputReader in, PrintWriter out) {
        int n = in.nextInt();
        int a = in.nextInt();
        if (n < 5) {
            out.println(n);
            return true;
        }
        List<Integer> d;
        switch (a) {
            case 2:
                d = get3(n);
                if (n % 3 == 1) {
                    d.remove(d.size() - 1);
                    d.remove(d.size() - 1);
                    d.add(4);
                }
                if (n % 3 == 2) {
                    d.remove(d.size() - 1);
                    d.remove(d.size() - 1);
                    d.remove(d.size() - 1);
                    d.add(4);
                    d.add(4);
                }
                break;
            case 3:
                d = get2(n);
                if (n % 2 == 1) {
                    d.remove(d.size() - 1);
                    d.remove(d.size() - 1);
                    d.remove(d.size() - 1);
                    d.add(5);
                }
                break;
            default:
                d = get3(n);
        }
        long res = 1;
        for (int x : d) {
            res *= x;
            res %= MOD;
        }
        out.println(res);
        return true;
    }

    private List<Integer> get3(int n) {
        List<Integer> res = new ArrayList<>();
        if (n < 5) {
            res.add(n);
        } else {
            for (int i = 0; i + 1 < n / 3; ++i) {
                res.add(3);
            }
            if (n % 3 == 0) {
                res.add(3);
            } else if (n % 3 == 1) {
                res.add(2);
                res.add(2);
            } else if (n % 3 == 2) {
                res.add(3);
                res.add(2);
            }
        }
        return res;
    }

    private List<Integer> get2(int n) {
        List<Integer> res = new ArrayList<>();
        if (n < 3) {
            res.add(n);
        } else {
            for (int i = 0; i < n / 2; ++i) {
                res.add(2);
            }
            if (n % 2 == 1) {
                res.add(1);
            }
        }
        return res;
    }

    private static final long MOD = (long) 1e9 + 7;
}
