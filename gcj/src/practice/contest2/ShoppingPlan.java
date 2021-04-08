package practice.contest2;

import java.io.*;
import java.util.*;
import utils.io.*;

// Problem D
public class ShoppingPlan {
    public void process(int testCase, InputReader in, PrintWriter out) {
        this.numOfItems = in.nextInt();
        this.numOfStore = in.nextInt();
        this.priceOfGas = in.nextInt();
        this.perishable = 0;
        Map<String, Integer> map = new HashMap<>();
        for (int i = 0; i < numOfItems; ++i) {
            String item = in.next();
            if (item.endsWith("!")) {
                item = item.substring(0, item.length() - 1);
                perishable |= 1 << i;
            }
            map.put(item, i);
        }
        this.stores = new Store[numOfStore + 1];
        for (int i = 1; i <= numOfStore; ++i) {
            String[] s = in.nextLine().split(" ");
            int x = Integer.parseInt(s[0]);
            int y = Integer.parseInt(s[1]);
            stores[i] = new Store(x, y, numOfItems);
            for (int k = 2; k < s.length; ++k) {
                String[] e = s[k].split(":");
                stores[i].add(map.get(e[0]), Integer.parseInt(e[1]));
            }
            stores[i].init();
        }
        this.stores[0] = new Store(0, 0, 0);
        memo = new double[numOfStore + 1][2][1 << numOfItems];
        for (int i = 0; i <= numOfStore; ++i) {
            Arrays.fill(memo[i][0], -1);
            Arrays.fill(memo[i][1], -1);
        }
        for (int i = 1; i <= numOfStore; ++i) {
            memo[i][0][0] = cost(i, 0);
            memo[i][1][0] = cost(i, 0);
        }
        memo[0][0][0] = 0;
        memo[0][1][0] = 0;
        out.format("Case #%d: %.8f\n", testCase, get((1 << numOfItems) - 1, 0, 0));
    }

    private double get(int set, int s, int bought) {
        if (memo[s][bought][set] == -1) {
            memo[s][bought][set] = Double.MAX_VALUE / 2;
            if (s == 0 || bought > 0) {
                for (int k = 1; k <= numOfStore; ++k) {
                    if (s == k) {
                        continue;
                    }
                    memo[s][bought][set] = Math.min(memo[s][bought][set], cost(s, k) + get(set, k, 0));
                }
            } else {
                int has = stores[s].mask & set;
                for (int sub = has; sub > 0; sub = (sub - 1) & has) {
                    double value;
                    if ((perishable & sub) != 0) {
                        value = stores[s].buy(sub) + get(set ^ sub, 0, 0) + cost(s, 0);
                    } else {
                        value = stores[s].buy(sub) + get(set ^ sub, s, 1);
                    }
                    memo[s][bought][set] = Math.min(memo[s][bought][set], value);
                }
            }
        }
        return memo[s][bought][set];
    }

    private double cost(int a, int b) {
        if (a != b) {
            double x = stores[a].x - stores[b].x;
            double y = stores[a].y - stores[b].y;
            double d = Math.sqrt(x * x + y * y);
            return priceOfGas * d;
        }
        return 0;
    }

    private int numOfItems;
    private int numOfStore;
    private int priceOfGas;
    private int perishable;
    private Store[] stores;
    private double[][][] memo;

    private static class Store {
        public final int x;
        public final int y;
        public int mask;
        public int cost[];

        public Store(int x, int y, int n) {
            this.x = x;
            this.y = y;
            this.mask = 0;
            this.cost = new int[1 << n];
            for (int i = 1; i < 1 << n; ++i) {
                cost[i] = Integer.MAX_VALUE;
            }
        }

        public void init() {
            for (int set = 1; set < cost.length; ++set) {
                cost[set] = cost[set & (set - 1)] + cost[set & (-set)];
            }
        }

        public void add(int item, int cost) {
            int set = 1 << item;
            this.mask |= set;
            this.cost[set] = cost;
        }

        public boolean has(int set) {
            return (mask & set) == set;
        }

        public int buy(int set) {
            return cost[set];
        }
    }
}
