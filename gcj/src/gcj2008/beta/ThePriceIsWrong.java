package gcj2008.beta;

import java.io.*;
import java.util.*;
import utils.io.*;

// Problem B
public class ThePriceIsWrong {
    public void process(int testCase, InputReader in, PrintWriter out) {
        String s[] = in.nextLine().split(" ");
        String c[] = in.nextLine().split(" ");
        out.format("Case #%d:%s\n", testCase, get(Item.parse(s, c)));
    }

    private static String get(List<Item> set) {
        set.sort(Comparator.comparing(a -> a.name));
        int have = lis(set, 0);
        int goal = set.size() - have;
        long skip = 0;
        for (int i = 0; goal > 0; ++i) {
            skip ^= 1L << i;
            if (lis(set, skip) == have) {
                --goal;
                continue;
            }
            skip ^= 1L << i;
        }
        StringBuilder res = new StringBuilder();
        for (int i = 0; i < set.size(); ++i) {
            if (has(skip, i)) {
                res.append(" " + set.get(i).name);
            }
        }
        return res.toString();
    }

    private static int lis(List<Item> set, long skip) {
        List<Item> now = new ArrayList<>();
        for (int i = 0; i < set.size(); ++i) {
            if (has(skip, i) == false) {
                now.add(set.get(i));
            }
        }
        now.sort(Comparator.comparing(a -> a.indx));
        int lis[] = new int[now.size()];
        int res = 1;
        for (int i = 0; i < now.size(); ++i) {
            lis[i] = 1;
            for (int j = i - 1; j >= 0; --j) {
                if (now.get(i).cost > now.get(j).cost) {
                    if (lis[i] < lis[j] + 1) {
                        lis[i] = lis[j] + 1;
                    }
                }
            }
            res = Math.max(res, lis[i]);
        }
        return res;
    }

    private static boolean has(long set, int x) {
        return (set & (1L << x)) != 0;
    }

    private static final class Item {
        public final String name;
        public final int cost;
        public final int indx;

        public Item(String name, int cost, int indx) {
            this.name = name;
            this.cost = cost;
            this.indx = indx;
        }

        public static List<Item> parse(String[] s, String[] c) {
            List<Item> res = new ArrayList<>();
            for (int i = 0; i < s.length; ++i) {
                res.add(new Item(s[i], Integer.parseInt(c[i]), i));
            }
            return res;
        }
    }
}
