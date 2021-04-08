package gcj2008.semiEMEA;

import utils.io.*;
import java.io.*;
import java.util.*;

// Problem B
public class PaintingFence {
    public void process(int testCase, InputReader in, PrintWriter out) {
        this.n = in.nextInt();
        this.offers = new HashMap<>();
        this.colors = new ArrayList<>();
        for (int i = 0; i < n; ++i) {
            String color = in.next();
            if (offers.containsKey(color) == false) {
                offers.put(color, new ArrayList<>());
                colors.add(color);
            }
            int A = in.nextInt();
            int B = in.nextInt();
            offers.get(color).add(new Offer(A, B));
        }
        int res = Integer.MAX_VALUE / 2;
        for (int i = 0; i < colors.size(); ++i) {
            res = Math.min(res, get(i));
            for (int j = i + 1; j < colors.size(); ++j) {
                res = Math.min(res, get(i, j));
                for (int k = j + 1; k < colors.size(); ++k) {
                    res = Math.min(res, get(i, j, k));
                }
            }

        }
        if (res < Integer.MAX_VALUE / 2) {
            out.format("Case #%d: %d\n", testCase, res);
        } else {
            out.format("Case #%d: %s\n", testCase, "IMPOSSIBLE");
        }
    }

    private int get(int... colors) {
        List<Offer> o = new ArrayList<>();
        for (int c : colors) {
            o.addAll(offers.get(this.colors.get(c)));
        }
        return get(o.toArray(new Offer[o.size()]));
    }

    private static int get(Offer[] o) {
        Arrays.sort(o);
        int[] f = new int[o.length];
        for (int i = 0; i < o.length; ++i) {
            f[i] = Integer.MAX_VALUE / 2;
            if (o[i].has(1)) {
                f[i] = 1;
            }
        }
        int res = Integer.MAX_VALUE / 2;
        for (int i = 0; i < o.length; ++i) {
            for (int j = i - 1; j >= 0; --j) {
                if (o[i].A <= o[j].B + 1) {
                    f[i] = Math.min(f[i], f[j] + 1);
                } else {
                    break;
                }
            }
            if (o[i].has(MAX)) {
                res = Math.min(res, f[i]);
            }
        }
        return res;
    }

    private Map<String, List<Offer>> offers;
    private List<String> colors;
    private int n;

    private static final int MAX = (int)1e4;

    private static class Offer implements Comparable<Offer> {
        public final int A;
        public final int B;

        public Offer(int A, int B) {
            this.A = A;
            this.B = B;
        }

        public boolean has(int x) {
            return A <= x && x <= B;
        }

        @Override
        public int compareTo(Offer o) {
            if (B != o.B) {
                return B - o.B;
            }
            return A - o.A;
        }
    }
}
