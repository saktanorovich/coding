import java.io.*;
import java.util.*;

public class Main {
    private static final class CommunicationLine {
        public boolean process(int testCase, InputReader in, PrintWriter out) {
            var N = in.nextInt();
            var M = in.nextInt();
            var S = in.next();
            var T = in.next();
            var V = new Character[256];
            for (var i = 0; i < S.length(); ++i) {
                V[S.charAt(i)] = T.charAt(i);
            }
            var lines = new ArrayList<String>();
            while (true) {
                var line = in.nextLine();
                if (line.startsWith("****")) {
                    break;
                }
                line = translation(line, V);
                line = compression(line);
                lines.add(line);
            }
            if (lines.size() > 0) {
                var buffer = new StringBuilder(lines.get(0));
                for (var i = 1; i < lines.size(); ++i) {
                    var line = lines.get(i);
                    if (buffer.length() + line.length() <= N) {
                        buffer.append(line);
                    } else {
                        out.println(buffer.toString());
                        buffer = new StringBuilder(line);
                    }
                }
                out.println(buffer.toString());
            }
            return false;
        }

        private String translation(String s, Character[] v) {
            var buffer = new StringBuilder();
            for (var c : s.toCharArray()) {
                var t = v[c];
                if (t != null) {
                    buffer.append(t);
                } else {
                    buffer.append(c);
                }
            }
            return buffer.toString();
        }

        private String compression(String s) {
            var buffer = new StringBuilder();
            var spaces = 0;
            for (var c : s.toCharArray()) {
                if (c == ' ') {
                    spaces ++;
                } else {
                    buffer.append(format(spaces));
                    buffer.append(c);
                    spaces = 0;
                }
            }
            buffer.append(format(spaces));
            return buffer.toString();
        }

        private String format(int spaces) {
            var buffer = new StringBuilder();
            if (spaces >= 4) {
                buffer.append("&");
                buffer.append(String.format("%02d", spaces));
            } else {
                for (var i = 0; i < spaces; ++i) {
                    buffer.append(" ");
                }
            }
            return buffer.toString();
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
                contd = new CommunicationLine().process(test, in, out);
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