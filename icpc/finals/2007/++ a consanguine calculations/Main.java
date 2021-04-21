import java.io.*;
import java.util.*;

public class Main {
    private static final class ConsanguineCalculations {
        public boolean process(int testCase, InputReader in, PrintWriter out) {
            String p1 = in.next();
            String p2 = in.next();
            String ch = in.next();
            if (p1.equals("E")) {
                return false;
            }
            out.format("Case %d: ", testCase);
            if (p1.equals("?")) {
                out.format("%s %s %s\n", one(p2, ch), p2, ch);
            } else if (p2.equals("?")) {
                out.format("%s %s %s\n", p1, one(p1, ch), ch);
            } else {
                out.format("%s %s %s\n", p1, p2, two(p1, p2));
            }
            return true;
        }

        private String one(String m, String c) {
            List<String> res = new ArrayList<>();
            for (String f : BLOOD) {
                if (okay(m, f, c)) {
                    res.add(f);
                }
            }
            return toString(res);
        }

        private String two(String m, String f) {
            List<String> res = new ArrayList<>();
            for (String c : BLOOD) {
                if (okay(m, f, c)) {
                    res.add(c);
                }
            }
            return toString(res);
        }

        private boolean okay(String m, String f, String c) {
            // Rh+ = { ++, +-, + }
            // Rh- = { -- }
            int mRh = "-+".indexOf(m.charAt(m.length() - 1));
            int fRh = "-+".indexOf(f.charAt(f.length() - 1));
            int cRh = "-+".indexOf(c.charAt(c.length() - 1));
            if (cRh > mRh + fRh) {
                return false;
            }
            int mAbo = indexOf(m.substring(0, m.length() - 1));
            int fAbo = indexOf(f.substring(0, f.length() - 1));
            int cAbo = indexOf(c.substring(0, c.length() - 1));
            if ((TABLE[mAbo][fAbo] & (1 << cAbo)) != 0) {
                return true;
            } else {
                return false;
            }
        }

        private int indexOf(String abo) {
            switch (abo) {
                case  "A": return 0;
                case  "B": return 1;
                case "AB": return 2;
                case  "O": return 3;
            }
            return -1;
        }

        private static String toString(List<String> s) {
            if (s.size() > 0) {
                if (s.size() == 1) {
                    return s.get(0);
                } else {
                    StringBuilder res = new StringBuilder();
                    res.append("{");
                    for (int i = 0; i < s.size(); ++i) {
                        res.append(s.get(i));
                        if (i + 1 < s.size()) {
                            res.append(", ");
                        }
                    }
                    res.append("}");
                    return res.toString();
                }
            }
            return "IMPOSSIBLE";
        }

        private static final int  A = 1;
        private static final int  B = 2;
        private static final int AB = 4;
        private static final int  O = 8;

        private static final int[][] TABLE = new int[][] {
                /*        A          B       AB     O  */
        /*  A */ {      A|O,  A|B|AB|O,  A|B|AB,  A|O  },
        /*  B */ { A|B|AB|O,       B|O,  A|B|AB,  B|O  },
        /* AB */ {   A|B|AB,    A|B|AB,  A|B|AB,  A|B  },
        /*  O */ {      A|O,       B|O,     A|B,    O  },
        };

        private static final String[] BLOOD = new String[] {
            "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-"
        };
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
                contd = new ConsanguineCalculations().process(test, in, out);
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
