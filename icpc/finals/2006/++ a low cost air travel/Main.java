import java.io.*;
import java.util.*;

public class Main {
    private static final class LowCostAirTravel {
        public boolean process(int testCase, InputReader in, PrintWriter out) {
            this.NT = in.nextInt();
            if (NT == 0) {
                return false;
            }
            this.routes = new int[NT][];
            this.prices = new int[NT];
            for (int i = 0; i < NT; ++i) {
                prices[i] = in.nextInt();
                routes[i] = new int[in.nextInt()];
                for (int j = 0; j < routes[i].length; ++j) {
                    routes[i][j] = in.nextInt();
                }
            }
            this.stops = new HashMap<>();
            this.NI = in.nextInt();
            for (int i = 1; i <= NI; ++i) {
                int trip[] = new int[in.nextInt()];
                for (int j = 0; j < trip.length; ++j) {
                    trip[j] = in.nextInt();
                }
                int tickets[] = doit(trip, trip.length - 1);
                int cost = 0;
                for (int ticket : tickets) {
                    cost += prices[ticket];
                }
                out.format("Case %d, Trip %d: Cost = %d\n", testCase, i, cost);
                out.format("  Tickets used:");
                for (int ticket : tickets) {
                    out.format(" %d", ticket + 1);
                }
                out.println();
            }
            return true;
        }

        private int[] doit(int[] trip, int T) {
            SPFA spfa = new SPFA(1 << 12);
            for (int at = 0; at <= T; ++at) {
                for (int ticket = 0; ticket < NT; ++ticket) {
                    int has = at;
                    int now = at;
                    for (int j = 1; j < routes[ticket].length; ++j) {
                        if (now + 1 <= T) {
                            if (routes[ticket][j] == trip[now + 1]) {
                                now = now + 1;
                            }
                            Stop source = new Stop(routes[ticket][0], has);
                            Stop target = new Stop(routes[ticket][j], now);
                            spfa.add(get(source), get(target), prices[ticket], ticket);
                        }
                    }
                }
            }
            Stop source = new Stop(trip[0], 0);
            Stop target = new Stop(trip[T], T);
            return spfa.get(get(source), get(target));
        }

        private int get(Stop stop) {
            if (stops.containsKey(stop) == false) {
                stops.put(stop, stops.size());
            }
            return stops.get(stop);
        }

        private Map<Stop, Integer> stops;
        private int routes[][];
        private int prices[];
        private int NT;
        private int NI;

        private static final class Stop implements Comparable<Stop> {
            public final int city;
            public final int have;

            public Stop(int city, int have) {
                this.city = city;
                this.have = have;
            }

            @Override
            public int compareTo(Stop o) {
                if (city != o.city) {
                    return city - o.city;
                }
                return have - o.have;
            }

            @Override
            public int hashCode() {
                return city * 3337 + have;
            }

            @Override
            public boolean equals(Object obj) {
                return compareTo((Stop)obj) == 0;
            }
        }

        private static final class SPFA {
            private final List<Arc>[] graph;

            public SPFA(int V) {
                graph = new ArrayList[V];
                for (int i = 0; i < V; ++i) {
                    graph[i] = new ArrayList<>();
                }
            }

            public void add(int source, int target, int price, int label) {
                graph[source].add(new Arc(source, target, price, label));
            }

            public int[] get(int source, int target) {
                int[] best = new int[graph.length];
                Arc[] prev = new Arc[graph.length];
                Arrays.fill(best, Integer.MAX_VALUE);
                Queue<Integer> queue = new LinkedList<>();
                boolean inqueue[] = new boolean[graph.length];
                for (best[source] = 0, inqueue[source] = true, queue.add(source); !queue.isEmpty();) {
                    Integer curr = queue.poll();
                    inqueue[curr] = false;
                    for (Arc edge : graph[curr]) {
                        if (best[edge.target] > best[curr] + edge.price) {
                            best[edge.target] = best[curr] + edge.price;
                            prev[edge.target] = edge;
                            if (inqueue[edge.target] == false) {
                                queue.add(edge.target);
                                inqueue[edge.target] = true;
                            }
                        }
                    }
                }
                Stack<Integer> path = new Stack<>();
                for (int curr = target; curr != source;) {
                    path.push(prev[curr].label);
                    curr = prev[curr].source;
                }
                int[] result = new int[path.size()];
                for (int i = 0; !path.isEmpty(); ++i) {
                    result[i] = path.pop();
                }
                return result;
            }

            private static final class Arc {
                public final int source;
                public final int target;
                public final int price;
                public final int label;

                public Arc(int source, int target, int price, int label) {
                    this.source = source;
                    this.target = target;
                    this.price = price;
                    this.label = label;
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
            //int T = in.nextInt();
            for (int test = 1; in.hasNext() && contd; ++test) {
                long beg = System.nanoTime();
                contd = new LowCostAirTravel().process(test, in, out);
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
