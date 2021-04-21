import java.io.*;
import java.util.*;

public class Main {
    private static final class MissionImprobable {
        public boolean process(int testCase, InputReader in, PrintWriter out) {
            int n = in.nextInt();
            int m = in.nextInt();
            int a[][] = new int[n][m];
            for (int i = 0; i < n; ++i) {
                for (int j = 0; j < m; ++j) {
                    a[i][j] = in.nextInt();
                }
            }
            out.println(doit(a, n, m));
            return true;
        }

        private long doit(int[][] a, int n, int m) {
            HashSet<Integer> H = new HashSet<>();
            int rmax[] = new int[n];
            int cmax[] = new int[m];
            long res = 0;
            for (int i = 0; i < n; ++i) {
                for (int j = 0; j < m; ++j) {
                    rmax[i] = Math.max(rmax[i], a[i][j]);
                    cmax[j] = Math.max(cmax[j], a[i][j]);
                    // if current stack contains more than one container
                    // let's keep only the bottom one and steal the others
                    // because the top view contains mark at this position
                    if (a[i][j] > 1) {
                        res += a[i][j] - 1;
                    }
                }
            }
            for (int h : rmax) H.add(h);
            for (int h : cmax) H.add(h);
            for (int h : H) {
                if (h > 1) {
                    // find the best matching between rows and columns
                    // which contain stacks of height h
                    res -= 1L * (h - 1) * match(a, n, m, rmax, cmax, h);
                }
            }
            return res;
        }

        private int match(int[][] a, int n, int m, int[] rmax, int[] cmax, int h) {
            List<Integer>[] g = new ArrayList[n];
            for (int i = 0; i < n; ++i) {
                g[i] = new ArrayList<>();
                if (rmax[i] == h) {
                    for (int j = 0; j < m; ++j) {
                        if (cmax[j] == h) {
                            // we can put the stack of height h only at
                            // position which is marked by the top view
                            if (a[i][j] > 0) {
                                g[i].add(j);
                            }
                        }
                    }
                }
            }
            int rc = 0;
            int cc = 0;
            for (int i = 0; i < n; ++i) {
                if (rmax[i] == h) {
                    rc++;
                }
            }
            for (int j = 0; j < m; ++j) {
                if (cmax[j] == h) {
                    cc++;
                }
            }
            return rc + cc - kuhn(g, n, m);
        }

        private int kuhn(List<Integer>[] g, int n1, int n2) {
            int m1[] = new int[n1];
            int m2[] = new int[n2];
            Arrays.fill(m1, -1);
            Arrays.fill(m2, -1);
            int cardinality = 0;
            while (augment(g, m1, m2)) {
                cardinality = cardinality + 1;
            }
            return cardinality;
        }

        private boolean augment(List<Integer>[] g, int[] m1, int[] m2) {
            boolean[] was = new boolean[m1.length];
            for (int u1 = 0; u1 < m1.length; ++u1) {
                if (m1[u1] == -1) {
                    if (dfs(g, u1, m1, m2, was)) {
                        return true;
                    }
                }
            }
            return false;
        }

        private boolean dfs(List<Integer>[] g, int u1, int[] m1, int[] m2, boolean[] was) {
            if (was[u1]) {
                return false;
            }
            was[u1] = true;
            for (int u2 : g[u1]) {
                if (m2[u2] == -1 || dfs(g, m2[u2], m1, m2, was)) {
                    m1[u1] = u2;
                    m2[u2] = u1;
                    return true;
                }
            }
            return false;
        }
    }

    public static void main(String[] args) throws Exception {
        InputReader in = new InputReader(new FileInputStream("input.txt"));
        PrintWriter out = new PrintWriter(new FileOutputStream("output.txt"));
        //InputReader in = new InputReader(System.in);
        //PrintWriter out = new PrintWriter(System.out);

        if (args.length > 0 && args[0].equals("-g")) {
        } else {
            System.err.println("Test Case: Elapsed time");
            boolean contd = true;
            for (int test = 1; in.hasNext() && contd; ++test) {
                long beg = System.nanoTime();
                contd = new MissionImprobable().process(test, in, out);
                out.flush();
                long end = System.nanoTime();
                System.err.format("Test #%3d: %9.3f ms\n", test, (end - beg) / 1e6);
            }
        }

        in.close();
        out.close();
    }

    private static final class InputReader {
        private BufferedReader reader;
        private StringTokenizer tokenizer;

        public InputReader(InputStream input) {
            reader = new BufferedReader(new InputStreamReader(input), 32768);
            tokenizer = null;
        }

        public boolean hasNext() {
            while (tokenizer == null || tokenizer.hasMoreTokens() == false) {
                try {
                    String nextLine = reader.readLine();
                    if (nextLine != null) {
                        tokenizer = new StringTokenizer(nextLine);
                    } else {
                        return false;
                    }
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
            return tokenizer.hasMoreTokens();
        }

        public String next() {
            if (hasNext()) {
                return tokenizer.nextToken();
            }
            return null;
        }

        public int nextInt() {
            return Integer.parseInt(next());
        }

        public long nextLong() {
            return Long.parseLong(next());
        }

        public double nextDouble() {
            return Double.parseDouble(next());
        }

        public String nextLine() {
            try {
                return reader.readLine();
            } catch (IOException e) {
                e.printStackTrace();
            }
            return null;
        }

        public void close() throws IOException {
            reader.close();
            reader = null;
        }
    }
}
