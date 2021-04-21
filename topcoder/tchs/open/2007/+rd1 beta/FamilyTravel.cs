using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class FamilyTravel {
        public int shortest(string[] edges) {
            return shortest(edges, edges.Length);
        }

        private int shortest(string[] edges, int n) {
            graph = new int[n, n];
            for (var i = 0; i < n; ++i) {
                for (var j = 0; j < n; ++j) {
                    graph[i, j] = dist(edges[i][j]);
                }
            }
            memo = new int[n, n + 1, 100];
            for (var curr = 0; curr < n; ++curr) {
                for (var prev = 0; prev <= n; ++prev) {
                    for (var cost = 0; cost < 100; ++cost) {
                        memo[curr, prev, cost] = -1;
                    }
                }
            }
            var res = run(n, 0, n, 99);
            if (res < int.MaxValue / 2) {
                return res;
            }
            return -1;
        }

        private int run(int n, int curr, int prev, int cost) {
            if (curr == 1) {
                return 0;
            }
            if (memo[curr, prev, cost] == -1) {
                memo[curr, prev, cost] = int.MaxValue / 2;
                for (var next = 0; next < n; ++next) {
                    if (next != prev && graph[curr, next] <= cost) {
                        memo[curr, prev, cost] = Math.Min(memo[curr, prev, cost], run(n, next, curr, graph[curr, next]) + graph[curr, next]);
                    }
                }
            }
            return memo[curr, prev, cost];
        }

        private static int dist(char e) {
            if ('a' <= e && e <= 'z') {
                return e - 'a' + 1;
            }
            if ('A' <= e && e <= 'Z') {
                return e - 'A' + 27;
            }
            return int.MaxValue / 2;
        }

        private int[,] graph;
        private int[,,] memo;
    }
}
