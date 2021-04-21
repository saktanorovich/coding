class Solution {
    public int maximumInvitations(int[] favorite) {
        var solver = new Solver(favorite);
        var result = solver.solve();
        return result;
    }

    private final class Solver {
        private final Queue<Integer> queue; 
        private final int[] favorite;
        private final int[] distance;
        private final int[] inDegree;
        private final int n;

        public Solver(int[] favorite) {
            this.n = favorite.length;
            this.favorite = favorite;
            this.distance = new int[n];
            this.inDegree = new int[n];
            for (var i = 0; i < n; ++i) {
                inDegree[favorite[i]] ++;
            }
            this.queue = new LinkedList<>();
        }

        public int solve() {
            for (var i = 0; i < n; ++i) {
                if (inDegree[i] == 0) {
                    queue.add(i);
                }
                distance[i] = 1;
            }
            while (queue.isEmpty() == false) {
                var curr = queue.poll();
                var next = favorite[curr];
                distance[next] = Math.max(distance[next], distance[curr] + 1);
                inDegree[next] --;
                if (inDegree[next] == 0) {
                    queue.add(next);
                }
            }
            var eqTwoLoopLen = 0;
            var gtTwoLoopLen = 0;
            for (var i = 0; i < n; ++i) {
                if (inDegree[i] == 0) {
                    continue;
                }
                var loopLen = loop(i);
                if (loopLen == 2) {
                    var distToSrc = distance[i];
                    var distToDst = distance[favorite[i]];
                    eqTwoLoopLen += distToSrc + distToDst;
                } else {
                    gtTwoLoopLen = Math.max(gtTwoLoopLen, loopLen);
                }
            }
            return Math.max(eqTwoLoopLen, gtTwoLoopLen);
        }

        private int loop(int curr) {
            var loopLen = 0;
            while (inDegree[curr] != 0) {
                inDegree[curr] = 0;
                loopLen ++;
                curr = favorite[curr];
            }
            return loopLen;
        }
    }
}