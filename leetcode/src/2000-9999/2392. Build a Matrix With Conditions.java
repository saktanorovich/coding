class Solution {
    public int[][] buildMatrix(int k, int[][] rowConditions, int[][] colConditions) {
        var rowSorted = topSort(rowConditions, k);
        var colSorted = topSort(colConditions, k);
        if (rowSorted == null || colSorted == null) {
            return new int[0][0];
        }
        var res = new int[k][k];
        for (var i = 0; i < k; ++i) {
            for (var j = 0; j < k; ++j) {
                int r = rowSorted.get(i);
                int c = colSorted.get(j);
                if (r == c) {   
                    res[i][j] = r;
                }
            }
        }
        return res;
    }
 
    private ArrayList<Integer> topSort(int[][] conds, int k) {    
        List<Integer>[] adj = new ArrayList[k + 1];
        for (var i = 1; i <= k; ++i) {
            adj[i] = new ArrayList<Integer>();
        }
        var deg = new int[k + 1];
        for (var e : conds) {
            adj[e[0]].add(e[1]);
            deg[e[1]] ++;
        }
        var que = new LinkedList<Integer>();
        for (var i = 1; i <= k; ++i) {
            if (deg[i] == 0) {
                que.add(i);
            }
        }
        var res = new ArrayList<Integer>(k);
        while (que.isEmpty() == false) {
            var cur = que.poll();
            res.add(cur);
            for (var nxt : adj[cur]) {
                deg[nxt] --;
                if (deg[nxt] == 0) {
                    que.add(nxt);
                }
            }
        }
        return res.size() == k ? res : null;
    }
}