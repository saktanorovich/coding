using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1801 {
        public int GetNumberOfBacklogOrders(int[][] orders) {
            var backlog = new Backlog();
            for (var i = 0; i < orders.Length; ++i) {
                backlog.put(i, orders[i]);
            }
            return backlog.count;
        }

        private const int mod = (int)1e9 + 7;

        private class Backlog {
            private SortedSet<int[]> blog;
            private SortedSet<int[]> slog;

            public Backlog() {
                blog = new SortedSet<int[]>(
                    Comparer<int[]>.Create((a, b) => {
                        if (a[1] != b[1]) {
                            return b[1] - a[1];
                        } else {
                            return a[0] - b[0];
                        }
                }));
                slog = new SortedSet<int[]>(
                    Comparer<int[]>.Create((a, b) => {
                        if (a[1] != b[1]) {
                            return a[1] - b[1];
                        } else {
                            return a[0] - b[0];
                        }
                }));
            }

            public int count {
                get {
                    var cnt = 0;
                    foreach (var item in blog.Concat(slog)) {
                        cnt += item[2];
                        cnt %= mod;
                    }
                    return cnt;
                }
            }

            public void put(int index, int[] order) {
                if (order[2] == 0) {
                    b(index, order[0], order[1]);
                } else {
                    s(index, order[0], order[1]);
                }
            }

            private void b(int index, int price, int amount) {
                while (slog.Count > 0 && amount > 0) {
                    var min = slog.First();
                    if (min[1] <= price) {
                        if (amount >= min[2]) {
                            amount -= min[2];
                            slog.Remove(min);
                        } else {
                            min[2] -= amount;
                            amount = 0;
                        }
                    } else break;
                }
                if (amount > 0) {
                    blog.Add(new[] { index, price, amount });
                }
            }

            private void s(int index, int price, int amount) {
                while (blog.Count > 0 && amount > 0) {
                    var max = blog.First();
                    if (max[1] >= price) {
                        if (amount >= max[2]) {
                            amount -= max[2];
                            blog.Remove(max);
                        } else {
                            max[2] -= amount;
                            amount = 0;
                        }
                    } else break;
                }
                if (amount > 0) {
                    slog.Add(new[] { index, price, amount });
                }
            }
        }
    }
}
