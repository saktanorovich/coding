using System;
using System.Collections.Generic;

namespace coding.leetcode {
    public class LFUCache {
        private readonly Dictionary<int, LinkedListNode<int>> addr;
        private readonly Dictionary<int, LinkedList<int>> list;
        private readonly Dictionary<int, int> freq;
        private readonly Dictionary<int, int> vals;
        private readonly int capacity;
        private int numItems;
        private int minIndex;

        public LFUCache(int capacity) {
            this.capacity = capacity;
            this.numItems = 0;
            this.minIndex = 0;
            this.addr = new Dictionary<int, LinkedListNode<int>>();
            this.list = new Dictionary<int, LinkedList<int>>();
            this.freq = new Dictionary<int, int>();
            this.vals = new Dictionary<int, int>();
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
                var node = Create(key);
                Attach(node);
                if (minIndex < 1) {
                    minIndex = 1;
                }
                if (numItems > capacity) {
                    Remove(list[minIndex].Last);
                }
                minIndex = 1;
            }
            vals[key] = value;
        }

        private bool Warm(int key) {
            if (addr.TryGetValue(key, out var node)) {
                Detach(node);
                Attach(node);
                return true;
            } else {
                return false;
            }
        }

        private LinkedListNode<int> Create(int key) {
            var node = new LinkedListNode<int>(key);
            freq[key] = 1;
            addr[key] = node;
            return node;
        }

        private void Attach(LinkedListNode<int> node) {
            var f = freq[node.Value];
            if (list.ContainsKey(f) == false) {
                list.Add(f, new LinkedList<int>());
            }
            list[f].AddFirst(node);
            ++numItems;
        }

        private void Detach(LinkedListNode<int> node) {
            var hits = freq[node.Value];
            var slot = list[hits];
            slot.Remove(node);
            if (slot.Count == 0) {
                list.Remove(hits);
                if (minIndex == hits) {
                    minIndex++;
                }
            }
            freq[node.Value]++;
            --numItems;
        }

        private void Remove(LinkedListNode<int> node) {
            Detach(node);
            addr.Remove(node.Value);
            freq.Remove(node.Value);
            vals.Remove(node.Value);
        }
    }
}