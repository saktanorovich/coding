using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1851 {
        public int[] MinInterval(int[][] intervals, int[] queries) {
            var order = Enumerable.Range(0, queries.Length).ToArray();
            Array.Sort(queries, order);
            Array.Sort(intervals, (x, y) => x[0] - y[0]);
            var result = Enumerable.Repeat(-1, queries.Length).ToArray();
            var pqueue = new PriorityQueue<int[], int>();
            for (int q = 0, i = 0; q < queries.Length; ++q) {
                for (; i < intervals.Length; ++i) {
                    if (intervals[i][0] <= queries[q]) {
                        if (queries[q] <= intervals[i][1]) {
                            pqueue.Enqueue(intervals[i], intervals[i][1] - intervals[i][0] + 1);
                        }
                    } else break;
                }
                while (pqueue.Count > 0) {
                    if (pqueue.Peek()[1] < queries[q]) {
                        pqueue.Dequeue();
                    } else break;
                }
                if (pqueue.TryPeek(out _, out var length)) {
                    result[order[q]] = length;
                }
            }
            return result;
        }
    }
}