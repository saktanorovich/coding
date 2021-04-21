import java.io.*;
import java.util.*;

public class Main {
    private static final class DuckPond {
        public boolean process(int testCase, InputReader in, PrintWriter out) {
            var n = in.nextInt();
            var m = in.nextInt();
            var res = new int[n];
            var pond = new Pond(n);
            for (var i = 0; i < n; ++i) {
                var duck = pond.next(m);
                res[duck] = i;
            }
            for (var i = 0; i < n; ++i) {
                out.print(res[i] + 1);
                if (i + 1 < n) {
                    out.print(" ");
                }
            }
            out.println();
            return false;
        }

        private final class Pond {
            private Node pond;
            private Node head;

            public Pond(int size) {
                for (var i = 0; i < size; ++i) {
                    make(i);
                }
            }

            public int next(int m) {
                while (m > 1) {
                    head = head.next;
                    m = m - 1;
                }
                var next = head.value;
                head = drop(head);
                return next;
            }

            private void make(int value) {
            	var node = new Node();
            	node.value = value;
                if (pond == null) {
                    pond = node;
                    pond.prev = pond;
                    pond.next = pond;
                    head = pond;
                } else {
                    pond.next = node;
                    node.prev = pond;
                    node.next = head;
                    head.prev = node;
                    pond = node;
                }
            }

            private Node drop(Node node) {
                var prev = node.prev;
                var next = node.next;
                prev.next = next;
                next.prev = prev;
                return next;
            }

            private final class Node {
                public Node next;
                public Node prev;
                public int value;
            }
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
                contd = new DuckPond().process(test, in, out);
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