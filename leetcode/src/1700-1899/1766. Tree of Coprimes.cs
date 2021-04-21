using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1766 {
        public int[] GetCoprimes(int[] nums, int[][] edges) {
            this.nums = nums;
            this.tree = new List<int>[edges.Length + 1];
            for (var i = 0; i < tree.Length; ++i) {
                tree[i] = new List<int>();
            }
            foreach (var e in edges) {
                tree[e[0]].Add(e[1]);
                tree[e[1]].Add(e[0]);
            }
            this.elements = new Stack<(int, int)>[51];
            this.ancestor = new int[tree.Length];
            return dfs(0, -1, 0);
        }

        private Stack<(int elem, int depth)>[] elements;
        private List<int>[] tree;
        private int[] nums;
        private int[] ancestor;

        private int[] dfs(int node, int prev, int depth) {
            (int elem, int depth) best = (-1, -1);
            for (var x = 1; x <= 50; ++x) {
                if (gcd(x, nums[node]) == 1) {
                    if (elements[x] != null && elements[x].Count > 0) {
                        if (best.elem == -1 || best.depth < elements[x].Peek().depth) {
                            best = elements[x].Peek();
                        }
                    }
                }
            }
            ancestor[node] = best.elem;
            elements[nums[node]] = elements[nums[node]] ?? new Stack<(int, int)>();
            elements[nums[node]].Push((node, depth));
            foreach (var next in tree[node]) {
                if (next != prev) {
                    dfs(next, node, depth + 1);
                }
            }
            elements[nums[node]].Pop();
            return ancestor;
        }

        private static int gcd(int a, int b) {
            while (a != 0 && b != 0) {
                if (a > b) {
                    a %= b;
                } else {
                    b %= a;
                }
            }
            return a + b;
        }
    }
}
