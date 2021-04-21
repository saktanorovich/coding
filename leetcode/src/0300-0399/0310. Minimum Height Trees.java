class Solution {
    public List<Integer> findMinHeightTrees(int n, int[][] edges) {
        var tree = make(edges, n);
        var degree = new int[tree.length];
        for (var i = 0; i < tree.length; ++i) {
            degree[i] = tree[i].size();
        }
        var queue = new LinkedList<Integer>();
        for (var i = 0; i < tree.length; ++i) {
            if (degree[i] <= 1) {
                queue.offer(i);
            }
        }
        var have = tree.length;
        while (have > 2) {
            var size = queue.size();
            have -= size;
            while (size > 0) {
                var curr = queue.poll();
                size = size - 1;
                for (var next : tree[curr]) {
                    degree[next] --;
                    if (degree[next] == 1) {
                        queue.offer(next);
                    }
                }
            }
        }
        return new ArrayList<>(queue);
    }

    private ArrayList<Integer>[] make(int[][] edges, int n) {
        var tree = new ArrayList[n];
        for (var i = 0; i < n; ++i) {
            tree[i] = new ArrayList<>();
        }
        for (var e : edges) {
            var a = e[0];
            var b = e[1];
            tree[a].add(b);
            tree[b].add(a);
        }
        return tree;
    }
}