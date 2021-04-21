import java.io.*;
import java.time.*;
import java.time.format.*;
import java.util.*;

public class Main {
    private static final class IntersectingDates {
        public boolean process(int testCase, InputReader in, PrintWriter out) throws Exception {
            int n = in.nextInt();
            int q = in.nextInt();
            if (n + q == 0) {
                return false;
            }
            List<Range> obtained = new ArrayList<>();
            List<Range> required = new ArrayList<>();
            for (int i = 0; i < n; ++i) {
                LocalDate a = parse(in.next(), INP_FORMAT);
                LocalDate b = parse(in.next(), INP_FORMAT);
                obtained.add(new Range(a, b));
            }
            for (int i = 0; i < q; ++i) {
                LocalDate a = parse(in.next(), INP_FORMAT);
                LocalDate b = parse(in.next(), INP_FORMAT);
                required.add(new Range(a, b));
            }
            Stock stock = new Stock();
            stock.add(obtained);
            List<Range> res = stock.get(required);
            if (testCase > 1) {
                out.println();
                out.format("Case %d:\n", testCase);
            } else {
                out.format("Case %d:\n", testCase);
            }
            if (res.size() > 0) {
                for (Range r : res) {
                    out.format(RES_FORMAT, r.toString(OUT_FORMAT));
                }
            } else {
                out.format(RES_FORMAT, "No additional quotes are required.");
            }
            return true;
        }

        private static LocalDate parse(String s, String format) {
            return LocalDate.parse(s, DateTimeFormatter.ofPattern(format));
        }

        private static final String INP_FORMAT = "yyyyMMdd";
        private static final String OUT_FORMAT = "M/d/yyyy";
        private static final String RES_FORMAT = "    %s\n";

        private static class Stock {
            private List<Range> store = new ArrayList<>();

            public void add(List<Range> dates) {
                for (Range x : dates) {
                    store.add(x);
                }
            }

            public List<Range> get(List<Range> dates) {
                Stack<Range> source = stack(store);
                Stack<Range> target = stack(dates);
                List<Range> result = new ArrayList<>();
                while (!source.isEmpty() && !target.isEmpty()) {
                    Range S = source.pop();
                    Range T = target.pop();
                    if (S.max.compareTo(T.min) < 0) {
                        target.push(T);
                        continue;
                    }
                    if (T.max.compareTo(S.min) < 0) {
                        source.push(S);
                        result.add (T);
                        continue;
                    }
                    if (T.min.compareTo(S.min) < 0) {
                        result.add (new Range(T.min, S.min.minusDays(1)));
                        source.push(S);
                        target.push(new Range(S.min, T.max));
                        continue;
                    }
                    if (S.max.compareTo(T.max) < 0) {
                        target.push(new Range(S.max.plusDays(1), T.max));
                    } else {
                        source.push(S);
                    }
                }
                while (!target.isEmpty()) {
                    result.add(target.pop());
                }
                return merge(result);
            }

            private Stack<Range> stack(List<Range> list) {
                list = merge(list);
                Stack<Range> res = new Stack<>();
                for (int i = list.size() - 1; i >= 0; --i) {
                    res.push(list.get(i));
                }
                return res;
            }

            public static List<Range> merge(List<Range> list) {
                if (list.size() < 2) {
                    return list;
                }
                list.sort((x, y) -> {
                    if (x.min.compareTo(y.min) != 0) {
                        return x.min.compareTo(y.min);
                    }
                    return x.max.compareTo(y.max);
                });
                List<Range> res = new ArrayList<>();
                LocalDate a = null;
                LocalDate b = null;
                for (Range x : list) {
                    if (a == null && b == null) {
                        a = x.min;
                        b = x.max;
                    } else if (b.plusDays(1).compareTo(x.min) < 0) {
                        res.add(new Range(a, b));
                        a = x.min;
                        b = x.max;
                    } else if (b.compareTo(x.max) < 0) {
                        b = x.max;
                    }
                }
                res.add(new Range(a, b));
                return res;
            }
        }

        public void generate(PrintWriter out, int testCases) {
            Random rand = new Random(50847534);
            LocalDate MIN = LocalDate.of(1800, 01, 01);
            LocalDate MAX = LocalDate.of(2000, 12, 31);
            for (int testCase = 1; 2 * testCase <= testCases; ++testCase) {
                int[] ns = new int[2];
                ns[0] = rand.nextInt(100) + 1;
                ns[1] = rand.nextInt(100) + 1;
                out.println(ns[0] + " " + ns[1]);
                for (int n : ns) {
                    for (int i = 0; i < n; ++i) {
                        int x = rand.nextInt(1000) + 1;
                        LocalDate min = MIN.plusDays(x);
                        int y = rand.nextInt(1000) + 1;
                        LocalDate max = MAX.minusDays(y);
                        if (min.compareTo(MIN) < 0) min = MIN;
                        if (min.compareTo(MAX) > 0) min = MAX;
                        if (max.compareTo(MIN) < 0) max = MIN;
                        if (max.compareTo(MAX) > 0) max = MAX;
                        if (min.compareTo(max) <= 0) {
                            out.println(new Range(min, max).toString());
                        } else {
                            out.println(new Range(max, min).toString());
                        }
                    }
                }
            }
            for (int testCase = 1; 2 * testCase <= testCases; ++testCase) {
                List<Range> obtained = new ArrayList<>();
                List<Range> required = new ArrayList<>();
                LocalDate at = MIN;
                for (int i = 0; i < rand.nextInt(100) + 1; ++i) {
                    int x = rand.nextInt(100) + 1;
                    if (x < 10) {
                        x = 10;
                    }
                    LocalDate min = at;
                    LocalDate max = at.plusDays(2 * x);
                    Range r = new Range(min, max);
                    at = at.plusDays(x);
                    obtained.add(r);
                }
                List<Range> merged = Stock.merge(required);
                for (int i = 1; i < obtained.size(); ++i) {
                    Range prev = obtained.get(i - 1);
                    Range curr = obtained.get(i);
                    LocalDate min = prev.max.plusDays(1);
                    LocalDate max = curr.min.minusDays(1);
                    if (min.compareTo(max) <= 0) {
                        required.add(new Range(min, max));
                    } else {
                        required.add(new Range(max, min));
                    }
                }
                out.println(obtained.size() + " " + required.size());
                for (Range r : required) {
                    out.println(r.toString());
                }
                for (Range r : obtained) {
                    out.println(r.toString());
                }
            }
        }

        private static class Range {
            public final LocalDate min;
            public final LocalDate max;

            public Range(LocalDate min, LocalDate max) {
                this.min = min;
                this.max = max;
            }

            public String toString() {
                DateTimeFormatter formatter = DateTimeFormatter.ofPattern(INP_FORMAT);
                return min.format(formatter) + " " + max.format(formatter);
            }

            public String toString(String format) {
                DateTimeFormatter formatter = DateTimeFormatter.ofPattern(format);
                if (min.equals(max)) {
                    return min.format(formatter);
                } else {
                    return min.format(formatter) + " to " + max.format(formatter);
                }
            }
        }
    }

    public static void main(String[] args) throws Exception {
        InputReader in = new InputReader(new FileInputStream("input.txt"));
        PrintWriter out = new PrintWriter(new FileOutputStream("output.txt"));
        //InputReader in = new InputReader(System.in);
        //PrintWriter out = new PrintWriter(System.out);

        System.err.println("Test Case: Elapsed time");
        boolean contd = true;
        for (int test = 1; in.hasNext() && contd; ++test) {
            long beg = System.nanoTime();
            contd = new IntersectingDates().process(test, in, out);
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
