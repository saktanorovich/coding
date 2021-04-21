import java.io.*;
import java.util.*;

public class Main {
    private static final class ResourceAllocation {
        public boolean process(int testCase, InputReader in, PrintWriter out) {
            int D = in.nextInt();
            if (D == 0) {
                return false;
            }
            int P = in.nextInt();
            int B = in.nextInt();
            Department deps[] = new Department[D];
            for (int i = 0; i < D; ++i) {
                deps[i] = Department.readFrom(in);
            }
            Allocation curr = new Allocation(D);
            Allocation best = new Allocation(D);
            search(deps, 0, P, B, curr, best);
            if (testCase > 1) {
                out.format("\n");
                out.format("\n");
            }
            out.format("Optimal resource allocation problem #%d\n\n", testCase);
            out.format("Total budget: $%d\n", best.totalMoney);
            out.format("Total new programmers: %d\n", best.totalHired);
            out.format("Total productivity increase: %d\n", best.totalLines);
            out.format("\n");
            for (int i = 0; i < D; ++i) {
                out.format("Division #%d resource allocation:\n", i + 1);
                out.format("Budget:  $%d\n", best.money[i]);
                out.format("Programmers: %d\n", best.hired[i]);
                out.format("Incremental lines of code: %d\n", best.lines[i]);
                if (i + 1 < D) {
                    out.format("\n");
                }
            }
            return true;
        }

        private void search(Department[] deps, int index, int P, int B, Allocation curr, Allocation best) {
            if (index == deps.length) {
                if (best.totalLines < curr.totalLines) {
                    best.totalLines = curr.totalLines;
                    best.totalHired = curr.totalHired;
                    best.totalMoney = curr.totalMoney;
                    for (int i = 0; i < deps.length; ++i) {
                        best.hired[i] = curr.hired[i];
                        best.money[i] = curr.money[i];
                        best.lines[i] = curr.lines[i];
                    }
                }
                return;
            }
            Department dep = deps[index];
            for (int i = 0; i < dep.programmers.length; ++i) {
                if (dep.programmers[i] <= P) {
                    curr.hired[index] = dep.programmers[i];
                    for (int j = 0; j < dep.budgetCases.length; ++j) {
                        if (dep.budgetCases[j] <= B) {
                            curr.money[index] = dep.budgetCases[j];
                            curr.lines[index] = dep.linesOfCode[i][j];
                            curr.totalHired += curr.hired[index];
                            curr.totalMoney += curr.money[index];
                            curr.totalLines += curr.lines[index];
                            search(deps, index + 1, P - curr.hired[index], B - curr.money[index], curr, best);
                            curr.totalHired -= curr.hired[index];
                            curr.totalMoney -= curr.money[index];
                            curr.totalLines -= curr.lines[index];
                            curr.money[index] = 0;
                            curr.lines[index] = 0;
                        }
                    }
                    curr.hired[index] = 0;
                }
            }

        }

        private static final class Allocation {
            public int totalHired;
            public int totalMoney;
            public int totalLines;
            public int hired[];
            public int money[];
            public int lines[];

            public Allocation(int deps) {
                this.hired = new int[deps];
                this.money = new int[deps];
                this.lines = new int[deps];
            }
        }

        private static final class Department {
            public int programmers[];
            public int budgetCases[];
            public int linesOfCode[][];

            public static Department readFrom(InputReader in) {
                Department dep = new Department();
                dep.programmers = read(in);
                dep.budgetCases = read(in);
                dep.linesOfCode = read(in, dep.programmers.length, dep.budgetCases.length);
                return dep;
            }

            private static int[] read(InputReader in) {
                int n = in.nextInt();
                int[] res = new int[n];
                for (int i = 0; i < n; ++i) {
                    res[i] = in.nextInt();
                }
                return res;
            }
    
            private static int[][] read(InputReader in, int n, int m) {
                int[][] res = new int[n][];
                for (int i = 0; i < n; ++i) {
                    res[i] = new int[m];
                    for (int j = 0; j < m; ++j) {
                        res[i][j] = in.nextInt();
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
            for (int test = 1; contd; ++test) {
                long beg = System.nanoTime();
                contd = new ResourceAllocation().process(test, in, out);
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
