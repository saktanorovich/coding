using System;
using System.Collections.Generic;
using System.Text;

namespace coding.leetcode {
    public class Solution_0621 {
        public int LeastInterval(char[] tasks, int n) {
            var count = new int[26]; 
            foreach (var task in tasks) {
                count[task - 'A']++;
            }
            var current = new PriorityQueue<int, int>(Comparer<int>.Create((a, b) => b - a));
            var pending = new Queue<(int task, int time)>();
            for (var i = 0; i < 26; ++i) {
                if (count[i] > 0) {
                    current.Enqueue(count[i], count[i]);
                }
            }
            var time = 0;
            while (current.Count > 0 || pending.Count > 0) {
                while (pending.Count > 0) {
                    var item = pending.Peek();
                    if (item.time <= time) {
                        current.Enqueue(item.task, item.task);
                        pending.Dequeue();
                    } else break;
                }
                time = time + 1;
                if (current.TryDequeue(out var task, out _)) {
                    if (task > 1) {
                        pending.Enqueue((task - 1, time + n));
                    }    
                }
            }
            return time;
        }
    }
}
