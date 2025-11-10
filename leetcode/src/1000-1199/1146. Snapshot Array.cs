using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1146 {
        public class SnapshotArray {
            private readonly List<Entry>[] array;
            private int snapId;

            public SnapshotArray(int length) {
                array = new List<Entry>[length];
                snapId = 0;
            }

            public void Set(int index, int value) {
                if (array[index] == null) {
                    array[index] = new List<Entry>();
                }
                var items = array[index];
                if (items.Count == 0 || items.Last().SnapId < snapId) {
                    items.Add(new Entry(snapId, value));
                } else {
                    items.Last().Value = value;
                }
            }

            public int Snap() {
                return snapId++;
            }

            public int Get(int index, int snap_id) {
                if (array[index] == null) {
                    return 0;
                }
                var items = array[index];
                var lo = 0;
                var hi = items.Count - 1;
                while (lo < hi) {
                    var x = (lo + hi) / 2;
                    if (items[x].SnapId < snap_id) {
                        lo = x + 1;
                    } else {
                        hi = x;
                    }
                }
                if (items[lo].SnapId <= snap_id) {
                    return items[lo].Value;
                } else if (lo > 0) {
                    return items[lo - 1].Value;
                } else {
                    return 0;
                }
            }

            private class Entry {
                public int SnapId;
                public int Value;

                public Entry(int snapId, int value) {
                    SnapId = snapId;
                    Value = value;
                }
            }
        }
    }
}
