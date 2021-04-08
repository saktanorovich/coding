using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0127 {
        public int LadderLength(string begWord, string endWord, IList<string> words) {
            for (var i = 0; i < words.Count; ++i) {
                if (words[i] == endWord) {
                    return bfs(build(begWord, words), 0, i + 1);
                }
            }
            return 0;
        }

        private List<int>[] build(string initial, IList<string> words) {
            var graph = new List<int>[words.Count + 1];
            for (var i = 0; i < words.Count + 1; ++i) {
                graph[i] = new List<int>();
            }
            for (var i = 0; i < words.Count; ++i) {
                if (single(initial, words[i])) {
                    graph[0].Add(i + 1);
                    graph[i + 1].Add(0);
                }
                for (var j = i + 1; j < words.Count; ++j) {
                    if (single(words[i], words[j])) {
                        graph[i + 1].Add(j + 1);
                        graph[j + 1].Add(i + 1);
                    }
                }
            }
            return graph;
        }

        private int bfs(List<int>[] graph, int src, int dst) {
            var dist = new int[graph.Length];
            for (var i = 0; i < graph.Length; ++i) {
                dist[i] = int.MaxValue;
            }
            dist[src] = 1;
            var queue = new Queue<int>();
            for (queue.Enqueue(src); queue.Count > 0;) {
                var curr = queue.Dequeue();
                if (curr == dst) {
                    return dist[dst];
                }
                foreach (var next in graph[curr]) {
                    if (dist[next] > dist[curr] + 1) {
                        dist[next] = dist[curr] + 1;
                        queue.Enqueue(next);
                    }
                }
            }
            return 0;
        }

        private bool single(string a, string b) {
            var c = 0;
            for (var i = 0; i < a.Length; ++i) {
                if (a[i] != b[i]) {
                    c++;
                    if (c > 1) {
                        return false;
                    }
                }
            }
            return c == 1;
        }
    }
}
