import java.io.*;
import java.util.*;

public class Main {
    private static final class RememberTheALaMode {
        public boolean process(int testCase, InputReader in, PrintWriter out) {
            int A = in.nextInt();
            int B = in.nextInt();
            if (A == 0 && B == 0) {
                return false;
            }
            int a[] = new int[A];
            for (int i = 0; i < A; ++i) {
                a[i] = in.nextInt();
            }
            int b[] = new int[B];
            for (int i = 0; i < B; ++i) {
                b[i] = in.nextInt();
            }
            int c1[][] = new int[A][B];
            int c2[][] = new int[A][B];
            for (int i = 0; i < A; ++i) {
                for (int j = 0; j < B; ++j) {
                    String s = in.next();
                    if (s.equals("-1") == false) {
                        int z = parse(s);
                        c1[i][j] = +1 * z;
                        c2[i][j] = -1 * z + MAX;
                    }
                }
            }
            MCFMResult min = get(A, B, a, b, c1);
            MCFMResult max = get(A, B, a, b, c2);
            out.format("Problem %d: %.2f to %.2f\n", testCase
                , (min.cost) / 100.0
                , (max.flow * MAX - max.cost) / 100.0);
            return true;
        }

        private int parse(String s) {
            int z = s.indexOf('.');
            if (z >= 0) {
                String le = s.substring(0, z);
                String ri = s.substring(z + 1);
                while (ri.length() < 2) {
                    ri += "0";
                }
                return Integer.parseInt(le) * 100 + Integer.parseInt(ri);
            }
            return Integer.parseInt(s) * 100;
        }

        private MCFMResult get(int A, int B, int[] a, int[] b, int[][] c) {
            MCMF mcmf = new MCMF(A + B + 2);
            int S = A + B;
            int T = A + B + 1;
            for (int i = 0; i < A; ++i) {
                mcmf.add(S, i, 0, a[i]);
            }
            for (int i = 0; i < B; ++i) {
                mcmf.add(A + i, T, 0, b[i]);
            }
            for (int i = 0; i < A; ++i) {
                for (int j = 0; j < B; ++j) {
                    if (c[i][j] > 0) {
                        mcmf.add(i, A + j, c[i][j], MAX);
                    }
                }
            }
            return mcmf.get(S, T);
        }

        private static final int MAX = (int)1e6;

        private static final class MCMF {
            private final Queue<Integer> queue;
            private final List<Edge>[] graph;
            private final boolean[] mark;
            private final long[] dist;

            public MCMF(int n) {
                graph = new List[n];
                for (int i = 0; i < n; ++i) {
                    graph[i] = new ArrayList<>();
                }
                queue = new LinkedList<>();
                mark = new boolean[n];
                dist = new long[n];
            }

            public int flow(int src, int dst) {
                for (Edge edge : graph[src]) {
                    if (edge.dst == dst) {
                        return edge.flow;
                    }
                }
                return 0;
            }

            public void add(int source, int target, int cost, int capa) {
                Edge e1 = new Edge(source, target, +cost, capa);
                Edge e2 = new Edge(target, source, -cost, 0);
                e1.back = e2;
                e2.back = e1;
                graph[source].add(e1);
                graph[target].add(e2);
            }

            public MCFMResult get(int source, int target) {
                return get(source, target, Integer.MAX_VALUE);
            }

            public MCFMResult get(int source, int target, int maxFlow) {
                int cost = 0;
                int flow = 0;
                Edge[] from = new Edge[graph.length];
                while (flow < maxFlow && augment(source, target, from)) {
                    int by = Integer.MAX_VALUE;
                    for (int at = target; at != source; at = from[at].src) {
                        by = Math.min(by, from[at].residual());
                    }
                    by = Math.min(by, maxFlow - flow);
                    for (int at = target; at != source; at = from[at].src) {
                        Edge edge = from[at];
                        Edge back = from[at].back;
                        edge.flow += by;
                        back.flow -= by;
                        cost += by * edge.cost;
                    }
                    flow += by;
                }
                return new MCFMResult(flow, cost);
            }

            private boolean augment(int source, int target, Edge[] from) {
                assert hasNegativeCycle(source) == false : "Negative cycle detected";

                Arrays.fill(dist, Long.MAX_VALUE / 2);
                for (dist[source] = 0, queue.add(source); queue.isEmpty() == false;) {
                    int at = queue.poll();
                    mark[at] = false;
                    for (Edge edge : graph[at]) {
                        if (edge.residual() > 0) {
                            if (dist[edge.dst] > dist[edge.src] + edge.cost) {
                                dist[edge.dst] = dist[edge.src] + edge.cost;
                                from[edge.dst] = edge;
                                if (mark[edge.dst] == false) {
                                    mark[edge.dst] = true;
                                    queue.add(edge.dst);
                                }
                            }
                        }
                    }
                }
                return dist[target] < Long.MAX_VALUE / 2;
            }

            private boolean hasNegativeCycle(int source) {
                Arrays.fill(dist, Long.MAX_VALUE / 2);
                dist[source] = 0;
                for (int i = 0; i <= graph.length; ++i) {
                    for (int u = 0; u < graph.length; ++u) {
                        for (Edge edge : graph[u]) {
                            if (edge.residual() > 0) {
                                if (dist[edge.dst] > dist[edge.src] + edge.cost) {
                                    dist[edge.dst] = dist[edge.src] + edge.cost;
                                    if (i >= graph.length) {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
                return false;
            }

            private static class Edge {
                public final int src;
                public final int dst;
                public final int cost;
                public final int capa;
                public int flow;
                public Edge back;

                public Edge(int src, int dst, int cost, int capa) {
                    this.src = src;
                    this.dst = dst;
                    this.cost = cost;
                    this.capa = capa;
                    this.flow = 0;
                }

                public int residual() {
                    return capa - flow;
                }
            }
        }

        private static final class MCFMResult {
            public final int flow;
            public final int cost;

            public MCFMResult(int flow, int cost) {
                this.flow = flow;
                this.cost = cost;
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
            //int T = in.nextInt();
            for (int test = 1; in.hasNext() && contd; ++test) {
                long beg = System.nanoTime();
                contd = new RememberTheALaMode().process(test, in, out);
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
