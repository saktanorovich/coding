import java.io.*;
import java.util.*;

public class Main {
    private static final class Workshops {
        public boolean process(int testCase, InputReader in, PrintWriter out) {
            int W = in.nextInt();
            if (W == 0) {
                return false;
            }
            Work[] w;
            Room[] r;
            w = new Work[W];
            for (int i = 0; i < W; ++i) {
                int p = in.nextInt();
                int d = in.nextInt();
                w[i] = new Work(p, d);
            }
            int R = in.nextInt();
            r = new Room[R];
            for (int i = 0; i < R; ++i) {
                int s = in.nextInt();
                int h = parse(in.next());
                r[i] = new Room(s, h);
            }
            Arrays.sort(w, (a, b) -> {
                if (a.p != b.p) {
                    return b.p - a.p;
                }
                return b.d - a.d;
            });
            int tentW = W;
            int tentP = 0;
            for (Work x : w) {
                tentP += x.p;
            }
            boolean[] u = new boolean[R];
            for (int i = 0; i < W; ++i) {
                int k = -1;
                for (int j = 0; j < R; ++j) {
                    if (u[j] == false) {
                        // if we have enough seats
                        if (w[i].p <= r[j].s) {
                            // if we have enough time
                            if (w[i].d <= r[j].h) {
                                if (k == -1 || r[k].h > r[j].h) {
                                    k = j;
                                }
                            }
                        }
                    }
                }
                if (k != -1) {
                    u[k] = true;
                    tentW -= 1;
                    tentP -= w[i].p;
                }
            }
            out.format("Trial %d: %d %d\n\n", testCase, tentW, tentP);
            return true;
        }

        private int parse(String t) {
            int at = t.indexOf(':');
            int hh = Integer.parseInt(t.substring(0, at));
            int mm = Integer.parseInt(t.substring(at + 1));
            return (hh - 14) * 60 + mm;
        }

        private final class Work {
            public final int p;
            public final int d;

            public Work(int p, int d) {
                this.p = p;
                this.d = d;
            }
        }

        private final class Room {
            public final int s;
            public final int h;

            public Room(int s, int h) {
                this.s = s;
                this.h = h;
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
            for (int test = 1; in.hasNext() && contd; ++test) {
                long beg = System.nanoTime();
                contd = new Workshops().process(test, in, out);
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
