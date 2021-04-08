package kickstart2018.rounda;

import utils.io.*;
import java.io.*;
import java.util.*;

// Problem C
public class ScrambledWords {
    public void process(int testCase, InputReader in, PrintWriter out) {
        this.L = in.nextInt();
        this.W = new int[L][];
        for (int i = 0; i < L; ++i) {
            String w = in.next();
            W[i] = new int[w.length()];
            for (int j = 0; j < w.length(); ++j) {
                W[i][j] = w.charAt(j) - 'a';
            }
        }
        char S1 = in.next().charAt(0);
        char S2 = in.next().charAt(0);
        this.N = in.nextInt();
        this.A = in.nextInt();
        this.B = in.nextInt();
        this.C = in.nextInt();
        this.D = in.nextInt();
        this.T = new int[N + 1];
        T[1] = S1 - 'a';
        T[2] = S2 - 'a';
        long x[] = new long[N + 1];
        x[1] = (int)S1;
        x[2] = (int)S2;
        for (int i = 3; i <= N; ++i) {
            x[i] = (A * x[i - 1] + B * x[i - 2] + C) % D;
            T[i] = (int)(x[i] % 26);
        }
        int res;
        if (1L * L * N <= 1e6) {
            res = new Small().process();
        } else {
            res = new Large().process();
        }
        out.format("Case #%d: %d\n", testCase, res);
    }

    private static boolean same(int[] a, int[] b) {
        for (int i = 0; i < 26; ++i) {
            if (a[i] != b[i]) {
                return false;
            }
        }
        return true;
    }

    private static int[] make(int[] a, int k) {
        int res[] = new int[26];
        if (k <= a.length) {
            for (int i = 0; i < k; ++i) {
                ++res[a[i]];
            }
        }
        return res;
    }

    private class Small {
        public int process() {
            int res = 0;
            for (int[] w : W) {
                int len = w.length;
                int[] mask = make(w, len);
                int[] have = make(T, len);
                for (int i = w.length; i <= N; ++i) {
                    --have[T[i - w.length]];
                    ++have[T[i]];
                    int a = T[i - len + 1];
                    int b = T[i];
                    if (a == w[0] && b == w[len - 1]) {
                        if (same(mask, have)) {
                            res = res + 1;
                            break;
                        }
                    }
                }
            }
            return res;
        }
    }

    private class Large {
        private HashMap<Integer, HashMap<Freq, Integer>[][]> index;

        public Large() {
            this.index = new HashMap<>();
        }

        public int process() {
            for (int i = 0; i < L; ++i) {
                int len = W[i].length;
                if (index.containsKey(len) == false) {
                    index.put(len, new HashMap[26][26]);
                }
                int a = W[i][0];
                int b = W[i][len - 1];
                HashMap<Freq, Integer>[][] entry = index.get(len);
                if (entry[a][b] == null) {
                    entry[a][b] = new HashMap<>();
                }
                Freq freq = new Freq(make(W[i], len));
                if (entry[a][b].containsKey(freq) == false) {
                    entry[a][b].put(freq, 0);
                }
                entry[a][b].put(freq, entry[a][b].get(freq) + 1);
            }
            int res = 0;
            for (int len : index.keySet()) {
                HashMap<Freq, Integer>[][] entry = index.get(len);
                int[] have = make(T, len);
                for (int i = len; i <= N; ++i) {
                    --have[T[i - len]];
                    ++have[T[i]];
                    int a = T[i - len + 1];
                    int b = T[i];
                    if (entry[a][b] != null) {
                        Freq freq = new Freq(have);
                        if (entry[a][b].containsKey(freq)) {
                            res += entry[a][b].remove(freq);
                        }
                    }
                }
            }
            return res;
        }

        private class Freq {
            private int value[];
            private int hash;

            public Freq(int[] value) {
                this.value = value;
                this.hash = Arrays.hashCode(value);
            }

            @Override
            public boolean equals(Object obj) {
                Freq f = (Freq)obj;
                if (hash == f.hash) {
                    return Arrays.equals(value, f.value);
                }
                return false;
            }

            @Override
            public int hashCode() {
                return hash;
            }
        }
    }

    private long A;
    private long B;
    private long C;
    private long D;
    private int W[][];
    private int T[];
    private int L;
    private int N;
}
