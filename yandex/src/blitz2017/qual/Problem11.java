package blitz2017.qual;

import utils.io.*;
import java.io.*;
import java.util.*;

// Nested rectangles
public class Problem11 {
    public boolean process(int testCase, InputReader in, PrintWriter out) {
        int n = in.nextInt();
        int x[] = new int[n];
        int y[] = new int[n];
        for (int i = 0; i < n; ++i) {
            x[i] = in.nextInt();
            y[i] = in.nextInt();
        }
        Arrays.sort(x);
        Arrays.sort(y);
        int minX = 0;
        int minY = 0;
        int maxX = n - 1;
        int maxY = n - 1;
        int res = 0;
        for (boolean contd = true; contd; ) {
            res = res + 1;
            contd = false;
            if (x[minX] != x[maxX]) {
                minX = move(x, minX, +1);
                maxX = move(x, maxX, -1);
                contd = true;
            }
            if (minX > maxX) {
                break;
            }
            if (y[minY] != y[maxY]) {
                minY = move(y, minY, +1);
                maxY = move(y, maxY, -1);
                contd = true;
            }
            if (minY > maxY) {
                break;
            }
        }
        out.println(res);
        return true;
    }

    private int move(int[] a, int it, int stp) {
        while (a[it + stp] == a[it]) {
            it += stp;
        }
        it += stp;
        return it;
    }
}
