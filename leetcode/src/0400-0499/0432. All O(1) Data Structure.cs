using System;
using System.Collections.Generic;
using System.Linq;

namespace coding.leetcode {
    public class AllOne {
        private readonly Dictionary<int, LinkedListNode<HashSet<string>>> freq;
        private readonly Dictionary<string, int> vals;
        private readonly LinkedList<HashSet<string>> list;

        public AllOne() {
            this.freq = new Dictionary<int, LinkedListNode<HashSet<string>>>();
            this.vals = new Dictionary<string, int>();
            this.list = new LinkedList<HashSet<string>>();
            list.AddFirst(new HashSet<string> {
                String.Empty
            });
            freq[0] = list.First;
        }

        public void Inc(string key) {
            if (vals.ContainsKey(key)) {
                Detach(key);
            } else {
                Create(key);
            }
            Attach(key, +1);
        }

        public void Dec(string key) {
            if (vals.ContainsKey(key)) {
                Detach(key);
                if (vals[key] == 1) {
                    Remove(key);
                } else {
                    Attach(key, -1);
                }
            }
        }

        public string GetMaxKey() {
            if (list.Count > 1) {
                return list.Last.Value.First();
            } else {
                return String.Empty;
            }
        }

        public string GetMinKey() {
            if (list.Count > 1) {
                return list.First.Next.Value.First();
            } else {
                return String.Empty;
            }
        }

        private void Attach(string key, int inc) {
            var prev = vals[key];
            var curr = vals[key] + inc;
            if (freq.ContainsKey(curr) == false) {
                var node = new LinkedListNode<HashSet<string>>(new HashSet<string>());
                freq.Add(curr, node);
                if (curr > prev) {
                    list.AddAfter (freq[prev], freq[curr]);
                } else {
                    list.AddBefore(freq[prev], freq[curr]);
                }
            }
            freq[curr].Value.Add(key);
            var slot = freq[prev];
            if (slot.Value.Count == 0) {
                list.Remove(slot);
                freq.Remove(prev);
            }
            vals[key] = curr;
        }

        private void Detach(string key) {
            var hits = vals[key];
            var slot = freq[hits];
            slot.Value.Remove(key);
        }

        private void Create(string key) {
            vals[key] = 0;
        }

        private void Remove(string key) {
            vals.Remove(key);
            var slot = freq[1];
            if (slot.Value.Count == 0) {
                list.Remove(slot);
                freq.Remove(1);
            }
        }
    }
}