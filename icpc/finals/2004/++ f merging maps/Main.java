import java.io.*;
import java.util.*;

public class Main {
    private static final class MergingMaps {
        public boolean process(int testCase, InputReader in, PrintWriter out) throws Exception {
            int n = in.nextInt();
            if (n == 0) {
                return false;
            }
            MapFactory factory = new MapFactory();
            List<Map> maps = new ArrayList<>();
            for (int i = 0; i < n; ++i) {
                maps.add(factory.load(in));
            }
            while (true) {
                MapUnion res = null;
                for (Map map1 : maps) {
                    for (Map map2 : maps) {
                        if (map1.id != map2.id) {
                            MapUnion now = new MapUnion(map1, map2);
                            if (now.score() > 0) {
                                if (res == null || res.score() < now.score()) {
                                    res = now;
                                }
                            }
                        }
                    }
                }
                if (res != null) {
                    maps.remove(res.map1);
                    maps.remove(res.map2);
                    Map map = res.getMap(factory);
                    maps.add(map);
                } else {
                    break;
                }
            }
            if (testCase > 1) {
                out.println();
                out.format("Case %d\n", testCase);
            } else {
                out.format("Case %d\n", testCase);
            }
            for (int i = 0; i < maps.size(); ++i) {
                out.print(maps.get(i).toString(4));
                if (i + 1 < maps.size()) {
                    out.println();
                }
            }
            return true;
        }

        private static class Map {
            public final int f[][];
            public final int id;
            public final int n;
            public final int m;

            public Map(int id, int[][] f, int n, int m) {
                this.id = id;
                this.f = f;
                this.n = n;
                this.m = m;
            }

            public String toString(int padding) {
                String offset = pad(padding, " ");
                StringBuilder result = new StringBuilder();
                result.append(offset);
                result.append(String.format("MAP %d:\n", id));
                result.append(offset);
                result.append("+");
                result.append(pad(m, "-"));
                result.append("+\n");
                for (int i = 0; i < n; ++i) {
                    result.append(offset);
                    result.append("|");
                    for (int j = 0; j < m; ++j) {
                        result.append(ABC.charAt(f[i][j]));
                    }
                    result.append("|");
                    result.append("\n");
                }
                result.append(offset);
                result.append("+");
                result.append(pad(m, "-"));
                result.append("+\n");
                return result.toString();
            }

            private static String pad(int length, String s) {
                StringBuilder res = new StringBuilder();
                for (int i = 0; i < length; ++i) {
                    res.append(s);
                }
                return res.toString();
            }
        }

        private static class MapUnion {
            public final Map map1;
            public final Map map2;
            private boolean ready;
            private int value;
            private int row;
            private int col;
            private int f[][];
            private int n;
            private int m;
        
            public MapUnion(Map map1, Map map2) {
                this.map1 = map1;
                this.map2 = map2;
            }

            public int score() {
                if (ready) {
                    return value;
                }
                int rmin = -map2.n + 1;
                int rmax = +map1.n;
                int cmin = -map2.m + 1;
                int cmax = +map1.m;
                value = 0;
                for (int r = rmin; r < rmax; ++r) {
                    for (int c = cmin; c < cmax; ++c) {
                        int cost = 0;
                        int imin = Math.max(0, r);
                        int imax = Math.min(map1.n, r + map2.n);
                        int jmin = Math.max(0, c);
                        int jmax = Math.min(map1.m, c + map2.m);
                        scan:
                        for (int i1 = imin; i1 < imax; ++i1) {
                            for (int j1 = jmin; j1 < jmax; ++j1) {
                                int i2 = i1 - r;
                                int j2 = j1 - c;
                                assert 0 <= i1 && i1 < map1.n;
                                assert 0 <= j1 && j1 < map1.m;
                                assert 0 <= i2 && i2 < map2.n;
                                assert 0 <= j2 && j2 < map2.m;
                                if (map1.f[i1][j1] == 0) {
                                    continue;
                                }
                                if (map2.f[i2][j2] == 0) {
                                    continue;
                                }
                                if (map1.f[i1][j1] == map2.f[i2][j2]) {
                                    cost = cost + 1;
                                } else {
                                    cost = 0;
                                    break scan;
                                }
                            }
                        }
                        if (value < cost) {
                            value = cost;
                            row = r;
                            col = c;
                        }
                    }
                }
                ready = true;
                return value;
            }

            public Map getMap(MapFactory factory) {
                if (score() > 0) {
                    int rmin = Math.min(row, 0);
                    int rmax = Math.max(map1.n, row + map2.n);
                    int cmin = Math.min(col, 0);
                    int cmax = Math.max(map1.m, col + map2.m);
                    int n = rmax - rmin;
                    int m = cmax - cmin;
                    int f[][] = new int[n][m];
                    int di1 = 0;
                    int dj1 = 0;
                    int di2 = 0;
                    int dj2 = 0;
                    if (row < 0) {
                        di1 = -row;
                    } else {
                        di2 = +row;
                    }
                    if (col < 0) {
                        dj1 = -col;
                    } else {
                        dj2 = +col;
                    }
                    Map res = factory.make(f, n, m);
                    copy(map1, res, di1, dj1);
                    copy(map2, res, di2, dj2);
                    return res;
                }
                return null;
            }

            private static void copy(Map src, Map dst, int di, int dj) {
                for (int i = 0; i < src.n; ++i) {
                    for (int j = 0; j < src.m; ++j) {
                        if (src.f[i][j] > 0) {
                            dst.f[i + di][j + dj] = src.f[i][j];
                        }
                    }
                }
            }
        }

        private static class MapFactory {
            private int ID = 0;

            public Map load(InputReader in) {
                int n = in.nextInt();
                int m = in.nextInt();
                int f[][] = new int[n][m];
                for (int i = 0; i < n; ++i) {
                    String s = in.nextLine();
                    for (int j = 0; j < m; ++j) {
                        f[i][j] = ABC.indexOf(s.charAt(j));
                    }
                }
                return make(f, n, m);
            }

            public Map make(int[][] f, int n, int m) {
                return new Map(++ID, f, n, m);
            }
        }

        public void generate(PrintWriter out, int testCases) {
            Random rand = new Random(50847534);
            for (int testCase = 1; testCase <= testCases; ++testCase) {
                int k = rand.nextInt(10) + 1;
                if (k < 2) {
                    k = 2;
                }
                out.println(k);
                int n = rand.nextInt(100) + 1;
                int m = rand.nextInt(100) + 1;
                if (n < 80) {
                    n = 80;
                }
                if (m < 80) {
                    m = 80;
                }
                int f[][] = new int[n][m];
                for (int i = 0; i < n; ++i) {
                    for (int j = 0; j < m; ++j) {
                        f[i][j] = rand.nextInt(27);
                    }
                }
                for (int t = 0; t < k; ++t) {
                    int nt = rand.nextInt(10) + 1;
                    int mt = rand.nextInt(10) + 1;
                    int rt = rand.nextInt(n);
                    int ct = rand.nextInt(m);
                    if (rt + nt > n) {
                        rt = n - nt;
                    }
                    if (ct + mt > m) {
                        ct = m - mt;
                    }
                    out.println(nt + " " + mt);
                    for (int i = 0; i < nt; ++i) {
                        for (int j = 0; j < mt; ++j) {
                            out.print(ABC.charAt(f[i + rt][j + ct]));
                        }
                        out.println();
                    }
                }
            }
        }

        private static final String ABC = "-ABCDEFGHIJKLMNOPQRSTUVWXYZ";
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
            contd = new MergingMaps().process(test, in, out);
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
