using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_2642 {
        public class Graph {
            private readonly List<Edge>[] graph;

            public Graph(int n, int[][] edges) {
                graph = new List<Edge>[n];
                for (var i = 0; i < n; ++i) {
                    graph[i] = new List<Edge>();
                }
                foreach (var e in edges) {
                    graph[e[0]].Add(new Edge(e[0], e[1], e[2]));
                }
            }
    
            public void AddEdge(int[] e) {
                graph[e[0]].Add(new Edge(e[0], e[1], e[2]));
            }
    
            public int ShortestPath(int source, int target) {
                var best = new int[graph.Length];
                for (var i = 0; i < graph.Length; ++i) {
                    best[i] = int.MaxValue;
                }
                best[source] = 0;
                var queue = new Queue<int>();
                var onfly = new bool[graph.Length];
                for (onfly[source] = true, queue.Enqueue(source); queue.Count > 0;) {
                    var curr = queue.Dequeue();
                    onfly[curr] = false;
                    foreach (var edge in graph[curr]) {
                        if (best[edge.target] > best[curr] + edge.weight) {
                            best[edge.target] = best[curr] + edge.weight;
                            if (onfly[edge.target] == false) {
                                queue.Enqueue(edge.target);
                                onfly[edge.target] = true;
                            }
                        }
                    }
                }
                return best[target] < int.MaxValue ? best[target] : -1;
            }

            private sealed class Edge {
                public readonly int source;
                public readonly int target;
                public readonly int weight;

                public Edge(int source, int target, int weight) {
                    this.source = source;
                    this.target = target;
                    this.weight = weight;
                }
            }
        }
    }
}
