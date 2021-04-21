import java.io.*;
import java.util.*;

public class Main {
    private static final class Pilgrimage {
        public boolean process(int testCase, InputReader in, PrintWriter out) {
            int N = in.nextInt();
            if (N == 0) {
                return false;
            }
            Entry[] booklet = new Entry[N];
            for (int i = 0; i < N; ++i) {
                String cmd = in.next();
                if (cmd.charAt(0) == 'I') {
                    booklet[i] = new Entry(0, in.nextInt());
                } else if (cmd.charAt(0) == 'O') {
                    booklet[i] = new Entry(1, in.nextInt());
                } else if (cmd.charAt(0) == 'P') {
                    booklet[i] = new Entry(2, in.nextInt());
                } else if (cmd.charAt(0) == 'C') {
                    booklet[i] = new Entry(3, in.nextInt());
                }
            }
            List<Integer> res = new ArrayList<>();
            int S = eval(booklet);
            for (int init = S; init <= MAX_S; ++init) {
                if (okay(booklet, init)) {
                    res.add(init);
                }
            }
            int M = res.size();
            if (M == 0) {
                out.println("IMPOSSIBLE");
            } else if (M == MAX_S - S + 1) {
                out.format("SIZE >= %d", res.get(0));
                out.println();
            } else {
                for (int i = 0; i < M; ++i) {
                    out.print(res.get(i));
                    if (i + 1 < M) {
                        out.print(" ");
                    }
                }
                out.println();
            }
            return true;
        }

        private boolean okay(Entry[] booklet, int init) {
            int size = init;
            int paid = 0;
            int chng = 0;
            for (Entry e : booklet) {
                if (e.cmd == 0) {
                    if (chng > 0 && paid % size > 0) {
                        return false;
                    }
                    chng = 1; paid = 0; size += e.num;
                } else if (e.cmd == 1) {
                    if (chng > 0 && paid % size > 0) {
                        return false;
                    }
                    chng = 1; paid = 0; size -= e.num;
                    if (size <= 0) {
                        return false;
                    }
                } else if (e.cmd == 2) {
                    paid += e.num;
                }
            }
            return true;
        }

        private int eval(Entry[] booklet) {
            int size = 1;
            int have = 0;
            for (Entry e : booklet) {
                if (e.cmd == 0) {
                    have += e.num;
                    continue;
                }
                if (e.cmd == 1) {
                    have -= e.num;
                    if (have < 0) {
                        size = Math.max(size, -have + 1);
                    }
                }
            }
            return size;
        }

        private static final int MAX_S = 2000 * 50;

        private static final class Entry {
            public final int cmd;
            public final int num;

            public Entry(int cmd, int num) {
                this.cmd = cmd;
                this.num = num;
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
            for (int test = 1; contd; ++test) {
                long beg = System.nanoTime();
                contd = new Pilgrimage().process(test, in, out);
                out.flush();
                long end = System.nanoTime();
                if (contd) {
                    System.err.format("Test #%3d: %9.3f ms\n", test, (end - beg) / 1e6);
                }
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
