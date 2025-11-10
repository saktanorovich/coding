public class Solution {
    public double MaxProbability(int n, int[][] edges, double[] prob, int st, int en) {
        var best = new double[n];
        best[st] = 1.0;
        for (var v = 0; v < n; ++v) {
            var relax = false;
            for (var i = 0; i < edges.Length; ++i) {
                var a = edges[i][0];
                var b = edges[i][1];
                var p = prob [i];
                if (best[b] < best[a] * p) {
                    best[b] = best[a] * p;
                    relax = true;
                }
                if (best[a] < best[b] * p) {
                    best[a] = best[b] * p;
                    relax = true;
                }
            }
            if (relax == false) break;
        }
        return best[en];
    }
}
