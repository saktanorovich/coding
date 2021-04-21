import java.io.*;
import java.util.*;

public class Main {
    private static final class MarbleGame {
        public boolean process(int testCase, InputReader in, PrintWriter out) {
            this.N = in.nextInt();
            this.M = in.nextInt();
            this.W = in.nextInt();
            if (N == 0 && M == 0 && W == 0) {
                return false;
            }
            int[][] mask = new int[N][N];
            for (int i = 1; i <= M; ++i) {
                int x = in.nextInt();
                int y = in.nextInt();
                mask[x][y] += i;
            }
            for (int i = 1; i <= M; ++i) {
                int x = in.nextInt();
                int y = in.nextInt();
                mask[x][y] -= i;
            }
            this.wall = new boolean[N][N][4];
            for (int i = 0; i < W; ++i) {
                int x1 = in.nextInt();
                int y1 = in.nextInt();
                int x2 = in.nextInt();
                int y2 = in.nextInt();
                for (int k = 0; k < 4; ++k) {
                    if (x1 + dx[k] == x2 && y1 + dy[k] == y2) {
                        wall[x1][y1][k] = true;
                    }
                    if (x2 + dx[k] == x1 && y2 + dy[k] == y1) {
                        wall[x2][y2][k] = true;
                    }
                }
            }
            int res = bfs(new State(mask));
            if (res < Integer.MAX_VALUE) {
                out.format("Case %d: %d moves\n\n", testCase, res);
            } else {
                out.format("Case %d: %s\n\n", testCase, "impossible");
            }
            return true;
        }

        private int bfs(State init) {
            Set<State> v = new HashSet<>();
            Queue<State> q = new LinkedList<>();
            v.add(init);
            q.add(init);
            State goal = new State(new int[N][N]);
            while (q.isEmpty() == false) {
                State curr = q.poll();
                if (curr.equals(goal)) {
                    return curr.move;
                }
                for (int k = 0; k < 4; ++k) {
                    State next = curr.next(k);
                    if (next != null) {
                        if (v.contains(next) == false) {
                            v.add(next);
                            q.add(next);
                        }
                    }
                }
            }
            return Integer.MAX_VALUE;
        }

        private boolean wall[][][];
        private int N;
        private int M;
        private int W;

        private final int dx[] = new int[] { -1, 0, +1, 0 };
        private final int dy[] = new int[] { 0, -1, 0, +1 };

        private final class State {
            private final String text;
            private final int mask[][];
            private final int hash;
            private final int move;

            public State(int[][] mask) {
                this(mask, 0);
            }

            public State(int[][] mask, int move) {
                this.mask = mask;
                StringBuilder s = new StringBuilder();
                for (int i = 0; i < N; ++i) {
                    for (int j = 0; j < N; ++j) {
                        if (mask[i][j] == 0) {
                            s.append('.');
                        } else if (mask[i][j] > 0) {
                            s.append((char)('a' + mask[i][j] - 1));
                        } else if (mask[i][j] < 0) {
                            s.append((char)('A' - mask[i][j] - 1));
                        }
                    }
                    s.append("\n");
                }
                this.text = s.toString();
                this.hash = text.hashCode();
                this.move = move;
            }

            public State next(int k) {
                int next[][] = new int[N][N];
                for (int x = 0; x < N; ++x) {
                    for (int y = 0; y < N; ++y) {
                        next[x][y] = mask[x][y];
                    }
                }
                for (boolean contd = true; contd;) {
                    contd = false;
                    for (int x = 0; x < N; ++x) {
                        for (int y = 0; y < N; ++y) {
                            if (next[x][y] > 0) {
                                if (wall[x][y][k] == false) {
                                    int u = x + dx[k];
                                    int v = y + dy[k];
                                    if (0 <= u && u < N && 0 <= v && v < N) {
                                        if (next[u][v] == 0 || next[u][v] == -next[x][y]) {
                                            next[u][v] += next[x][y];
                                            next[x][y] = 0;
                                            contd = true;
                                        } else if (next[u][v] < 0) {
                                            return null;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return new State(next, move + 1);
            }

            @Override
            public boolean equals(Object o) {
                State s = (State)o;
                if (hash == s.hash) {
                    for (int i = 0; i < N; ++i) {
                        for (int j = 0; j < N; ++j) {
                            if (mask[i][j] != s.mask[i][j]) {
                                return false;
                            }
                        }
                    }
                    return true;
                }
                return false;
            }

            @Override
            public int hashCode() {
                return hash;
            }

            @Override
            public String toString() {
                return text;
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
                contd = new MarbleGame().process(test, in, out);
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
