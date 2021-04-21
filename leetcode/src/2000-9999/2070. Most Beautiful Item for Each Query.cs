public class Solution {
    public int[] MaximumBeauty(int[][] items, int[] queries) {
        Array.Sort(items, (a, b) => a[0] - b[0]);
        var order = Enumerable.Range(0, queries.Length).ToArray();
        Array.Sort(queries, order);
        var best = 0;
        var answ = new int[queries.Length];
        for (int q = 0, i = 0; q < queries.Length; ++q) {
            while (i < items.Length) {
                if (items[i][0] <= queries[q]) {
                    best = Math.Max(best, items[i][1]);
                    i = i + 1;
                }
                else break;
            }
            answ[order[q]] = best;
        }
        return answ;
    }
}
