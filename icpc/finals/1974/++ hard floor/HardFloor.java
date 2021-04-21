import java.io.*;
import java.util.*;

public class Main {
    private static final class HardFloor {
        public boolean process(int testCase, InputReader in, PrintWriter out) {
            var n = in.nextInt();
            var curr = new Point();
            var poly = new ArrayList<Point>();
            poly.add(curr);
            for (var i = 0; i < n; ++i) {
                var term = in.next();
                var vect = term.charAt(0);
                var dist = Integer.parseInt(term.substring(1));
                var next = new Point();
                next.x = curr.x;
                next.y = curr.y;
                switch (vect) {
                    case 'N': next.y += dist; break;
                    case 'E': next.x += dist; break;
                    case 'S': next.y -= dist; break;
                    case 'W': next.x -= dist; break;
                }
                poly.add(next);
                curr = next;
            }
            var area = calc(poly);
            out.format("THE AREA IS %d", area);
            out.println();
            return false;
        }

        private int calc (ArrayList<Point> p) {
            var res = 0;
            for (var i = 0; i + 1 < p.size(); ++i) {
                var a = p.get(i);
                var b = p.get(i + 1);
                var dx = b.x - a.x;
                var dy = b.y + a.y;
                res += dx * dy;
            }
            return Math.abs(res) / 2;
        }

        private final class Point {
            public int x;
            public int y;
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
                contd = new HardFloor().process(test, in, out);
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