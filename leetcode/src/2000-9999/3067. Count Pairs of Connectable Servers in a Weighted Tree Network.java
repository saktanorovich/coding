class Solution {
    public int[] countPairsOfConnectableServers(int[][] edges, int signalSpeed) {
        var solver = new Solver(edges, signalSpeed);
        var result = solver.solve();
        return result;
    }

    private final class Solver {
        private final List<Edge>[] tree;
        private final int speed;

        public Solver(int[][] edges, int speed) {
            this.tree = make(edges);
            this.speed = speed;
        }

        public int[] solve() {
            var answ = new int[tree.length];
            for (var node = 0; node < tree.length; ++node) {
                answ[node] = count(node);
            }
            return answ;
        }

        private int count(int node) {
            var cnt = 0;
            var res = 0;
            for (var edge : tree[node]) {
                var has = calc(edge.next, node, edge.cost);
                res = res + has * cnt;
                cnt = cnt + has;
            }
            return res;
        }

        private int calc(int node, int prev, int cost) {
            var answ = cost % speed == 0 ? 1 : 0;
            for (var edge : tree[node]) {
                var next = edge.next;
                if (next != prev) {
                    answ += calc(
                        next,
                        node,
                        edge.cost + cost);
                }
            }
            return answ;
        }

        private List<Edge>[] make(int[][] edges) {
            var size = edges.length + 1;
            var tree = new ArrayList[size];
            for (var i = 0; i < size; ++i) {
                tree[i] = new ArrayList<>();
            }
            for (var e : edges) {
                tree[e[0]].add(new Edge(e[1], e[2]));
                tree[e[1]].add(new Edge(e[0], e[2]));
            }
            return tree;
        }

        private final class Edge {
            public final int next;
            public final int cost;

            public Edge(int next, int cost) {
                this.next = next;
                this.cost = cost;
            }
        }
    }
}