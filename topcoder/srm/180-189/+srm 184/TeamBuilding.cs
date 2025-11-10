using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class TeamBuilding {
        public int fewestPaths(string[] paths) {
            return fewestPaths(Array.ConvertAll(paths, path => {
                return Array.ConvertAll(path.ToCharArray(), bit => bit == '1');
            }));
        }

        private static int fewestPaths(bool[][] graph) {
            /* build transitive closure in order to determine cycles and
             * reachable vertices for each vertex in the graph.. */
            for (var k = 0; k < graph.Length; ++k)
                for (var i = 0; i < graph.Length; ++i)
                    for (var j = 0; j < graph.Length; ++j) {
                        graph[i][j] = graph[i][j] || graph[i][k] && graph[k][j];
                    }
            var reachable = new int[graph.Length];
            var reachedby = new int[graph.Length];
            var targetset = 0;
            for (var i = 0; i < graph.Length; ++i) {
                if (!graph[i][i]) {
                    targetset |= 1 << i;
                }
                reachable[i] |= 1 << i;
                reachedby[i] |= 1 << i;
                for (var j = 0; j < graph.Length; ++j) {
                    if (graph[i][j]) reachable[i] |= 1 << j;
                    if (graph[j][i]) reachedby[i] |= 1 << j;
                }
            }
            return Math.Max(fewestPaths(reachable, targetset), fewestPaths(reachedby, targetset));
        }

        private static int fewestPaths(IList<int> sets, int targetset) {
            /* solve minimum set cover problem.. */
            var result = sets.Count;
            for (var set = 0; set < 1 << sets.Count; ++set) {
                var size = cardinality(set);
                if (size < result) {
                    var covered = 0;
                    for (var ix = 0; ix < sets.Count; ++ix) {
                        if (contains(set, 1 << ix)) {
                            covered |= sets[ix];
                        }
                    }
                    if (contains(covered, targetset)) {
                        result = size;
                    }
                }
            }
            return result;
        }

        private static int cardinality(int set) {
            if (set > 0) {
                return 1 + cardinality(set & (set - 1));
            }
            return 0;
        }

        private static bool contains(int set, int subset) {
            return (set & subset) == subset;
        }
    }
}