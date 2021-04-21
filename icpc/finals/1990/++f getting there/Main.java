import java.io.*;
import java.util.*;

public class Main {
    private static final class GettingThere {
        public boolean process(int testCase, InputReader in, PrintWriter out) {
            StringTokenizer tokenizer = new StringTokenizer(in.nextLine());
            String travelNm = tokenizer.nextToken();
            String travelId = tokenizer.nextToken();
            assert travelNm.equals("TRAVEL");
            List<Flight> flights = new ArrayList<>();
            while (true) {
                String line = in.nextLine();
                if (line.equals("#")) {
                    break;
                }
                tokenizer = new StringTokenizer(line);
                String beg = Utils.pretty(tokenizer.nextToken());
                String end = Utils.pretty(tokenizer.nextToken());
                int stTime = Utils.parseTime(tokenizer.nextToken());
                int enTime = Utils.parseTime(tokenizer.nextToken());
                double cost = Double.parseDouble(tokenizer.nextToken());
                flights.add(new Flight(beg, end, stTime, enTime, cost));
            }
            if (testCase > 1) {
                out.format("\n\n");
            }
            out.format("Requests and optimal routes for travel %d\n", Integer.parseInt(travelId));
            out.format("------------------------------------------\n");
            TravelOptimizer optimizer = new TravelOptimizer(flights);
            while (true) {
                String line = in.nextLine();
                if (line.equals("#")) {
                    break;
                }
                out.format("\n");
                tokenizer = new StringTokenizer(line);
                String beg = Utils.pretty(tokenizer.nextToken());
                String end = Utils.pretty(tokenizer.nextToken());
                String typ = tokenizer.nextToken();
                Travel travel = null;
                switch (typ) {
                    case "TIME":
                        travel = optimizer.optimizeByTime(beg, end);
                        break;
                    case "COST":
                        travel = optimizer.optimizeByCost(beg, end);
                        break;
                    default:
                        throw new RuntimeException();
                }
                if (travel == null) {
                    out.format("There is no route from %s to %s.\n", beg, end);
                } else if (travel.empty()) {
                    out.format("You are already in %s.\n", beg);
                } else {
                    out.format("From: %-20s To: %-20s Optimize: %s\n", beg, end, Utils.pretty(typ));
                    out.format("==================================================================\n");
                    out.format("%-19s %-22s %-7s %-6s %8s\n", "From", "To", "Leave", "Arrive", "Cost");
                    out.format("==================================================================\n");
                    for (Flight f : travel.flights) {
                        out.format("%-19s %-22s %-7s %-6s %8s\n", f.beg, f.end,
                            Utils.toTimeString(f.stTime),
                            Utils.toTimeString(f.enTime),
                            Utils.toCostString(f.cost));
                    }
                    out.format("%66s\n", "-----------------------");
                    out.format("%57s %8s\n", Utils.toSpanString(travel.span()), Utils.toCostString(travel.cost()));
                }
            }
            return true;
        }

        private static class Flight {
            public final String beg;
            public final String end;
            public final double cost;
            public final int stTime;
            public final int enTime;

            public Flight(String beg, String end, int stTime, int enTime, double cost) {
                this.beg = beg;
                this.end = end;
                this.cost = cost;
                this.stTime = stTime;
                this.enTime = enTime;
            }
        }

        private static class Travel {
            public final List<Flight> flights;

            public Travel(List<Flight> flights) {
                this.flights = flights;
            }

            public boolean empty() {
                return flights == null || flights.size() == 0;
            }

            public int span() {
                List<Integer> time = new ArrayList<>();
                for (Flight f : flights) {
                    time.add(f.stTime);
                    time.add(f.enTime);
                }
                int res = 0;
                for (int i = 1; i < time.size(); ++i) {
                    res += Utils.toSpan(time.get(i - 1), time.get(i));
                }
                return res;
            }

            public double cost() {
                double res = 0;
                for (Flight f : flights) {
                    res += f.cost;
                }
                return res;
            }
        }

        private static class TravelPoint {
            public final int city;
            public final int time;

            public TravelPoint(int city, int time) {
                this.city = city;
                this.time = time;
            }
        }

        private static class TravelOptimizer {
            private final Map<Integer, List<Flight>> flights;
            private final Map<String, Integer> cities;

            public TravelOptimizer(List<Flight> flights) {
                this.flights = new HashMap<>();
                this.cities = new HashMap<>();
                for (Flight f : flights) {
                    this.cities.putIfAbsent(f.beg, this.cities.size());
                    this.cities.putIfAbsent(f.end, this.cities.size());
                    int beg = this.cities.get(f.beg);
                    int end = this.cities.get(f.end);
                    this.flights.putIfAbsent(beg, new ArrayList<>());
                    this.flights.putIfAbsent(end, new ArrayList<>());
                    this.flights.get(beg).add(f);
                }
            }

            public Travel optimizeByTime(String beg, String end) {
                if (beg.equals(end)) {
                    return new Travel(null);
                }
                if (cities.containsKey(beg) == false) return null;
                if (cities.containsKey(end) == false) return null;

                int source = cities.get(beg);
                int target = cities.get(end);
                Program dp = new Program(cities);
                Queue<TravelPoint> q = new LinkedList<>();
                for (Flight f : flights.get(source)) {
                    dp.cost[source][f.stTime] = 0;
                    dp.time[source][f.stTime] = 0;
                    q.add(new TravelPoint(source, f.stTime));
                }
                while (q.isEmpty() == false) {
                    TravelPoint point = q.poll();
                    int currCity = point.city;
                    int currTime = point.time;
                    for (Flight f : flights.get(currCity)) {
                        if (dp.relaxTime(currCity, currTime, f)) {
                            q.add(new TravelPoint(cities.get(f.end), f.enTime));
                        }
                    }
                }
                int time = dp.getBestTimeIndex(target);
                if (time == -1) {
                    return null;
                }
                return dp.traverse(source, target, time);
            }

            public Travel optimizeByCost(String beg, String end) {
                if (beg.equals(end)) {
                    return new Travel(null);
                }
                if (cities.containsKey(beg) == false) return null;
                if (cities.containsKey(end) == false) return null;

                int source = cities.get(beg);
                int target = cities.get(end);
                Program dp = new Program(cities);
                Queue<TravelPoint> q = new LinkedList<>();
                for (Flight f : flights.get(source)) {
                    dp.cost[source][f.stTime] = 0;
                    dp.time[source][f.stTime] = 0;
                    q.add(new TravelPoint(source, f.stTime));
                }
                while (q.isEmpty() == false) {
                    TravelPoint point = q.poll();
                    int currCity = point.city;
                    int currTime = point.time;
                    for (Flight f : flights.get(currCity)) {
                        if (dp.relaxCost(currCity, currTime, f)) {
                            q.add(new TravelPoint(cities.get(f.end), f.enTime));
                        }
                    }
                }
                int time = dp.getBestCostIndex(target);
                if (time == -1) {
                    return null;
                }
                return dp.traverse(source, target, time);
            }

            private static final class Program {
                public final Map<String, Integer> cities;
                public final double[][] cost;
                public final int[][] time;
                public final int[][] prev;
                public final Flight[][] what;

                public Program(Map<String, Integer> cities) {
                    this.cities = cities;
                    this.cost = new double[cities.size()][];
                    this.time = new int[cities.size()][];
                    this.prev = new int[cities.size()][];
                    this.what = new Flight[cities.size()][];
                    for (int city = 0; city < cities.size(); ++city) {
                        cost[city] = new double[24 * 60];
                        time[city] = new int[24 * 60];
                        prev[city] = new int[24 * 60];
                        what[city] = new Flight[24 * 60];
                        Arrays.fill(cost[city], Integer.MAX_VALUE / 2);
                        Arrays.fill(time[city], Integer.MAX_VALUE / 2);
                    }
                }

                public boolean relaxCost(int currCity, int currTime, Flight f) {
                    int nextCity = cities.get(f.end);
                    int nextTime = f.enTime;
                    int timeSpan = Utils.toSpan(currTime, f.stTime) + Utils.toSpan(f.stTime, f.enTime);
                    if (cost[nextCity][nextTime] > cost[currCity][currTime] + f.cost) {
                        cost[nextCity][nextTime] = cost[currCity][currTime] + f.cost;
                        time[nextCity][nextTime] = time[currCity][currTime] + timeSpan;
                        prev[nextCity][nextTime] = currTime;
                        what[nextCity][nextTime] = f;
                        return true;
                    }
                    if (cost[nextCity][nextTime] == cost[currCity][currTime] + f.cost) {
                        if (time[nextCity][nextTime] > time[currCity][currTime] + timeSpan) {
                            time[nextCity][nextTime] = time[currCity][currTime] + timeSpan;
                            prev[nextCity][nextTime] = currTime;
                            what[nextCity][nextTime] = f;
                            return true;
                        }
                    }
                    return false;
                }

                public boolean relaxTime(int currCity, int currTime, Flight f) {
                    int nextCity = cities.get(f.end);
                    int nextTime = f.enTime;
                    int timeSpan = Utils.toSpan(currTime, f.stTime) + Utils.toSpan(f.stTime, f.enTime);
                    if (time[nextCity][nextTime] > time[currCity][currTime] + timeSpan) {
                        time[nextCity][nextTime] = time[currCity][currTime] + timeSpan;
                        cost[nextCity][nextTime] = cost[currCity][currTime] + f.cost;
                        prev[nextCity][nextTime] = currTime;
                        what[nextCity][nextTime] = f;
                        return true;
                    }
                    if (time[nextCity][nextTime] == time[currCity][currTime] + timeSpan) {
                        if (cost[nextCity][nextTime] > cost[currCity][currTime] + f.cost) {
                            cost[nextCity][nextTime] = cost[currCity][currTime] + f.cost;
                            prev[nextCity][nextTime] = currTime;
                            what[nextCity][nextTime] = f;
                            return true;
                        }
                    }
                    return false;
                }

                public int getBestCostIndex(int city) {
                    double bestCost = Integer.MAX_VALUE / 2;
                    int bestTime = Integer.MAX_VALUE / 2;
                    int bestIndx = -1;
                    for (int t = 0; t < 24 * 60; ++t) {
                        double currCost = cost[city][t];
                        int currTime = time[city][t];
                        if (currCost < Integer.MAX_VALUE / 2) {
                            if (bestCost > currCost || (bestCost == currCost && bestTime > currTime)) {
                                bestCost = currCost;
                                bestTime = currTime;
                                bestIndx = t;
                            }
                        }
                    }
                    return bestIndx;
                }

                public int getBestTimeIndex(int city) {
                    double bestCost = Integer.MAX_VALUE / 2;
                    int bestTime = Integer.MAX_VALUE / 2;
                    int bestIndx = -1;
                    for (int t = 0; t < 24 * 60; ++t) {
                        int currTime = time[city][t];
                        double currCost = cost[city][t];
                        if (currTime < Integer.MAX_VALUE / 2) {
                            if (bestTime > currTime || (bestTime == currTime && bestCost > currCost)) {
                                bestTime = currTime;
                                bestCost = currCost;
                                bestIndx = t;
                            }
                        }
                    }
                    return bestIndx;
                }

                public Travel traverse(int source, int target, int indx) {
                    List<Flight> travel = new ArrayList<>();
                    for (int city = target; city != source;) {
                        int temp = indx;
                        travel.add(what[city][temp]);
                        indx = prev[city][temp];
                        city = cities.get(what[city][temp].beg);
                    }
                    Collections.reverse(travel);
                    return new Travel(travel);
                }
            }
        }

        private static class Utils {
            public static int parseTime(String s) {
                String[] t = s.split(":");
                int hh = Integer.parseInt(t[0]);
                int mm = Integer.parseInt(t[1].substring(0, t[1].length() - 1));
                switch (s.charAt(s.length() - 1)) {
                    case 'A':
                        return mm + 60 * (hh);
                    case 'P':
                        return mm + 60 * (hh + 12);
                    default:
                        throw new RuntimeException();
                }
            }

            public static String toTimeString(int time) {
                int hh = time / 60;
                int mm = time % 60;
                if (hh >= 12) {
                    hh -= 12;
                    return String.format("%d:%02dP", hh, mm);
                } else {
                    return String.format("%d:%02dA", hh, mm);
                }
            }

            public static String toSpanString(int span) {
                StringBuilder res = new StringBuilder();
                int days = span / (60 * 24);
                if (days > 0) {
                    res.append(days);
                    if (days == 1) {
                        res.append(" day");
                    } else {
                        res.append(" days");
                    }
                }
                span %= 60 * 24;
                int hh = span / 60;
                int mm = span % 60;
                res.append(String.format(" %d:%02d", hh, mm));
                return res.toString();
            }

            public static String toCostString(double cost) {
                return String.format("$%.2f", cost);
            }

            public static int toSpan(int time1, int time2) {
                if (time1 <= time2) {
                    return time2 - time1;
                } else {
                    return time2 - time1 + 24 * 60;
                }
            }

            public static String pretty(String s) {
                StringBuilder builder = new StringBuilder(s.toLowerCase());
                builder.setCharAt(0, Character.toUpperCase(s.charAt(0)));
                return builder.toString();
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
            for (int test = 1; in.hasNext(); ++test) {
                long beg = System.nanoTime();
                contd = new GettingThere().process(test, in, out);
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
        private String nextLine;

        public InputReader(InputStream input) {
            reader = new BufferedReader(new InputStreamReader(input), 32768);
        }

        public boolean hasNext() {
            try {
                nextLine = reader.readLine();
                if (nextLine != null) {
                    return true;
                }
            } catch (IOException e) {
                e.printStackTrace();
            }
            return false;
        }

        public String nextLine() {
            if (nextLine == null) {
                hasNext();
            }
            String result = nextLine;
            nextLine = null;
            return result;
        }

        public void close() throws IOException {
            reader.close();
            reader = null;
        }
    }
}
