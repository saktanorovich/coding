using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1834 {
        public int[] GetOrder(int[][] tasks) {
            var queue1 = new SortedSet<int[]>(
                Comparer<int[]>.Create((int[] t1, int[] t2) => {
                    if (t1[0] != t2[0]) {
                        return t1[0] - t2[0];
                    }
                    if (t1[1] != t2[1]) {
                        return t1[1] - t2[1];
                    }
                    return t1[2] - t2[2];
                }));
            for (var i = 0; i < tasks.Length; ++i) {
                queue1.Add(new[] { tasks[i][0], tasks[i][1], i });
            }
            var queue2 = new SortedSet<int[]>(
                Comparer<int[]>.Create((int[] t1, int[] t2) => {
                    if (t1[1] != t2[1]) {
                        return t1[1] - t2[1];
                    }
                    return t1[2] - t2[2];
                }));
            var order = new List<int>();
            for (long time = 0; queue1.Count > 0 || queue2.Count > 0;) {
                while (queue1.Count > 0) {
                    var task = queue1.Min;
                    if (task[0] <= time) {
                        queue1.Remove(task);
                        queue2.Add(task);
                    }
                    else break;
                }
                if (queue2.Count == 0) {
                    time = queue1.Min[0];
                } else {
                    var task = queue2.Min;
                    queue2.Remove(task);
                    order.Add(task[2]);
                    time += task[1];
                }
            }
            return order.ToArray();
        }
    }
}
