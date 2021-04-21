class Solution {
    public int[] loudAndRich(int[][] richer, int[] quiet) {
        return new Solver(richer, quiet).solve();
    }

    private final class Solver {
        private final List<Integer>[] graph;
        private final int[] quiet;
        private final int n;

        public Solver(int[][] richer, int[] quiet) {
            this.n = quiet.length;
            this.quiet = quiet;
            this.graph = new ArrayList[n];
            for (var i = 0; i < n; ++i) {
                graph[i] = new ArrayList<Integer>();
            }
            for (var e : richer) {
                graph[e[1]].add(e[0]);
            }
        }

        public int[] solve() {
            var answer = new int[n];
            Arrays.fill(answer, -1);
            for (var i = 0; i < n; ++i) {
                dfs(i, answer);
            }
            return answer;
        }

        private void dfs(int curr, int[] answer) {
            if (answer[curr] != -1) {
                return;
            }
            answer[curr] = curr;
            for (var next : graph[curr]) {
                dfs(next, answer);
                if (quiet[answer[curr]] > quiet[answer[next]]) {
                    answer[curr] = answer[next];
                }
            }
        }
    }
}