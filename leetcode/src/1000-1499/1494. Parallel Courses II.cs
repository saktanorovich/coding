using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1494 {
        public int MinNumberOfSemesters(int n, int[][] deps, int k) {
            graph = new List<int>[n];
            for (var i = 0; i < n; ++i) {
                graph[i] = new List<int>();
            }
            foreach (var dep in deps) {
                graph[dep[1] - 1].Add(dep[0] - 1);
            }
            bits = new int[1 << n];
            for (var i = 1; i < 1 << n; ++i) {
                bits[i] = bits[i >> 1] + (i & 1);
            }
            memo = new int[1 << n];
            for (var i = 1; i < 1 << n; ++i) {
                memo[i] = -1;
            }
            return doit((1 << n) - 1, n, k);
        }

        private int doit(int goal, int n, int k) {
            if (memo[goal] == -1) {
                var have = 0;
                for (var i = 0; i < n; ++i) {
                    if ((goal & (1 << i)) != 0) {
                        var deg = graph[i].Count;
                        foreach (var j in graph[i]) {
                            if ((goal & (1 << j)) == 0) {
                                deg--;
                            }
                        }
                        if (deg == 0) {
                            have |= 1 << i;
                        }
                    }
                }
                memo[goal] = int.MaxValue;
                for (var take = have; take > 0; take = (take - 1) & have) {
                    if (bits[take] <= k) {
                        memo[goal] = Math.Min(memo[goal], doit(goal ^ take, n, k) + 1);
                    }
                }
            }
            return memo[goal];
        }

        private List<int>[] graph;
        private int[] memo;
        private int[] bits;
    }
}
