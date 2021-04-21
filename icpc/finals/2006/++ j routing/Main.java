import java.io.*;
import java.util.*;

public class Main {
    private static final class Routing {
        public boolean process(int testCase, InputReader in, PrintWriter out) {
            this.N = in.nextInt();
            this.M = in.nextInt();
            if (N == 0 && M == 0) {
                return false;
            }
            this.G = new int[N][N];
            for (int i = 0; i < M; ++i) {
                int a = in.nextInt() - 1;
                int b = in.nextInt() - 1;
                G[a][b] = 1;
            }
            int used[] = new int[N];
            int best = 0;
            best += spfa(0, 1, used);
            best += spfa(1, 0, used);
            if (best < oo) {
                Arrays.fill(used, 0);
                used[0] = 1;
                out.format("Network %d\nMinimum number of nodes = %d\n\n", testCase, doit(0, best, 1, used));
            } else {
                out.format("Network %d\nImpossible\n\n", testCase);
            }
            return true;
        }

        private int doit(int curr, int best, int have, int[] used) {
            if (best > have) {
                if (curr == 1) {
                    int temp = have + spfa(1, 0, used.clone());
                    if (best > temp) {
                        best = temp;
                    }
                    return best;
                }
                for (int next = 0; next < N; ++next) {
                    if (G[curr][next] > 0) {
                        if (used[next] == 0) {
                            used[next] = 1;
                            best = Math.min(best, doit(next, best, have + 1, used));
                            used[next] = 0;
                        }
                    }
                }
            }
            return best;
        }

        private int spfa(int source, int target, int[] used) {
            int[] best = new int[N];
            int[] prev = new int[N];
            Arrays.fill(best, oo);
            Queue<Integer> queue = new LinkedList<>();
            boolean inqueue[] = new boolean[N];
            for (best[source] = 0, inqueue[source] = true, queue.add(source); !queue.isEmpty(); ) {
                Integer curr = queue.poll();
                inqueue[curr] = false;
                for (int next = 0; next < N; ++next) {
                    if (G[curr][next] > 0) {
                        if (best[next] > best[curr] + 1 - used[next]) {
                            best[next] = best[curr] + 1 - used[next];
                            prev[next] = curr;
                            if (inqueue[next] == false) {
                                queue.add(next);
                                inqueue[next] = true;
                            }
                        }
                    }
                }
            }
            if (best[target] < oo) {
                for (int curr = target; curr != source;) {
                    used[curr] = 1;
                    curr = prev[curr];
                }
                return best[target];
            }
            return oo;
        }

        private int G[][];
        private int N;
        private int M;

        private static final int oo = (int)1e6;
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
                contd = new Routing().process(test, in, out);
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
