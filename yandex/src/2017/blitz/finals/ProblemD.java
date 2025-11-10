package blitz2017.finals;

import java.io.*;
import java.util.*;
import utils.io.*;

public class ProblemD {
    public boolean process(int testCase, InputReader in, PrintWriter out) {
        this.n = in.nextInt();
        this.m = in.nextInt();
        this.o = in.next();
        this.a = new String[m];
        for (int i = 0; i < m; ++i) {
            a[i] = in.next();
        }
        int k = in.nextInt();
        for (int i = 0; i < k; ++i) {
            if (doit(in.next())) {
                out.println("YES");
            } else {
                out.println("NO");
            }
        }
        return true;
    }

    private boolean doit(String s) {
        byte sets[][] = new byte[m][];
        for (int i = 0; i < m; ++i) {
            sets[i] = make(a[i], s);
        }
        return new Solver(sets, make(o, s)).doit();
    }

    private byte[] make(String a, String b) {
        byte[] res = new byte[n];
        for (int i = 0; i < n; ++i) {
            res[i] = 2;
            if (a.charAt(i) != '-') {
                if (a.charAt(i) == b.charAt(i)) {
                    res[i] = 1;
                } else {
                    res[i] = 0;
                }
            }
        }
        return res;
    }

    private String a[];
    private String o;
    private int n;
    private int m;

    private class Solver {
        private List<Integer> what[];
        private byte sets[][];
        private byte bulb[];
        private int wait[];
        private int have[];

        public Solver(byte sets[][], byte[] bulb) {
            this.sets = sets;
            this.bulb = bulb;
        }

        public boolean doit() {
            have = new int[sets.length];
            wait = new int[sets.length];
            what = new List[bulb.length];
            for (int i = 0; i < bulb.length; ++i) {
                what[i] = new ArrayList<>();
            }
            int head = 0;
            int tail = 0;
            for (int x = 0; x < sets.length; ++x) {
                for (int i = 0; i < bulb.length; ++i) {
                    if (sets[x][i] == 0) {
                        wait[x]++;
                        what[i].add(x);
                    }
                }
                if (wait[x] == 0) {
                    have[tail++] = x;
                }
            }
            byte[] mask = new byte[bulb.length];
            while (head < tail) {
                byte[] last = sets[have[head++]];
                for (int i = 0; i < mask.length; ++i) {
                    if (mask[i] == 1 || last[i] == 2) {
                        continue;
                    }
                    if (last[i] == 1) {
                        mask[i] = 1;
                        for (int x : what[i]) {
                            wait[x]--;
                            if (wait[x] == 0) {
                                have[tail++] = x;
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < bulb.length; ++i) {
                int val = mask[i] |= bulb[i];
                if (val != 1) {
                    return false;
                }
            }
            return true;
        }
    }

    public void generate(PrintWriter out, int testCases) {
        final int MAX_N = 500;
        final int MAX_M = 500;
        final int MAX_K = 100;
        Random rand = new Random(50847534);
        for (int testCase = 1; testCase < 1; ++testCase) {
            int n = rand.nextInt(MAX_N) + 1;
            int m = rand.nextInt(MAX_M) + 1;
            out.println(n + " " + m);
            for (int i = 0; i < n; ++i) {
                out.print((char) ('a' + rand.nextInt(26)));
            }
            out.println();
            for (int j = 0; j < m; ++j) {
                for (int i = 0; i < n; ++i) {
                    int c = rand.nextInt(26);
                    if (rand.nextInt() % 2 == 0) {
                        out.print('-');
                    } else {
                        out.print((char) ('a' + c));
                    }
                }
                out.println();
            }
            int k = rand.nextInt(MAX_K) + 1;
            out.println(k);
            for (int j = 0; j < k; ++j) {
                for (int i = 0; i < n; ++i) {
                    out.print((char) ('a' + rand.nextInt(26)));
                }
                out.println();
            }
        }
        for (int testCase = 1; testCase < 1; ++testCase) {
            int n = MAX_N;
            int m = MAX_M;
            out.println(n + " " + m);
            char[] o = new char[n];
            for (int i = 0; i < n; ++i) {
                o[i] = (char) ('a' + rand.nextInt(26));
                out.print(o[i]);
            }
            out.println();
            char[][] a = new char[m][n];
            for (int j = 0; j < m; ++j) {
                for (int i = 0; i < n; ++i) {
                    int c = rand.nextInt(26);
                    if (rand.nextInt() % 2 == 0) {
                        a[j][i] = '-';
                    } else {
                        a[j][i] = (char) ('a' + c);
                    }
                    out.print(a[j][i]);
                }
                out.println();
            }
            int k = MAX_K;
            out.println(k);
            int p[] = new int[n];
            for (int i = 0; i < n; ++i) {
                p[i] = i;
            }
            for (int j = 0; j < k; ++j) {
                char[] state = o.clone();
                for (int x : take(rand, take(rand, p, rand.nextInt(n) + 1))) {
                    for (int i = 0; i < n; ++i) {
                        if (a[x][i] != '-') {
                            state[i] = a[x][i];
                        }
                    }
                }
                for (int i = 0; i < n; ++i) {
                    out.print(state[i]);
                }
                out.println();
            }
        }
        for (int testCase = 1; testCase <= testCases; ++testCase) {
            int n = MAX_N;
            int m = MAX_M;
            out.println(n + " " + m);
            for (int i = 0; i < n; ++i) {
                out.print('a');
            }
            out.println();
            char c = (char) ('a' + rand.nextInt(26));
            for (int j = 0; j < m; ++j) {
                for (int i = 0; i < n; ++i) {
                    if (i == j) {
                        out.print(c);
                    } else {
                        out.print('-');
                    }
                }
                out.println();
            }
            int k = MAX_K;
            out.println(k);
            for (int j = 0; j < k; ++j) {
                for (int i = 0; i < n; ++i) {
                    out.print(c);
                }
                out.println();
            }
        }
        out.flush();
    }

    private static int[] take(Random rand, int[] a) {
        return take(rand, a, a.length);
    }

    private static int[] take(Random rand, int[] a, int k) {
        if (k > 0) {
            int b[] = new int[k];
            for (int i = 0; i < k; ++i) {
                b[i] = a[i + rand.nextInt(a.length - i)];
            }
            return b;
        }
        return take(rand, a);
    }
}