package women.io2017;

import java.io.*;
import java.util.*;
import utils.io.*;

// Problem D
public class WhereYaGonnaCall {
    public void process(int testCase, InputReader in, PrintWriter out) {
        int  n = in.nextInt();
        long g[][] = new long[n][n];
        long f[][] = new long[n][n];
        for (int i = 1; i < n; ++i) {
            for (int j = 0; j < i; ++j) {
                int d = in.nextInt();
                if (d != -1) {
                    g[i][j] = 2 * d;
                    g[j][i] = 2 * d;
                    f[i][j] = 2 * d;
                    f[j][i] = 2 * d;
                }
                else {
                    g[i][j] = -1;
                    g[j][i] = -1;
                    f[i][j] = Long.MAX_VALUE / 2;
                    f[j][i] = Long.MAX_VALUE / 2;
                }
            }
        }
        for (int k = 0; k < n; ++k) {
            for (int i = 0; i < n; ++i) {
                for (int j = 0; j < n; ++j) {
                    f[i][j] = Math.min(f[i][j], f[i][k] + f[k][j]);
                }
            }
        }
        // It can be shown that the optimal location is either one
        // of the nodes or a point on some edge with the property
        // that left and right parts of the edge has integer length
        // or integer length plus 0.5.
        long res = Long.MAX_VALUE;
        if (n <= 30) {
            for (int i = 0; i < n; ++i) {
                for (int j = 0; j < i; ++j) {
                    if (g[i][j] > 0) {
                        res = Math.min(res, get(n, f[i], f[j], g[i][j]));
                    }
                }
            }
        } else {
            // let's check using binary search if it is possible
            // to reach a distance D or less then D.
            long lo = 0, hi = Long.MAX_VALUE / 2;
            while (lo < hi) {
                long x = (lo + hi) / 2;
                if (can(g, f, n, x)) {
                    hi = x;
                } else {
                    lo = x + 1;
                }
            }
            res = lo;
        }
        out.format("Case #%d: %.1f\n", testCase, 0.5 * res);
    }

    private static boolean can(long[][] g, long[][] f, int n, long D) {
        int[] has = new int[n];
        for (int i = 0; i < n; ++i) {
            for (int j = 0; j < i; ++j) {
                if (g[i][j] > 0) {
                    long d = g[i][j];

                    List<Event> ev = new ArrayList<>();
                    for (int k = 0; k < n; ++k) {
                        long x = (f[k][j] + d - f[k][i]) / 2;
                        long a = Math.min(x, +D - f[k][i]);
                        long b = Math.max(x, -D + f[k][j] + d);
                        if (a >= 0) {
                            ev.add(new Event(0, -1, k));
                            ev.add(new Event(a, +1, k));
                        }
                        if (b <= d) {
                            ev.add(new Event(b, -1, k));
                            ev.add(new Event(d, +1, k));
                        }
                    }
                    ev.sort(Comparator.naturalOrder());
                    for (int k = 0, total = 0; k < ev.size(); ++k) {
                        Event e = ev.get(k);
                        if (e.type < 0) {
                            if (has[e.owner] == 0) {
                                ++total;
                            }
                            ++has[e.owner];
                        } else {
                            --has[e.owner];
                            if (has[e.owner] == 0) {
                                --total;
                            }
                        }
                        if (total == n) {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    // The radius of a graph G is the minimum graph eccentricity
    // of any graph vertex in the graph. The eccentricity e(v) of
    // a graph vertex v in a connected graph G is the maximum graph
    // distance between v and any other vertex u of G.
    private static long get(int n, long[] a, long[] b, long d) {
        List<Long> at = new ArrayList<>();
        at.add(0L);
        at.add(d);
        for (int i = 0; i < n; ++i) {
            for (int j = 0; j < n; ++j) {
                long x = (b[j] - a[i] + d) / 2;
                if (0 <= x && x <= d) {
                    at.add(x);
                }
            }
        }
        long radius = Long.MAX_VALUE;
        for (long x : at) {
            long eccentricity = 0;
            for (int i = 0; i < n; ++i) {
                long shortest = Math.min(a[i] + x, b[i] + d - x);
                eccentricity  = Math.max(eccentricity, shortest);
            }
            radius = Math.min(radius, eccentricity);
        }
        return radius;
    }

    private static class Event implements Comparable<Event> {
        public final Long value;
        public final Integer type;
        public final Integer owner;

        public Event(long value, int type, int owner) {
            this.value = value;
            this.type = type;
            this.owner = owner;
        }

        @Override
        public int compareTo(Event o) {
            int sign = value.compareTo(o.value);
            if (sign == 0) {
                return type.compareTo(o.type);
            }
            return sign;
        }
    }
}
