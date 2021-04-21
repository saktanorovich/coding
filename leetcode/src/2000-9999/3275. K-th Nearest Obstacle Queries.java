class Solution {
    public int[] resultsArray(int[][] queries, int k) {
        return doit(queries, queries.length, k);
    }

    private int[] doit(int[][] q, int n, int k) {
        var heap = new PriorityQueue<Integer>(k,
            (a, b) -> b - a
        );
        var res = new int[n];
        for (var i = 0; i < n; ++i) {
            heap.add(dist(q[i]));
            if (heap.size() > k) {
                heap.poll();
            }
            res[i] = -1;
            if (heap.size() == k) {
                res[i] = heap.peek();
            }
        }
        return res;
    }

    private int dist(int[] pt) {
        return Math.abs(pt[0]) + Math.abs(pt[1]);
    }
}