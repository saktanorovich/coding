public class Solution {
    public bool[] FindAnswer(int n, int[][] edges) {
        return find(n, edges.Length, edges, make(edges, n));
    }

    private bool[] find(int n, int m, int[][] edges, List<(int target, int weight)>[] graph) {
        var src2trg = spfa(graph, n, 0, n - 1);
        var trg2src = spfa(graph, n, n - 1, 0);
        var answ = new bool[m];
        for (var i = 0; i < m; ++i) {
            var a = edges[i][0];
            var b = edges[i][1];
            var w = edges[i][2];
            if (okay(src2trg, trg2src, n, a, b, w)) answ[i] = true;
            if (okay(src2trg, trg2src, n, b, a, w)) answ[i] = true;
        }
        return answ;
    }

    private static bool okay(long[] src2trg, long[] trg2src, int n, int a, int b, long w) {
        var ca = src2trg[a];
        var cb = trg2src[b];
        if (ca + cb + w == src2trg[n - 1]) {
            return true;
        } else {
            return false;
        }
    }

    private static long[] spfa(List<(int target, int weight)>[] graph, int n, int a, int b) {
        var best = new long[n];
        for (var i = 0; i < n; ++i) {
            best[i] = long.MaxValue / 4;
        }
        best[a] = 0;
        var has = new bool[n];
        var que = new Queue<int>();
        for (que.Enqueue(a); que.Count > 0;) {
            var source = que.Dequeue();
            has[source] = false;
            foreach (var edge in graph[source]) {
                var target = edge.target;
                var weight = edge.weight;
                if (best[target] > best[source] + weight) {
                    best[target] = best[source] + weight;
                    if (has[target] == false) {
                        has[target] = true;
                        que.Enqueue(target);
                    }
                }
            }
        }
        return best;
    }

    private static List<(int, int)>[] make(int[][] edges, int n) {
        var graph = new List<(int, int)>[n];
        for (var i = 0; i < n; ++i) {
            graph[i] = new List<(int, int)>();
        }
        for (var i = 0; i < edges.Length; ++i) {
            var a = edges[i][0];
            var b = edges[i][1];
            var w = edges[i][2];
            graph[a].Add((b, w));
            graph[b].Add((a, w));
        }
        return graph;
    }
}