package gcj2008.beta;

import utils.io.*;
import java.io.*;
import java.util.*;

// Problem C
public class RandomRoute {
    public void process(int testCase, InputReader in, PrintWriter out) {
        this.roadsNum = in.nextInt();
        this.nodes = new HashMap<>();
        this.nodesNum = 0;
        this.me = add(in.next());
        this.source = new int[roadsNum];
        this.target = new int[roadsNum];
        this.length = new int[roadsNum];
        for (int i = 0; i < roadsNum; ++i) {
            source[i] = add(in.next());
            target[i] = add(in.next());
            length[i] = in.nextInt();
        }
        this.graph = new long[nodesNum][nodesNum];
        this.dists = new long[nodesNum][nodesNum];
        this.paths = new long[nodesNum][nodesNum];
        for (int i = 0; i < nodesNum; ++i) {
            for (int j = 0; j < nodesNum; ++j) {
                graph[i][j] = (int)1e8;
                dists[i][j] = (int)1e8;
                paths[i][j] = -1;
            }
            graph[i][i] = 0;
            dists[i][i] = 0;
            paths[i][i] = 1;
        }
        for (int i = 0; i < roadsNum; ++i) {
            int s = source[i];
            int t = target[i];
            int l = length[i];
            graph[s][t] = Math.min(graph[s][t], l);
            dists[s][t] = graph[s][t];
        }
        for (int k = 0; k < nodesNum; ++k) {
            for (int i = 0; i < nodesNum; ++i) {
                for (int j = 0; j < nodesNum; ++j) {
                    dists[i][j] = Math.min(dists[i][j], dists[i][k] + dists[k][j]);
                }
            }
        }
        double[] prob = new double[roadsNum];
        int driveTo = 0;
        for (int to = 0; to < nodesNum; ++to) {
            if (dists[me][to] < (int)1e6 && me != to) {
                for (int i = 0; i < roadsNum; ++i) {
                    int ss = source[i];
                    int dd = target[i];
                    int ln = length[i];
                    if (graph[ss][dd] == ln) {
                        if (dists[me][ss] + ln + dists[dd][to] == dists[me][to]) {
                            long a = get(me, ss);
                            long b = get(dd, to);
                            long c = get(me, to);
                            prob[i] += 1.0 * (a * b) / c;
                        }
                    }
                }
                driveTo = driveTo + 1;
            }
        }
        for (int i = 0; i < roadsNum; ++i) {
            prob[i] /= 1.0 * driveTo;
        }
        out.format("Case #%d:", testCase);
        for (int i = 0; i < roadsNum; ++i) {
            out.format(" %.8f", prob[i]);
        }
        out.println();
    }

    private long get(int a, int b) {
        if (paths[a][b] == -1) {
            paths[a][b] = 0;
            for (int i = 0; i < roadsNum; ++i) {
                int s = source[i];
                int t = target[i];
                int l = length[i];
                if (s == a) {
                    if (dists[t][b] + l == dists[a][b]) {
                        paths[a][b] += get(t, b);
                    }
                }
            }
        }
        return paths[a][b];
    }

    private int add(String node) {
        if (nodes.containsKey(node) == false) {
            nodes.put(node, nodesNum);
            ++nodesNum;
        }
        return nodes.get(node);
    }

    private Map<String, Integer> nodes;
    private long[][] paths;
    private long[][] graph;
    private long[][] dists;
    private int[] source;
    private int[] target;
    private int[] length;
    private int roadsNum;
    private int nodesNum;
    private int me;
}
