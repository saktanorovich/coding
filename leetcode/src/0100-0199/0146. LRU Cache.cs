using System;
using System.Collections.Generic;

namespace coding.leetcode {
    public class LRUCache {
        private readonly Dictionary<int, LinkedListNode<int>> addr;
        private readonly Dictionary<int, int> vals;
        private readonly LinkedList<int> list;
        private readonly int capacity;

        public LRUCache(int capacity) {
            this.capacity = capacity;
            this.addr = new Dictionary<int, LinkedListNode<int>>();
            this.vals = new Dictionary<int, int>();
            this.list = new LinkedList<int>();
        }

        public int Get(int key) {
            if (Warm(key)) {
                return vals[key];
            } else {
                return -1;
            }
        }

        public void Put(int key, int value) {
            if (Warm(key) == false) {
                addr[key] = list.AddFirst(key);
                if (list.Count > capacity) {
                    var last = list.Last;
                    addr.Remove(last.Value);
                    vals.Remove(last.Value);
                    list.Remove(last);
                }
            }
            vals[key] = value;
        }

        private bool Warm(int key) {
            if (addr.TryGetValue(key, out var node)) {
                list.Remove(node);
                list.AddFirst(node);
                return true;
            } else {
                return false;
            }
        }
    }
}