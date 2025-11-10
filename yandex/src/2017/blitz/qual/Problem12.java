package blitz2017.qual;

import utils.io.*;
import java.io.*;
import java.util.*;

// Play with numbers
public class Problem12 {
    public boolean process(int testCase, InputReader in, PrintWriter out) {
        int n = in.nextInt();
        int a[] = new int[n];
        for (int i = 0; i < n; ++i) {
            a[i] = in.nextInt();
        }
        long res = 0;
        while (true) {
            Arrays.sort(a);
            if (a[0] == a[n - 1]) {
                break;
            }
            for (int i = 1; i < n; ++i) {
                if (a[i] > a[0]) {
                    if (a[i] % a[0] != 0) {
                        res += a[i] / a[0];
                        a[i] = a[i] % a[0];
                    } else {
                        res += a[i] / a[0] - 1;
                        a[i] = a[0];
                    }
                }
            }
        }
        out.println(res);
        return true;
    }
}
