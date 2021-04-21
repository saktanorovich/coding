import java.io.*;
import java.util.*;

public class Main {
    private static final class UrbanElevations {
        public boolean process(int testCase, InputReader in, PrintWriter out) {
            int numOfBuildings = in.nextInt();
            if (numOfBuildings == 0) {
                return false;
            }
            Building buildings[] = new Building[numOfBuildings];
            for (int i = 0; i < numOfBuildings; ++i) {
                buildings[i] = new Building();
                buildings[i].seqno = i + 1;
                buildings[i].xpos = in.nextDouble();
                buildings[i].ypos = in.nextDouble();
                buildings[i].width = in.nextDouble();
                buildings[i].depth = in.nextDouble();
                buildings[i].height = in.nextDouble();
            }
            List<Double> events = new ArrayList<>();
            for (int i = 0; i < numOfBuildings; ++i) {
                events.add(buildings[i].xpos);
                events.add(buildings[i].xpos + buildings[i].width);
            }
            events.sort(Comparator.naturalOrder());
            for (int i = 1; i < events.size(); ++i) {
                double e1 = events.get(i - 1);
                double e2 = events.get(i);
                if (MathUtils.sign(e2 - e1) == 0) {
                    continue;
                }
                List<Building> visible = new ArrayList<>();
                for (Building b : buildings) {
                    double x1 = b.xpos;
                    double x2 = b.xpos + b.width;
                    if (MathUtils.sign(e2 - x1) <= 0) {
                        continue;
                    }
                    if (MathUtils.sign(x2 - e1) <= 0) {
                        continue;
                    }
                    visible.add(b);
                }
                visible.sort(new Comparator<Building>() {
                    @Override
                    public int compare(Building o1, Building o2) {
                        return MathUtils.sign(o1.ypos - o2.ypos);
                    }
                });
                double height = 0;
                for (Building b : visible) {
                    if (MathUtils.sign(b.height - height) <= 0) {
                        continue;
                    }
                    b.visible = true;
                    height = b.height;
                }
            }
            Arrays.sort(buildings, new Comparator<Building>() { 
                @Override
                public int compare(Building o1, Building o2) {
                    if (MathUtils.sign(o1.xpos - o2.xpos) != 0) {
                        return MathUtils.sign(o1.xpos - o2.xpos);
                    } else {
                        return MathUtils.sign(o1.ypos - o2.ypos);
                    }
                }
            });
            if (testCase > 1) {
                out.format("\n");
            }
            out.format("For map #%d, the visible buildings are numbered as follows:\n", testCase);
            int numOfVisible = 0;
            for (Building b : buildings) {
                if (b.visible) {
                    if (numOfVisible > 0) {
                        out.format(" ");
                    }
                    out.format("%d", b.seqno);
                    numOfVisible ++;
                }
            }
            out.format("\n");
            return true;
        }

        private static final class Building {
            public int seqno;
            public double xpos;
            public double ypos;
            public double width;
            public double depth;
            public double height;
            public boolean visible;
        }

        private static final class MathUtils {
            public static final double EPS = 1e-8;

            public static int sign(double x) {
                if (x + EPS < 0) {
                    return -1;
                }
                if (x - EPS > 0) {
                    return +1;
                }
                return 0;
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
            for (int test = 1; contd; ++test) {
                long beg = System.nanoTime();
                contd = new UrbanElevations().process(test, in, out);
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
