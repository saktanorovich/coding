using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0207 {
        public bool CanFinish(int numCourses, int[][] prerequisites) {
            var graph = new List<int>[numCourses];
            var count = new int[numCourses];
            for (var i = 0; i < numCourses; ++i) {
                graph[i] = new List<int>();
            }
            foreach (var req in prerequisites) {
                var a = req[0];
                var b = req[1];
                graph[b].Add(a);
                count[a] ++;
            }
            var queue = new Queue<int>();
            for (var i = 0; i < numCourses; ++i) {
                if (count[i] == 0) {
                    queue.Enqueue(i);
                }
            }
            while (queue.Count > 0) {
                var curr = queue.Dequeue();
                foreach (var next in graph[curr]) {
                    count[next] --;
                    if (count[next] == 0) {
                        queue.Enqueue(next);
                    }
                }
            }
            return count.All(c => c == 0);
        }
    }
}
