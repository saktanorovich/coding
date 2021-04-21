class Solution {
    public int[][] validArrangement(int[][] edges) {
        return new Euler(edges).solve();
    }

    private final class Euler {
        private final Map<Integer, Deque<Integer>> graph;
        private final Map<Integer, Integer> incoming;
        private final Map<Integer, Integer> outgoing;
        private final int[][] edges;

        public Euler(int[][] edges) {
            this.graph = new HashMap<>();
            this.incoming = new HashMap<>();
            this.outgoing = new HashMap<>();
            for (var edge : edges) {
                var st = edge[0];
                var fn = edge[1];
                if (graph.containsKey(st) == false) {
                    graph.put(st, new ArrayDeque<>());
                    incoming.put(st, 0);
                    outgoing.put(st, 0);
                }
                if (graph.containsKey(fn) == false) {
                    graph.put(fn, new ArrayDeque<>());
                    incoming.put(fn, 0);
                    outgoing.put(fn, 0);
                }
                graph.get(st).add(fn);
                incoming.put(fn, incoming.get(fn) + 1);
                outgoing.put(st, outgoing.get(st) + 1);
            }
            this.edges = edges;
        }

        private int[][] solve() {
            // according to Euler Theorem no more than one bridge
            // should be in the given graph
            for (var node : graph.keySet()) {
                // let's find a bridge by comparing
                // outgoing and incoming counts
                var o = outgoing.get(node);
                var i = incoming.get(node);
                var d = o - i;
                if (d == 1) {
                    return find(node);
                }
            }
            return find(edges[0][0]);
        }

        private int[][] find(int node) {
            var path = dfs(node, new ArrayList<Integer>());
            var answ = new int[path.size() - 1][2];
            Collections.reverse(path);
            for (var i = 0; i + 1 < path.size(); ++i) {
                answ[i][0] = path.get(i);
                answ[i][1] = path.get(i + 1);
            }
            return answ;
        }

        private ArrayList<Integer> dfs(Integer curr, ArrayList<Integer> path) {
            var adj = graph.get(curr);
            while (adj.isEmpty() == false) {
                var next = adj.pollFirst();
                dfs(next, path);
            }
            path.add(curr);
            return path;
        }
    }
}