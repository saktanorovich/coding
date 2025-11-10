import java.io.*;
import java.util.*;

public class Main {
    private static final class Firetruck {
        public boolean process(int testCase, InputReader in, PrintWriter out) {
            this.layout = new int[7][];
            this.labels = new int[7][];
            for (int i = 0; i < 7; ++i) {
                layout[i] = new int[8];
                labels[i] = new int[8];
                for (int j = 0; j < 8; ++j) {
                    layout[i][j] = in.nextInt();
                }
            }
            this.marked = new int[29];
            if (testCase > 1) {
                out.format("\n\n\n");
            }
            out.format("Layout #%d:\n\n", testCase);
            dump(layout, out);
            out.format("\nMaps resulting from layout #%d are:\n\n", testCase);
            backtrack(0, 0, out);
            out.format("\nThere are %d solution(s) for layout #%d.\n", count, testCase);
            return true;
        }

        private void backtrack(int r, int c, PrintWriter out) {
            for (;; ++c) {
                if (c > 7) {
                    r = r + 1;
                    c = 0;
                }
                if (r > 6) {
                    count = count + 1;
                    dump(labels, out);
                    return;
                }
                if (labels[r][c] == 0) {
                    break;
                }
            }
            if (c < 7 && labels[r][c + 1] == 0) {
                int a = layout[r][c], b = layout[r][c + 1];
                int t = number[a][b];
                if (marked[t] == 0) {
                    marked[t] = 1;
                    labels[r][c] = t;
                    labels[r][c + 1] = t;
                    backtrack(r, c + 2, out);
                    labels[r][c + 1] = 0;
                    labels[r][c] = 0;
                    marked[t] = 0;
                }
            }
            if (r < 6 && labels[r + 1][c] == 0) {
                int a = layout[r][c], b = layout[r + 1][c];
                int t = number[a][b];
                if (marked[t] == 0) {
                    marked[t] = 1;
                    labels[r][c] = t;
                    labels[r + 1][c] = t;
                    backtrack(r, c + 1, out);
                    labels[r + 1][c] = 0;
                    labels[r][c] = 0;
                    marked[t] = 0;
                }
            }
        }

        private void dump(int[][] a, PrintWriter out) {
            for (int i = 0; i < 7; ++i) {
                for (int j = 0; j < 8; ++j) {
                    out.format("%4d", a[i][j]);
                }
                out.format("\n");
            }
            out.format("\n");
        }

        private int layout[][];
        private int labels[][];
        private int marked[];
        private int count;

        private static int[][] number = new int[7][7];
        static {
            for (int i = 0, k = 1; i < 7; ++i) {
                for  (int j = i; j < 7; ++j, ++k) {
                    number[i][j] = k;
                    number[j][i] = k;
                }
            }
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
            for (int test = 1; in.hasNext(); ++test) {
                long beg = System.nanoTime();
                contd = new Firetruck().process(test, in, out);
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
