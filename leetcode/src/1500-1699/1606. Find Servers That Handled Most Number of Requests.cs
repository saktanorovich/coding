using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1606 {
        public IList<int> BusiestServers(int k, int[] arrival, int[] load) {
            var busy = new PriorityQueue<int, int>();
            var idle = new RoundRobin(k);
            var take = new int[k];
            for (var i = 0; i < arrival.Length; ++i) {
                while (busy.TryPeek(out var indx, out var time)) {
                    if (time <= arrival[i]) {
                        idle.Enqueue(indx);
                        busy.Dequeue();
                    } else break;
                } {
                    var indx = idle.TryPeek(i % k);
                    if (indx >= 0) {
                        busy.Enqueue(indx, arrival[i] + load[i]);
                        idle.Dequeue(indx);
                        take[indx] ++;
                    }
                }
            }
            var busiest = take.Max();
            var outcome = new List<int>();
            for (var i = 0; i < k; ++i) {
                if (take[i] == busiest) {
                    outcome.Add(i);
                }
            }
            return outcome;
        }
        /**/
        private sealed class RoundRobin {
            private readonly int[] tree;
            private readonly int n;

            public RoundRobin(int n) {
                this.tree = new int[2 * n];
                for (var i = 0; i < n; ++i) {
                    tree[i + n] = i;
                }
                for (var i = n - 1; i > 0; --i) {
                    tree[i] = Math.Min(tree[i << 1], tree[i << 1 | 1]);
                }
                this.n = n;
            }

            public void Enqueue(int p) => Update(p + n, p);

            public void Dequeue(int p) => Update(p + n, n);

            public int TryPeek(int p) {
                var r = Query(p + n, n + n);
                if (r < n) {
                    return r;
                }
                var l = Query(0 + n, p + n);
                if (l < n) {
                    return l;
                }
                return -1;
            }

            private int Query(int l, int r) {
                var res = n;
                for (; l < r; l >>= 1, r >>= 1) {
                    if ((l & 1) == 1) res = Math.Min(res, tree[l++]);
                    if ((r & 1) == 1) res = Math.Min(res, tree[--r]);
                }
                return res;
            }

            private void Update(int p, int v) {
                tree[p] = v;
                for (; p > 1; p >>= 1) {
                    tree[p >> 1] = Math.Min(tree[p], tree[p ^ 1]);
                } 
            }
        }
        /**
        private sealed class RoundRobin {
            private readonly SortedSet<int> tree;

            public RoundRobin(int n) {
                tree = new SortedSet<int>();
                for (var i = 0; i < n; ++i) {
                    tree.Add(i);
                }
            }

            public void Enqueue(int p) => tree.Add(p);

            public void Dequeue(int p) => tree.Remove(p);

            public int TryPeek(int p) {
                if (tree.Count > 0) {
                    if (p > tree.Max)
                        return tree.Min;
                    else
                        return tree.GetViewBetween(p, tree.Max).Min;
                }
                return -1;
            }
        }
        /**/
    }
}