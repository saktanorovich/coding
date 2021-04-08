package kickstart2018.practice;

import utils.io.*;
import java.io.*;

// Problem D
public class SumsOfSums {
    public void process(int testCase, InputReader in, PrintWriter out) {
        this.n = in.nextInt();
        this.q = in.nextInt();
        this.a = new long[n + 1];
        this.s = new long[n + 1];
        this.p = new long[n + 1];
        for (int i = 1; i <= n; ++i) {
            a[i] = in.nextInt();
            s[i] = s[i - 1] + a[i];
            p[i] = p[i - 1] + s[i - 1];
        }
        out.format("Case #%d:\n", testCase);
        for (int i = 1; i <= q; ++i) {
            long L = in.nextLong();
            long R = in.nextLong();
            out.println(get(R) - get(L - 1));
        }
    }

    // sum of elements in a* from 1 to P inclusive
    private long get(long P) {
        if (P == 0) {
            return 0;
        }
        long S = sum(P);
        // If we assume that s[i] <= S for some i then
        // we need to add to the result the sum
        //   s[i]+(s[i]-s[1])+..+(s[i]-s[i-1])=i*s[i]-p[i]
        // where p[i]=s[1]+s[2]+..+s[i-1].
        // If s[i] > S for some i then we need to find the
        // smallest index k such that s[i] - s[k] <= S. In
        // this case we need to add to the result the sum
        //   (i-k)*s[i]-(p[i]-pk])
        // Also some sums can occur more than one time so we
        // need to handle this case separately.
        long res = 0;
        for (int i = 1; i <= n; ++i) {
            if (s[i] <= S) {
                res += i * s[i] - p[i];
            } else {
                int k = ind(S, i);
                res += i * s[i] - p[i];
                res -= k * s[i] - p[k];
            }
        }
        res -= S * (cnt(S) - P);
        return res;
    }

    // sum at position P in a*
    private long sum(long P) {
        long L = 1, H = (long)4e12;
        while (L < H) {
            long S = (L + H) / 2;
            if (cnt(S) < P) {
                L = S + 1;
            } else {
                H = S;
            }
        }
        return L;
    }

    // cnt of sums in a* which are less or equal to S
    private long cnt(long S) {
        long res = 0;
        for (int i = 1; i <= n; ++i) {
            if (s[i] <= S) {
                res += i;
            } else {
                res += i - ind(S, i);
            }
        }
        return res;
    }

    private int ind(long S, int i) {
        int l = 1, h = i;
        while (l < h) {
            int x = (l + h) / 2;
            if (s[i] - s[x] > S) {
                l = x + 1;
            } else {
                h = x;
            }
        }
        return l;
    }

    private long p[];
    private long s[];
    private long a[];
    private int n;
    private int q;
}
