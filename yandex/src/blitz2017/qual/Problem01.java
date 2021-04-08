package blitz2017.qual;

import java.io.*;
import utils.io.*;

// Game
public class Problem01 {
    public boolean process(int testCase, InputReader in, PrintWriter out) {
        int n = in.nextInt();
        int a[] = new int[n];
        for (int i = 0; i < n; i += 1) {
            a[i] = in.nextInt();
        }
        int p = 0;
        int v = 0;
        for (int i = 0; i < n; i += 3) {
            p += a[i];
            v += a[i + 1];
            if (a[i] < a[i + 1]) {
                p += a[i + 2];
            } else {
                v += a[i + 2];
            }
        }
        if (p > v) {
            out.println("Petya");
        } else {
            out.println("Vasya");
        }
        return true;
    }
}
