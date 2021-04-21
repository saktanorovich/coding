class Solution {
    public int[] countSubTrees(int n, int[][] edges, String labels) {
        var solver = new Solver(n, edges, labels);
        var result = solver.solve();
        return result;
    }

    private class Solver {
        private final String mark;
        private final List<Integer>[] tree;
        private final int[] answ;
        private final int n;

        public Solver(int n, int[][] edges, String labels) {
            this.tree = make(edges, n);
            this.answ = new int[n];
            this.mark = labels;
            this.n = n;
        }

        public int[] solve() {
            walk(0, -1);
            return answ;
        }

        private int[] walk(int curr, int prev) {
            answ[curr] = 1;
            var have = new int[26];
            have[mark.charAt(curr) - 'a'] = 1;
            for (var next : tree[curr]) {
                if (next != prev) {
                    var temp = walk(next, curr);
                    answ[curr] += temp[mark.charAt(curr) - 'a'];
                    for (var i = 0; i < 26; ++i) {
                        have[i] += temp[i];
                    }
                }
            }
            return have;
        }

        private static List<Integer>[] make(int[][] edges, int n) {
            var tree = new ArrayList[n];
            for (var i = 0; i < n; ++i) {
                tree[i] = new ArrayList<Integer>();
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
}