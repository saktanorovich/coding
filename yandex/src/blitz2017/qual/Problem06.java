package blitz2017.qual;

import utils.io.*;
import java.io.*;
import java.util.*;

// Interesting trip
public class Problem06 {
    public boolean process(int testCase, InputReader in, PrintWriter out) {
        int n = in.nextInt();
        int x[] = new int[n];
        int y[] = new int[n];
        for (int i = 0; i < n; ++i) {
            x[i] = in.nextInt();
            y[i] = in.nextInt();
        }
        int K = in.nextInt();
        int A = in.nextInt() - 1;
        int B = in.nextInt() - 1;
        boolean g[][] = new boolean[n][n];
        for (int i = 0; i < n; ++i) {
            for (int j = i + 1; j < n; ++j) {
                if (1L * Math.abs(x[j] - x[i]) + 1L * Math.abs(y[j] - y[i]) <= K) {
                    g[i][j] = true;
                    g[j][i] = true;
                }
            }
        }
        Queue<Integer> q = new LinkedList<>();
        int[] dist = new int[n];
        Arrays.fill(dist, Integer.MAX_VALUE);
        dist[A] = 0;
        for (q.add(A); q.size() > 0; ) {
            int curr = q.poll();
            for (int next = 0; next < n; ++next) {
                if (g[curr][next]) {
                    if (dist[next] > dist[curr] + 1) {
                        dist[next] = dist[curr] + 1;
                        q.add(next);
                    }
                }
            }
        }
        out.println(dist[B] < Integer.MAX_VALUE ? dist[B] : -1);
        return true;
    }
}
