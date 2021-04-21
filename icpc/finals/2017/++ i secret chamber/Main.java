import java.io.*;
import java.util.*;

public class Main {
    private static final class SecretChamber {
        public void process(int testCase, InputReader in, PrintWriter out) {
            int m = in.nextInt();
            int n = in.nextInt();
            boolean g[][] = new boolean[26][26];
            for (int i = 0; i < m; ++i) {
                int a = in.next().charAt(0) - 'a';
                int b = in.next().charAt(0) - 'a';
                g[a][b] = true;
            }
            for (int k = 0; k < 26; ++k) {
                g[k][k] = true;
            }
            for (int k = 0; k < 26; ++k) {
                for (int i = 0; i < 26; ++i) {
                    for (int j = 0; j < 26; ++j) {
                        g[i][j] = g[i][j] | (g[i][k] & g[k][j]);
                    }
                }
            }
            for (int i = 0; i < n; ++i) {
                String a = in.next();
                String b = in.next();
                if (match(a, b, g)) {
                    out.println("yes");
                } else {
                    out.println("no");
                }
            }
        }

        private static boolean match(String a, String b, boolean[][] g) {
            if (a.length() == b.length()) {
                for (int i = 0; i < a.length(); ++i) {
                    int x = a.charAt(i) - 'a';
                    int y = b.charAt(i) - 'a';
                    if (g[x][y] == false) {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
    }

    public static void main(String[] args) throws Exception {
        InputReader in = new InputReader(new FileInputStream("input.txt"));
        PrintWriter out = new PrintWriter(new FileOutputStream("output.txt"));
        //InputReader in = new InputReader(System.in);
        //PrintWriter out = new PrintWriter(System.out);

        System.err.println("Test Case: Elapsed time");
        for (int test = 1; in.hasNext(); ++test) {
            long beg = System.nanoTime();
            new SecretChamber().process(test, in, out);
            out.flush();
            long end = System.nanoTime();
            System.err.format("Test #%3d: %9.3f ms\n", test, (end - beg) / 1e6);
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

        public void close() throws IOException {
            reader.close();
            reader = null;
        }
    }
}
