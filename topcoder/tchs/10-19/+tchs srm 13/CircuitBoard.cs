using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class CircuitBoard {
        public int maxDataLines(string[] layout) {
            return maxDataLines(layout, layout.Length, layout[0].Length);
        }

        private static int maxDataLines(string[] layout, int n, int m) {
            var graph = new List<Edge>[2 * n * m + 2];
            for (int col = 0, top = 0, bot = n - 1; col < m; ++col) {
                if (layout[top][col] == '.') {
                    add(graph, 0, 2 * (top * m + col) + 2);
                }
                if (layout[bot][col] == '.') {
                    add(graph, 2 * (bot * m + col) + 3, 1);
                }
            }
            for (var row = 0; row < n; ++row) {
                for (var col = 0; col < m; ++col) {
                    add(graph, 2 * (row * m + col) + 2, 2 * (row * m + col) + 3);
                }
            }
            for (var row = 0; row + 1 < n; ++row) {
                for (var col = 0; col < m; ++col) {
                    if (layout[row][col] == '.') {
                        for (var stp = -1; stp <= +1; ++stp) {
                            if (0 <= col + stp && col + stp < m) {
                                if (layout[row + 1][col + stp] == '.') {
                                    add(graph, 2 * (row * m + col) + 3, 2 * ((row + 1) * m + col + stp) + 2);
                                }
                            }
                        }
                    }
                }
            }
            var res = 0;
            for (var queue = new Queue<int>();;) {
                var prev = new Edge[2 * n * m + 2];
                for (queue.Enqueue(0); queue.Count > 0;) {
                    var source = queue.Dequeue();
                    if (graph[source] != null) {
                        foreach (var edge in graph[source]) {
                            if (prev[edge.target] == null) {
                                if (edge.flow == 0) {
                                    prev[edge.target] = edge;
                                    queue.Enqueue(edge.target);
                                }
                            }
                        }
                    }
                }
                if (prev[1] != null) {
                    for (var source = 1; source != 0;) {
                        prev[source].flow = 1;
                        source = prev[source].source;
                    }
                    res += 1;
                }
                else break;
            }
            return res;
        }

        private static void add(List<Edge>[] graph, int source, int target) {
            if (graph[source] == null) {
                graph[source] = new List<Edge>();
            }
            graph[source].Add(new Edge(source, target));
        }

        private class Edge {
            public readonly int source;
            public readonly int target;
            public int flow;

            public Edge(int source, int target) {
                this.source = source;
                this.target = target;
                this.flow = 0;
            }
        }
    }
}
