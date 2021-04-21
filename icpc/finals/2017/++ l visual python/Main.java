import java.io.*;
import java.util.*;

public class Main {
    private static final class VisualPython {
        public void process(int testCase, InputReader in, PrintWriter out) {
            int n = in.nextInt();
            List<Point> p = new ArrayList<>();
            for (int k = 0, ind = 0; k < 2; ++k) {
                for (int i = 0; i < n; ++i, ++ind) {
                    int row = in.nextInt();
                    int col = in.nextInt();
                    p.add(new Point(ind, row, col));
                }
            }
            int[] res = make(p, n, bind(p, n));
            if (res != null) {
                for (int i = 0; i < n; ++i) {
                    out.println(res[i] - n + 1);
                }
            } else {
                out.println("syntax error");
            }
        }

        private int[] make(List<Point> ps, int n, int[] match) {
            Collections.sort(ps, (a, b) -> a.ind - b.ind);
            if (match != null) {
                List<Event> es = new ArrayList<>();
                for (int i = 0; i < n; ++i) {
                    Point a = ps.get(i);
                    Point b = ps.get(match[i]);
                    es.add(new Event(a.row, b.row, a.col, 0));
                    es.add(new Event(a.row, b.row, b.col, 1));
                }
                Collections.sort(es, (a, b) -> {
                    if (a.ecol != b.ecol) {
                        return a.ecol - b.ecol;
                    }
                    if (a.type != b.type) {
                        return a.type - b.type;
                    }
                    if (a.row1 != b.row1) {
                        return a.row1 - b.row1;
                    }
                    return a.row2 - b.row2;
                });
                Store s = new Store();
                for (Event e : es) {
                    if (e.type == 0) {
                        if (s.push(e) == false) return null;
                    } else {
                        if (s.pull(e) == false) return null;
                    }
                }
                return match;
            }
            return null;
        }

        private int[] bind(List<Point> ps, int n) {
            Collections.sort(ps, (a, b) -> {
                if (a.col != b.col) {
                    return a.col - b.col;
                }
                return a.row - b.row;
            });
            TreeSet<Point> corners = new TreeSet<>((a, b) -> {
                return a.row - b.row;
            });
            int[] res = new int[n];
            for (Point p : ps) {
                if (p.ind < n) {
                    corners.add(p);
                    continue;
                }
                // let's find the nearest corner using the following approach
                //   for (Point c : corners) {
                //       if (c.row <= p.row) {
                //           if (m == null || c.row > m.row) {
                //               m = c;
                //           }
                //       }
                //   }
                Point m = corners.lower(new Point(p.ind, p.row + 1, p.col));
                if (m != null) {
                    corners.remove(m);
                    res[m.ind] = p.ind;
                } else {
                    return null;
                }
            }
            return res;
        }

        private class Store {
            private final TreeSet<Integer> corners;
            private final TreeSet<Event> current;
            private Event last;

            public Store() {
                this.corners = new TreeSet<>();
                this.current = new TreeSet<>((a, b) -> a.row1 - b.row1);
                this.last = null;
            }

            public boolean push(Event e) {
                if (cover(e)) {
                    return false;
                }
                if (last != null && last.ecol == e.ecol) {
                    Event z = current.floor(e);
                    if (z != null) {
                        if (z.row2 >= e.row2) {
                            return false;
                        }
                    }
                } else {
                    current.clear();
                }
                corners.add(e.row1);
                corners.add(e.row2);
                current.add(e);
                last = e;
                return true;
            }

            public boolean pull(Event e) {
                corners.remove(e.row1);
                corners.remove(e.row2);
                Event z = current.ceiling(e);
                if (z != null) {
                    current.remove(z);
                }
                if (cover(e)) {
                    return false;
                }
                return true;
            }

            private boolean cover(Event e) {
                Integer corner = corners.ceiling(e.row1);
                if (corner != null) {
                    if (corner <= e.row2) {
                        return true;
                    }
                }
                return false;
            }
        }

        private class Event {
            public final int row1;
            public final int row2;
            public final int ecol;
            public final int type;

            public Event(int row1, int row2, int ecol, int type) {
                this.row1 = row1;
                this.row2 = row2;
                this.ecol = ecol;
                this.type = type;
            }
        }

        private class Point {
            public final int ind;
            public final int row;
            public final int col;

            public Point(int ind, int row, int col) {
                this.ind = ind;
                this.row = row;
                this.col = col;
            }
        }
    }

    public static void main(String[] args) throws Exception {
        //InputReader in = new InputReader(new FileInputStream("input.txt"));
        //PrintWriter out = new PrintWriter(new FileOutputStream("output.txt"));
        InputReader in = new InputReader(System.in);
        PrintWriter out = new PrintWriter(System.out);

        System.err.println("Test Case: Elapsed time");
        for (int test = 1; in.hasNext(); ++test) {
            long beg = System.nanoTime();
            new VisualPython().process(test, in, out);
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
