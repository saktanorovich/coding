using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0332 {
        public IList<string> FindItinerary(IList<IList<string>> tickets) {
            return new Reconstructor(tickets).Find("JFK");
        }

        private class Reconstructor {
            private readonly Dictionary<string, List<Edge>> graph;
            private readonly int ticketsCount;
            private List<string> path;
            private bool[] mark;

            public Reconstructor(IList<IList<string>> tickets) {
                graph = new Dictionary<string, List<Edge>>();
                for (var num = 0; num < tickets.Count; ++num) {
                    var ticket = tickets[num];
                    var source = ticket[0];
                    var target = ticket[1];
                    if (graph.ContainsKey(source) == false) {
                        graph.Add(source, new List<Edge>());
                    }
                    if (graph.ContainsKey(target) == false) {
                        graph.Add(target, new List<Edge>());
                    }
                    graph[source].Add(new Edge(num, target));
                }
                foreach (var edges in graph) {
                    edges.Value.Sort((a, b) => a.Target.CompareTo(b.Target));
                }
                ticketsCount = tickets.Count;
            }

            public IList<string> Find(string source) {
                mark = new bool[ticketsCount];
                path = new List<string> { source };
                dfs(source);
                return path;
            }

            private bool dfs(string source) {
                foreach (var edge in graph[source]) {
                    if (mark[edge.Number] == false) {
                        mark[edge.Number] = true;
                        path.Add(edge.Target);
                        if (dfs(edge.Target)) {
                            return true;
                        }
                        path.RemoveAt(path.Count - 1);
                        mark[edge.Number] = false;
                    }
                }
                return path.Count == ticketsCount + 1;
            }

            private class Edge {
                public readonly string Target;
                public readonly int Number;

                public Edge(int number, string target) {
                    Number = number;
                    Target = target;
                }
            }
        }
    }
}
