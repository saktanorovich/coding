using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0210 {
        public int[] FindOrder(int numCourses, int[][] prerequisites) {
            var graph = new List<int>[numCourses];
            for (var i = 0; i < numCourses; ++i) {
                graph[i] = new List<int>();
            }
            var degree = new int[numCourses];
            foreach (var dependency in prerequisites) {
                var a = dependency[0];
                var b = dependency[1];
                graph[b].Add(a);
                degree[a]++;
            }
            var queue = new Queue<int>();
            for (var i = 0; i < numCourses; ++i) {
                if (degree[i] == 0) {
                    queue.Enqueue(i);
                }
            }
            var order = new List<int>();
            while (queue.Count > 0) {
                var curr = queue.Dequeue();
                order.Add(curr);
                if (order.Count == numCourses) {
                    return order.ToArray();
                }
                foreach (var next in graph[curr]) {
                    degree[next]--;
                    if (degree[next] == 0) {
                        queue.Enqueue(next);
                    }
                }
            }
            return new int[0];
        }
    }
}
