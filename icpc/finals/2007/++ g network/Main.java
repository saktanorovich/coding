import java.io.*;
import java.util.*;

public class Main {
    private static final class Network {
        public boolean process(int testCase, InputReader in, PrintWriter out) {
            int N = in.nextInt();
            int M = in.nextInt();
            if (N == 0 && M == 0) {
                return false;
            }
            int memo[] = new int[N];
            for (int i = 0; i < N; ++i) {
                memo[i] = in.nextInt();
            }
            Packet pack[] = new Packet[M];
            for (int i = 0; i < M; ++i) {
                int msg = in.nextInt() - 1;
                int beg = in.nextInt() - 1;
                int end = in.nextInt() - 1;
                pack[i] = new Packet(msg, beg, end);
            }
            // the key note to understand the problem is that the
            // messages can pass the buffer in any order, but all
            // packets from a single message must pass the buffer
            // consecutively and in order
            int res = Integer.MAX_VALUE;
            for (Integer[] order : MathUtils.permute(N)) {
                Buffer buffer = new Buffer(memo, order);
                for (Packet p : pack) {
                    buffer.pass(p);
                }
                res = Math.min(res, buffer.size);
            }
            out.format("Case %d: %d\n\n", testCase, res);
            return true;
        }

        private final class Packet {
            public final int msg;
            public final int beg;
            public final int end;

            public Packet(int msg, int beg, int end) {
                this.msg = msg;
                this.beg = beg;
                this.end = end;
            }

            public int size() {
                return end - beg + 1;
            }
        }

        private final class Buffer {
            public final int memo[][];
            public final int have[];
            public final int last[];
            public Integer perm[];
            public int size;

            public Buffer(int[] memo, Integer perm[]) {
                this.memo = new int[memo.length][];
                this.last = new int[memo.length];
                this.have = new int[memo.length];
                for (int i = 0; i < memo.length; ++i) {
                    this.memo[i] = new int[memo[i]];
                }
                this.perm = perm;
                this.size = 0;
            }

            public void pass(Packet p) {
                for (int x = p.beg; x <= p.end; ++x) {
                    memo[p.msg][x] = 1;
                }
                have[p.msg] += p.size();
                scan(p.msg);
                int wait = 0;
                int curr = 0;
                for (Integer o : perm) {
                    if (wait == 1) {
                        curr += have[o];
                    } else if (last[o] < memo[o].length) {
                        curr += have[o] - last[o];
                        wait = 1;
                    }
                }
                size = Math.max(size, curr);
            }

            private void scan(int x) {
                while (last[x] < memo[x].length) {
                    if (memo[x][last[x]] == 1) {
                        last[x]++;
                    } else {
                        break;
                    }
                }
            }
        }

        private static final class MathUtils {
            public static List<Integer[]> permute(int N) {
                Integer[] list = new Integer[N];
                for (int i = 0; i < N; ++i) {
                    list[i] = i;
                }
                return permute(list);
            }

            public static List<Integer[]> permute(Integer[] list) {
                List<Integer[]> res = new ArrayList<>();
                if (list.length == 0) {
                    res.add(new Integer[0]);
                } else {
                    for (int i = 0; i < list.length; ++i) {
                        for (Integer[] nxt : permute(remove(list, i))) {
                            res.add(concat(list[i], nxt));
                        }
                    }
                }
                return res;
            }

            private static Integer[] concat(Integer item, Integer[] list) {
                List<Integer> res = new ArrayList<>();
                res.add(item);
                res.addAll(Arrays.asList(list));
                return res.toArray(new Integer[res.size()]);
            }

            private static Integer[] remove(Integer[] list, int index) {
                Integer[] res = new Integer[list.length - 1];
                for (int i = 0; i < list.length - 1; ++i) {
                    if (i < index) {
                        res[i] = list[i];
                    } else {
                        res[i] = list[i + 1];
                    }
                }
                return res;
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
                contd = new Network().process(test, in, out);
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
