import java.io.*;
import java.util.*;

public class Main {
    private static final class FreeFormInput {
        public boolean process(int testCase, InputReader in, PrintWriter out) {
            var input = in.nextLine();
            if (input == null) {
                return false;
            }
            input = input.replaceAll(" ", "");
            var nums = input.split(",");
            var suma = 0.0;
            for (var num : nums) {
                suma += Double.parseDouble(num);
            }
            out.println(suma);
            return true;
        }
    }

    public static void main(String[] args) throws Exception {
        var in  = new InputReader(System.in);
        var out = new PrintWriter(System.out);

        if (args.length > 0 && args[0].equals("-g")) {
        } else {
            System.err.println("Test Case: Elapsed time");
            var contd = true;
            for (var test = 1; contd; ++test) {
                var beg = System.nanoTime();
                contd = new FreeFormInput().process(test, in, out);
                out.flush();
                var end = System.nanoTime();
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
                    var nextLine = reader.readLine();
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
