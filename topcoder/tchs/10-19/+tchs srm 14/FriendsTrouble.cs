using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class FriendsTrouble {
        public int divide(string[] names, string[] friends) {
            var graph = new List<int>[names.Length];
            var table = new Dictionary<string, int>();
            for (var i = 0; i < names.Length; ++i) {
                graph[i] = new List<int>();
                table[names[i]] = i;
            }
            foreach (var s in friends) {
                var items = s.Split(' ');
                graph[table[items[0]]].Add(table[items[1]]);
                graph[table[items[1]]].Add(table[items[0]]);
            }
            var used = new bool[names.Length];
            var res = 0;
            for (var i = 0; i < names.Length; ++i) {
                if (used[i] == false) {
                    dfs(graph, used, i);
                    res = res + 1;
                }
            }
            return res;
        }

        private static void dfs(List<int>[] graph, bool[] used, int curr) {
            used[curr] = true;
            foreach (var next in graph[curr]) {
                if (used[next] == false) {
                    dfs(graph, used, next);
                }
            }
        }
    }
}
