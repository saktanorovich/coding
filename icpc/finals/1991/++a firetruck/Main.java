import java.io.*;
import java.util.*;

public class Main {
    private static final class Firetruck {
        public boolean process(int testCase, InputReader in, PrintWriter out) {
            this.target = in.nextInt() - 1;
            this.street = new boolean[21][];
            this.wereAt = new boolean[21];
            this.cities = new Stack<>();
            for (int i = 0; i < 21; ++i) {
                street[i] = new boolean[21];
            }
            while (true) {
                int a = in.nextInt();
                int b = in.nextInt();
                if (a == 0 || b == 0) {
                    break;
                }
                street[a - 1][b - 1] = true;
                street[b - 1][a - 1] = true;
            }
            out.format("CASE %d:\n", testCase);
            this.wereAt[0] = true;
            cities.push(0);
            traverse(0, out);
            return true;
        }

        private void traverse(int city, PrintWriter out) {
            if (city == target) {
                routes = routes + 1;
                for (int i = 0; i < cities.size(); ++i) {
                    out.format("%d", cities.get(i) + 1);
                    if (i + 1 < cities.size()) {
                        out.format(" ");
                    }
                }
                out.format("\n");
                return;
            }
            if (possible(city)) {
                for (int next = 0; next < 21; ++next) {
                    if (street[city][next] && wereAt[next] == false) {
                        wereAt[next] = true;
                        cities.push(next);
                        traverse(next, out);
                        cities.pop();
                        wereAt[next] = false;
                    }
                }
            }
            if (city == 0) {
                out.format("There are %d routes from the firestation to streetcorner %d.\n", routes, target + 1);
            }
        }

        private boolean possible(int city) {
            boolean[] inqueue = new boolean[21];
            inqueue[city] = true;
            Queue<Integer> queue = new LinkedList<>();
            for (queue.add(city); queue.isEmpty() == false;) {
                city = queue.poll();
                if (city == target) {
                    return true;
                }
                for (int next = 0; next < 21; ++next) {
                    if (street[city][next] && wereAt[next] == false) {
                        if (inqueue[next] == false) {
                            inqueue[next] = true;
                            queue.add(next);
                        }
                    }
                }
            }
            return false;
        }

        private boolean street[][];
        private boolean wereAt[];
        private Stack<Integer> cities;
        private int target;
        private int routes;
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
