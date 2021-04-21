using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class EulerianRace {
        public int[] planRoute(string[] bridges) {
            return planRoute(Array.ConvertAll(bridges, row => {
                var result = new List<int>();
                for (var i = 0; i < bridges.Length; ++i) {
                    if (row[i] == '1') {
                        result.Add(i);
                    }
                }
                return result;
            }));
        }

        private static int[] planRoute(List<int>[] graph) {
            var path = new List<int> { 0 };
            for (var done = false; !done;) {
                done = true;
                for (var pos = 0; pos < path.Count; ++pos) {
                    var from = path[pos];
                    if (graph[from].Count > 0) {
                        var curr = graph[from][0];
                        graph[from].Remove(curr);
                        graph[curr].Remove(from);
                        path.InsertRange(pos + 1, dfs(graph, from, curr));
                        done = false;
                        break;
                    }
                }
            }
            return path.ToArray();
        }

        private static IList<int> dfs(List<int>[] graph, int from, int curr) {
            var path = new List<int> { curr };
            if (curr != from) {
                foreach (var next in graph[curr]) {
                    graph[curr].Remove(next);
                    graph[next].Remove(curr);
                    path.AddRange(dfs(graph, from, next));
                    break;
                }
            }
            return path;
        }
    }
}
