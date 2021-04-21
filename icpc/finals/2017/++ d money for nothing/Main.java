import java.io.*;
import java.util.*;

public class Main {
    private static final class MoneyForNothing {
        public void process(int testCase, InputReader in, PrintWriter out) {
            int m = in.nextInt();
            int n = in.nextInt();
            this.P = getP(in, m);
            this.C = getC(in, n);
            out.println(doit(0, P.length - 1, 0, C.length - 1));
        }

        // Assume we have producers p and q (p < q) and consumers
        // a and b (a < b). Assume that s(p,b) > s(p,a). It can be
        // shown that s(q,b) > s(q,a). That's mean that we can
        // apply divide & conquer technique to improve performance.
        private long doit(int a0, int a1, int b0, int b1) {
            if (a0 > a1) {
                return 0;
            }
            int a = a0 + (a1 - a0) / 2;
            int b = b0;
            while (P[a].date >= C[b].date) {
                if (b + 1 <= b1) {
                    b = b + 1;
                } else {
                    break;
                }
            }
            long best = 0;
            for (int x = b; C[x].rate > P[a].rate;) {
                long earn = earn(P[a], C[x]);
                if (best <= earn) {
                    best  = earn;
                    b = x;
                }
                if (x + 1 <= b1) {
                    x = x + 1;
                } else {
                    break;
                }
            }
            best = Math.max(best, doit(a0, a - 1, b0, b));
            best = Math.max(best, doit(a + 1, a1, b, b1));
            return best;
        }

        private long earn(Party p, Party c) {
            return 1L * (c.rate - p.rate) * (c.date - p.date);
        }

        // If we have two producers pi and pj with property
        //   pi.date <= pj.date
        //   pi.rate <= pj.rate
        // then the producer pi is prefarable.
        private Party[] getP(InputReader in, int m) {
            Party[] A = load(in, m);
            Stack<Party> s = new Stack<>();
            for (int i = m - 1; i >= 0; --i) {
                while (!s.empty()) {
                    if (s.peek().rate >= A[i].rate) {
                        s.pop();
                    } else {
                        break;
                    }
                }
                s.push(A[i]);
            }
            Collections.reverse(s);
            return s.toArray(new Party[s.size()]);
        }

        // If we have to consumers ci and cj with property
        //   ci.date <= cj.date
        //   ci.rate <= cj.date
        // then the consumer cj is prefarable.
        private Party[] getC(InputReader in, int n) {
            Party[] A = load(in, n);
            Stack<Party> s = new Stack<>();
            for (int i = 0; i < n; ++i) {
                while (!s.empty()) {
                    if (s.peek().rate <= A[i].rate) {
                        s.pop();
                    } else {
                        break;
                    }
                }
                s.push(A[i]);
            }
            return s.toArray(new Party[s.size()]);
        }

        private Party[] load(InputReader in, int n) {
            Party[] res = new Party[n];
            for (int i = 0; i < n; ++i) {
                int r = in.nextInt();
                int d = in.nextInt();
                res[i] = new Party(r, d);
            }
            Arrays.sort(res);
            return res;
        }

        private Party P[];
        private Party C[];

        private class Party implements Comparable<Party> {
            public final int rate;
            public final int date;

            public Party(int rate, int date) {
                this.rate = rate;
                this.date = date;
            }

            @Override
            public int compareTo(final Party other) {
                if (this.date != other.date) {
                    return this.date - other.date;
                }
                return this.rate - other.rate;
            }
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
            new MoneyForNothing().process(test, in, out);
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
