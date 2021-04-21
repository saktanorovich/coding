import java.io.*;
import java.util.*;

public class Main {
    private static final class NearPerfectShuffle {
        public boolean process(int testCase, InputReader in, PrintWriter out) {
            int[] deck = new int[52];
            for (int i = 0; i < 52; ++i) {
                deck[i] = in.nextInt();
            }
            Report res = get(deck);
            out.format("Case %d\nNumber of shuffles = %d\n", testCase, res.number);
            if (res.hasErrors()) {
                for (int i = 1; i <= res.number; ++i) {
                    if (res.errors[i] != -1) {
                        out.format("Error in shuffle %d at location %d\n", i, res.errors[i]);
                    }
                }
            } else {
                out.format("No error in any shuffle\n");
            }
            out.format("\n");
            return true;
        }

        private Report get(int[] deck) {
            int number = 0;
            for (int i = 0; i < 11; ++i) {
                List<Integer> errors = diff(deck, PERFECT[i]);
                if (errors.size() <= 20) {
                    number = i;
                    break;
                }
            }
            Report curr = new Report(number, new int[number + 1]);
            Report best = new Report(number, null);
            Arrays.fill(curr.errors, -1);
            for (int swaps = 0; swaps <= number; ++swaps) {
                search(deck, number, swaps, curr, best);
                if (best.errors != null) {
                    return best;
                }
            }
            throw new RuntimeException();
        }

        private void search(int[] deck, int number, int swaps, Report curr, Report best) {
            List<Integer> diff = diff(deck, PERFECT[number]);
            if (diff.size() > 2 * number) {
                return;
            }
            if (diff.size() == 0) {
                if (best.errors == null || less(curr.errors, best.errors)) {
                    if (best.errors == null) {
                        best.errors = new int[best.number + 1];
                    }
                    for (int i = 0; i <= best.number; ++i) {
                        best.errors[i] = curr.errors[i];
                    }
                }
                return;
            }
            if (swaps > number) {
                return;
            }
            int[] rev = reverse(deck);
            search(rev, number - 1, swaps, curr, best);
            if (swaps == 0) {
                return;
            }
            for (int i : diff) {
                if (i < 51) {
                    int p1;
                    int p2;
                    if (i % 2 == 0) {
                        p1 = 26 + i / 2; p2 = (i + 1) / 2;
                    } else {
                        p1 = i / 2; p2 = 26 + (i + 1) / 2;
                    }
                    swap(rev, p1, p2);
                    curr.errors[number] = i;
                    search(rev, number - 1, swaps - 1, curr, best);
                    curr.errors[number] = -1;
                    swap(rev, p1, p2);
                }
            }
        }

        private List<Integer> diff(int[] a, int[] b) {
            List<Integer> res = new ArrayList<>();
            for (int i = 0; i < 52; ++i) {
                if (a[i] != b[i]) {
                    res.add(i);
                }
            }
            return res;
        }

        private boolean less(int[] list1, int[] list2) {
            List<Integer> pos1 = make(list1);
            List<Integer> pos2 = make(list2);
            if (pos1.size() < pos2.size()) {
                return true;
            }
            if (pos1.size() > pos2.size()) {
                return false;
            }
            for (int i = 0; i < pos1.size(); ++i) {
                if (pos1.get(i) < pos2.get(i)) {
                    return true;
                }
                if (pos1.get(i) > pos2.get(i)) {
                    return false;
                }
            }
            return false;
        }

        private List<Integer> make(int[] errors) {
            List<Integer> res = new ArrayList<>();
            for (int e : errors) {
                if (e != -1) {
                    res.add(e);
                }
            }
            Collections.sort(res);
            return res;
        }

        private void swap(int[] a, int x, int y) {
            int t = a[x];
            a[x] = a[y];
            a[y] = t;
        }

        private final class Report {
            public int errors[];
            public int number;

            public Report(int number, int[] errors) {
                this.number = number;
                this.errors = errors;
            }

            public boolean hasErrors() {
                for (int e : errors) {
                    if (e != -1) {
                        return true;
                    }
                }
                return false;
            }
        }

        private static int[] shuffle(int[] deck) {
            int[] res = new int[52];
            for (int i = 0, a = 0, b = 26; i < 52; ++i) {
                if (i % 2 == 0) {
                    res[i] = deck[b++];
                } else {
                    res[i] = deck[a++];
                }
            }
            return res;
        }

        private static int[] reverse(int[] deck) {
            int[] res = new int[52];
            for (int i = 0, a = 0, b = 26; i < 52; ++i) {
                if (i % 2 == 0) {
                    res[b++] = deck[i];
                } else {
                    res[a++] = deck[i];
                }
            }
            return res;
        }

        private static final int PERFECT[][] = new int[11][];
        static {
            PERFECT[0] = new int[52];
            for (int i = 0; i < 52; ++i) {
                PERFECT[0][i] = i;
            }
            for (int t = 1; t < 11; ++t) {
                PERFECT[t] = shuffle(PERFECT[t - 1]);
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
            int T = in.nextInt();
            for (int test = 1; test <= T && contd; ++test) {
                long beg = System.nanoTime();
                contd = new NearPerfectShuffle().process(test, in, out);
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
