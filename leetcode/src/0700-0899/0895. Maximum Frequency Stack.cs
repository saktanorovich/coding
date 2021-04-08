using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class FreqStack {
        private readonly Dictionary<int, int> freq;
        private readonly List<Stack<int>> list;
        private int head;

        public FreqStack() {
            this.freq = new Dictionary<int, int>();
            this.list = new List<Stack<int>>();
            this.head = -1;
        }

        public void Push(int x) {
            if (freq.ContainsKey(x) == false) {
                freq.Add(x, 0);
            } else {
                freq[x]++;
            }
            if (list.Count <= freq[x]) {
                list.Add(new Stack<int>());
            }
            list[freq[x]].Push(x);
            if (head < freq[x]) {
                head = freq[x];
            }
        }
    
        public int Pop() {
            var x = list[head].Pop();
            if (list[head].Count == 0) {
                head--;
            }
            freq[x]--;
            return x;
        }
    }
}