public class Solution {
    public int NumberOfSets(int n, int maxDistance, int[][] roads) {
        var dist = new int[n, n];
        for (var u = 0; u < n; ++u) {
            for (var v = u + 1; v < n; ++v) {
                dist[u, v] = int.MaxValue / 2;
                dist[v, u] = int.MaxValue / 2;
            }
        }
        foreach (var road in roads) {
            var u = road[0];
            var v = road[1];
            var w = road[2];
            dist[u, v] = Math.Min(dist[u, v], w);
            dist[v, u] = Math.Min(dist[v, u], w);
        }
        var res = 0;
        for (var set = 0; set < 1 << n; ++set) {
            var max = floyd((int[,])dist.Clone(), set, n);
            if (max <= maxDistance) {
                res ++;
            }
        }
        return res;
    }

    private int floyd(int[,] dist, int set, int n) {
        for (var k = 0; k < n; ++k) {
            if ((set & (1 << k)) != 0) {
                for (var i = 0; i < n; ++i) {
                    if ((set & (1 << i)) != 0) {
                        for (var j = 0; j < n; ++j) {
                            if ((set & (1 << j)) != 0) {
                                dist[i, j] = Math.Min(dist[i, j], dist[i, k] + dist[k, j]);
                            }
                        }
                    }
                }
            }
        }
        var res = 0;
        for (var i = 0; i < n; ++i) {
            if ((set & (1 << i)) != 0) {
                for (var j = 0; j < n; ++j) {
                    if ((set & (1 << j)) != 0) {
                        res = Math.Max(res, dist[i, j]);
                    }
                }
            }
        }
        return res;
    }
}
