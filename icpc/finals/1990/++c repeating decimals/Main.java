import java.io.*;
import java.util.*;

public class Main {
    private static final class RepeatingDecimals {
        public boolean process(int testCase, InputReader in, PrintWriter out) {
            int num = in.nextInt();
            int den = in.nextInt();
            out.format("%d/%d = ", num, den);
            if (num % den == 0) {
                out.format("%d.(0)\n", num / den);
                out.format("   ");
                out.format("%d = number of digits in repeating cycle\n\n", 1);
                return true;
            }
            out.format("%d.", num / den);
            StringBuilder builder = new StringBuilder();
            int[] indx = new int[den];
            Arrays.fill(indx, -1);
            for (num %= den; num > 0;) {
                if (indx[num] == -1) {
                    indx[num] = builder.length();
                    num *= 10;
                    builder.append(num / den);
                    num %= den;
                } else {
                    builder.insert(indx[num], "(");
                    builder.append(")");
                    for (int i = 0; i < builder.length(); ++i) {
                        out.print(builder.charAt(i));
                        if (builder.charAt(i) == '(') {
                            int len = builder.length() - i - 2;
                            int cnt = i;
                            for (++i; i < builder.length(); ++i) {
                                cnt = cnt + (builder.charAt(i) != ')' ? 1 : 0);
                                if (cnt < 51) {
                                    out.print(builder.charAt(i));
                                } else {
                                    out.format("...)");
                                    break;
                                }
                            }
                            out.format("\n   ");
                            out.format("%d = number of digits in repeating cycle\n\n", len);
                            return true;
                        }
                    }
                }
            }
            out.format("%s(0)\n", builder.toString());
            out.format("   ");
            out.format("%d = number of digits in repeating cycle\n\n", 1);
            return true;
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
            for (int test = 1; in.hasNext(); ++test) {
                long beg = System.nanoTime();
                contd = new RepeatingDecimals().process(test, in, out);
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
