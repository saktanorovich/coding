import java.io.*;
import java.util.*;

public class Main {
    private static final class DegreesOfSeparation {
        public boolean process(int testCase, InputReader in, PrintWriter out) {
            int P = in.nextInt();
            int R = in.nextInt();
            if (P == 0 && R == 0) {
                return false;
            }
            int G[][] = new int[P][P];
            for (int i = 0; i < P; ++i) {
                Arrays.fill(G[i], (int)(1e6));
                G[i][i] = 0;
            }
            Map<String, Integer> Z = new HashMap<>();
            for (int i = 0; i < R; ++i) {
                String a = in.next();
                String b = in.next();
                if (Z.containsKey(a) == false) {
                    Z.put(a, Z.size());
                }
                if (Z.containsKey(b) == false) {
                    Z.put(b, Z.size());
                }
                G[Z.get(a)][Z.get(b)] = 1;
                G[Z.get(b)][Z.get(a)] = 1;
            }
            for (int k = 0; k < P; ++k) {
                for (int i = 0; i < P; ++i) {
                    for (int j = 0; j < P; ++j) {
                        G[i][j] = Math.min(G[i][j], G[i][k] + G[k][j]);
                    }
                }
            }
            int max = 0;
            for (int i = 0; i < P; ++i) {
                for (int j = 0; j < P; ++j) {
                    max = Math.max(max, G[i][j]);
                }
            }
            if (max < (int)1e6) {
                out.format("Network %d: %d\n\n", testCase, max);
            } else {
                out.format("Network %d: DISCONNECTED\n\n", testCase);
            }
            return true;
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
            //int T = in.nextInt();
            for (int test = 1; in.hasNext() && contd; ++test) {
                long beg = System.nanoTime();
                contd = new DegreesOfSeparation().process(test, in, out);
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
