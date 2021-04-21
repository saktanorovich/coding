using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1847 {
        public int[] ClosestRoom(int[][] rooms, int[][] queries) {
            var tree = new Tree(rooms);
            var answ = new int[queries.Length];
            for (var i = 0; i < queries.Length; ++i) {
                answ[i] = tree.get(queries[i][0], queries[i][1]);
            }
            return answ;
        }

        public class Tree {
            private readonly List<int>[] ids;
            private readonly int[] min;
            private readonly int[] max;
            private readonly int size;

            public Tree(int[][] rooms) {
                Array.Sort(rooms, (int[] a, int[] b) => a[1] - b[1]);
                size = rooms.Length;
                ids = new List<int>[4 * size];
                min = new int[4 * size];
                max = new int[4 * size];
                build(rooms, 1, 0, size - 1);
            }

            public int get(int room, int minSize) {
                return get(1, 0, size - 1, room, minSize).room;
            }

            private (int room, int diff) get(int node, int lo, int hi, int room, int minSize) {
                if (node < 4 * size && ids[node] != null) {
                    if (minSize <= min[node]) {
                        return closest(ids[node], room);
                    }
                    if (minSize <= max[node]) {
                        var xx = (lo + hi) / 2;
                        var l = get(2 * node, lo, xx, room, minSize);
                        var r = get(2 * node + 1, xx + 1, hi, room, minSize);
                        if (l.room == -1)    return r;
                        if (r.room == -1)    return l;
                        if (r.diff < l.diff) return r;
                        if (l.diff < r.diff) return l;
                        return (Math.Min(l.room, r.room), l.diff);
                    }
                }
                return (-1, -1);
            }

            private (int room, int diff) closest(List<int> ids, int room) {
                var idx = ids.BinarySearch(room);
                if (idx < 0) {
                    idx = ~idx;
                    var opt = -1;
                    for (var t = idx - 1; t <= idx + 1; ++t) {
                        if (0 <= t && t < ids.Count) {
                            if (opt == -1 || Math.Abs(ids[t] - room) < Math.Abs(ids[opt] - room)) {
                                opt = t;
                            }
                        }
                    }
                    return (ids[opt], Math.Abs(ids[opt] - room));
                }
                return (ids[idx], 0);
            }

            private void build(int[][] rooms, int node, int lo, int hi) {
                if (lo == hi) {
                    ids[node] = new List<int> { rooms[lo][0] };
                    min[node] = rooms[lo][1];
                    max[node] = rooms[lo][1];
                } else {
                    var xx = (lo + hi) / 2;
                    build(rooms, 2 * node, lo, xx);
                    build(rooms, 2 * node + 1, xx + 1, hi);
                    min[node] = min[2 * node];
                    max[node] = max[2 * node + 1];
                    ids[node] = merge(ids[2 * node], ids[2 * node + 1]);
                }
            }

            private List<int> merge(List<int> list1, List<int> list2) {
                var res = new List<int>();
                var i1 = 0;
                var i2 = 0;
                while (i1 < list1.Count && i2 < list2.Count) {
                    if (list1[i1] < list2[i2]) {
                        res.Add(list1[i1++]);
                    } else {
                        res.Add(list2[i2++]);
                    }
                }
                while (i1 < list1.Count) res.Add(list1[i1++]);
                while (i2 < list2.Count) res.Add(list2[i2++]);
                return res;
            }
        }
    }
}
