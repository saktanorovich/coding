using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0981 {
        public class TimeMap {
            private readonly Dictionary<string, List<Entry>> store;

            public TimeMap() {
                store = new Dictionary<string, List<Entry>>();
            }

            public void Set(string key, string value, int timestamp) {
                if (store.TryGetValue(key, out var items) == false) {
                    items = new List<Entry>();
                    store[key] = items;
                }
                items.Add(new Entry(timestamp, value));
            }

            public string Get(string key, int timestamp) {
                if (store.TryGetValue(key, out var items)) {
                    var lo = 0;
                    var hi = items.Count - 1;
                    while (lo < hi) {
                        var x = (lo + hi) / 2;
                        if (items[x].Timestamp < timestamp) {
                            lo = x + 1;
                        } else {
                            hi = x;
                        }
                    }
                    if (items[lo].Timestamp <= timestamp) {
                        return items[lo].Value;
                    } else if (lo > 0) {
                        return items[lo - 1].Value;
                    }
                }
                return String.Empty;
            }

            private class Entry {
                public int Timestamp;
                public string Value;

                public Entry(int timestamp, string value) {
                    Timestamp = timestamp;
                    Value = value;
                }
            }
        }
    }
}
