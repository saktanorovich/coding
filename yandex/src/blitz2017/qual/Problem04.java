package blitz2017.qual;

import utils.io.*;
import java.io.*;
import java.util.*;

// Banks and money
public class Problem04 {
    public boolean process(int testCase, InputReader in, PrintWriter out) {
        this.n = in.nextInt();
        this.a = new int[n];
        for (int i = 0; i < n; ++i) {
            a[i] = in.nextInt();
        }
        this.s = in.nextInt();
        // Note that we can represent banks as a rooted tree where the root can be
        // considered as a main bank with given amount of money and internal nodes
        // as offices which store information about their children. Also note that
        // the value in the root has to be equal to the sum in direct children and
        // any sequence of coins a1>=a2>=..>=am can be stored in one bank.
        Arrays.sort(a);
        this.memo = new Boolean[s + 1][n];
        for (int i = 0; i < n; ++i) {
            if (a[i] <= s) {
                memo[a[i]][i] = true;
            }
        }
        if (doit(s, n - 1)) {
            out.println("Yes");
        } else {
            out.println("No");
        }
        return true;
    }

    private boolean doit(int money, int last) {
        if (money >= a[last]) {
            if (memo[money][last] == null) {
                memo[money][last] = false;
                for (int next = last - 1; next >= 0; --next) {
                    memo[money][last] |= doit(money - a[last], next);
                }
            }
            return memo[money][last];
        }
        return false;
    }

    private Boolean[][] memo;
    private int a[];
    private int n;
    private int s;
}
