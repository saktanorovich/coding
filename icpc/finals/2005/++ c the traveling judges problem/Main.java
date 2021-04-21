import java.io.*;
import java.util.*;

public class Main {
    private static final class TheTravelingJudgesProblem {
        public boolean process(int testCase, InputReader in, PrintWriter out) {
            this.nodes = in.nextInt();
            if (nodes == -1) {
                return false;
            }
            this.point = in.nextInt() - 1;
            this.edges = in.nextInt();
            this.graph = new int[nodes][nodes];
            for (int i = 0; i < nodes; ++i) {
                Arrays.fill(graph[i], Integer.MAX_VALUE);
            }
            for (int i = 0; i < edges; ++i) {
                int a = in.nextInt() - 1;
                int b = in.nextInt() - 1;
                int c = in.nextInt();
                graph[a][b] = c;
                graph[b][a] = c;
            }
            this.judgesNum = in.nextInt();
            this.judges = new int[judgesNum];
            for (int i = 0; i < judgesNum; ++i) {
                judges[i] = in.nextInt() - 1;
            }
            int use = exclude((1 << nodes) - 1, point);
            for (int judge : judges) {
                use = exclude(use, judge);
            }
            this.citiesNum = cardinality(use);
            this.cities = new int[citiesNum];
            for (int i = 0, k = 0; i < nodes; ++i) {
                if (contains(use, i)) {
                    cities[k++] = i;
                }
            }
            MST best = new MST(Integer.MAX_VALUE, null);
            for (int mask = 0; mask < 1 << citiesNum; ++mask) {
                int set = include(mask, point);
                for (int judge : judges) {
                    set = include(set, judge);
                }
                for (int i = 0; i < citiesNum; ++i) {
                    if (contains(mask, i)) {
                        set = include(set, cities[i]);
                    }
                }
                MST curr = prim(subgraph(set), cardinality(set));
                if (best.cost > curr.cost) {
                    best.cost = curr.cost;
                    best.prev = curr.prev;
                }
            }
            out.format("Case %d: distance = %d\n", testCase, best.cost);
            for (int judge : judges) {
                out.format("   %s\n", best.trace(judge));
            }
            out.format("\n");
            return true;
        }

        private MST prim(int[][] graph, int select) {
            int cost = 0;
            int prev[] = new int[nodes];
            int used[] = new int[nodes];
            int dist[] = new int[nodes];
            Arrays.fill(dist, Integer.MAX_VALUE);
            dist[point] = 0;
            for (; select > 0; --select) {
                int best = Integer.MAX_VALUE;
                int indx = -1;
                for (int i = 0; i < nodes; ++i) {
                    if (used[i] == 0) {
                        if (best > dist[i]) {
                            best = dist[i];
                            indx = i;
                        }
                    }
                }
                if (indx == -1) {
                    return new MST(Integer.MAX_VALUE, null);
                }
                used[indx] = 1;
                cost += best;
                for (int next = 0; next < nodes; ++next) {
                    if (used[next] == 0) {
                        if (dist[next] > graph[indx][next]) {
                            dist[next] = graph[indx][next];
                            prev[next] = indx;
                        }
                    }
                }
            }
            return new MST(cost, prev);
        }

        private int[][] subgraph(int set) {
            int res[][] = new int[nodes][nodes];
            for (int i = 0; i < nodes; ++i) {
                Arrays.fill(res[i], Integer.MAX_VALUE);
            }
            for (int i = 0; i < nodes; ++i) {
                if (contains(set, i)) {
                    for (int j = i + 1; j < nodes; ++j) {
                        if (contains(set, j)) {
                            res[i][j] = graph[i][j];
                            res[j][i] = graph[j][i];
                        }
                    }
                }
            }
            return res;
        }

        private int include(int set, int x) {
            return set | (1 << x);
        }

        private int exclude(int set, int x) {
            if (contains(set, x)) {
                return set ^ (1 << x);
            } else {
                return set;
            }
        }

        private int cardinality(int set) {
            int res = 0;
            while (set > 0) {
                res += 1;
                set -= set & (-set);
            }
            return res;
        }

        private boolean contains(int set, int x) {
            return (set & (1 << x)) != 0;
        }

        private int graph[][];
        private int nodes;
        private int edges;
        private int point;

        private int judgesNum;
        private int citiesNum;
        private int judges[];
        private int cities[];

        private final class MST {
            public int cost;
            public int prev[];

            public MST(int cost, int[] prev) {
                this.cost = cost;
                this.prev = prev;
            }

            public String trace(int x) {
                StringBuilder res = new StringBuilder();
                res.append(x + 1);
                while (x != point) {
                    res.append("-");
                    res.append(prev[x] + 1);
                    x = prev[x];
                }
                return res.toString();
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
                contd = new TheTravelingJudgesProblem().process(test, in, out);
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
