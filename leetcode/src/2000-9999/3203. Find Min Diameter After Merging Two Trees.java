class Solution {
    public int minimumDiameterAfterMerge(int[][] edges1, int[][] edges2) {
        var tree1 = make(edges1);
        var tree2 = make(edges2);
        var diam1 = from(tree1).getValue();
        var diam2 = from(tree2).getValue();
        var diam3 = 1;
        diam3 += (diam1 + 1) / 2;
        diam3 += (diam2 + 1) / 2;
        var res = diam3;
        res = Math.max(res, diam1);
        res = Math.max(res, diam2);
        return res;
    }

    private Pair<Integer, Integer> from(ArrayList<Integer>[] tree) {
        var best = new Pair<Integer, Integer>(0, 0);
        best = find(tree, best.getKey());
        best = find(tree, best.getKey());
        return best;
    }

    private Pair<Integer, Integer> find(ArrayList<Integer>[] tree, int from) {
        var taken = new boolean[tree.length];
        var queue = new LinkedList<Integer>();
        taken[from] = true;
        queue.offer(from);
        var curr = from;
        var diam = 0;
        for (; queue.isEmpty() == false; diam = diam + 1) {
            var size = queue.size();
            while (size > 0) {
                curr = queue.poll();
                size = size - 1;
                for (var next : tree[curr]) {
                    if (taken[next] == false) {
                        taken[next] = true;
                        queue.offer(next);
                    }
                }
            }
        }
        return new Pair<>(curr, diam - 1);
    }

    private ArrayList<Integer>[] make(int[][] edges) {
        var size = edges.length + 1;
        var tree = new ArrayList[size];
        for (var i = 0; i < size; ++i) {
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