using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_2071 {
        public int MaxTaskAssign(int[] tasks, int[] workers, int pills, int strength) {
            Array.Sort(tasks);
            Array.Sort(workers);
            int lo = 0, hi = Math.Min(tasks.Length, workers.Length);
            while (lo < hi) {
                var k = (lo + hi + 1) / 2;
                // try to assign k easiest tasks to k strengthest workers
                if (assign(tasks, workers, pills, strength, k)) {
                    lo = k;
                } else {
                    hi = k - 1;
                }
            }
            return lo;
        }

        private bool assign(int[] tasks, int[] workers, int pills, int strength, int k) {
            var deque = new LinkedList<int>();
            for (int task = k - 1, worker = workers.Length - 1; task >= 0; --task) {
                // find workers who can be assigned to the current hardest task
                while (worker >= workers.Length - k) {
                    if (workers[worker] + strength >= tasks[task]) {
                        deque.AddLast(worker--);
                    } else break;
                }
                if (deque.Count == 0) {
                    return false;
                }
                if (workers[deque.First.Value] >= tasks[task]) {
                    deque.RemoveFirst();
                } else {
                    deque.RemoveLast();
                    if (--pills < 0) {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
