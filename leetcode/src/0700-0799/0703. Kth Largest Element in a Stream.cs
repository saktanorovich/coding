using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0703 {
        public class KthLargest {
            private readonly PriorityQueue<int, int> heap;
            private readonly int k;

            public KthLargest(int k, int[] nums) {
                heap = new PriorityQueue<int, int>();
                foreach (var num in nums) {
                    heap.Enqueue(num, num);
                }
                while (heap.Count > k) {
                    heap.Dequeue();
                }
                this.k = k;
            }
            
            public int Add(int val) {
                heap.Enqueue(val, val);
                if (heap.Count > k) {
                    heap.Dequeue();
                }
                return heap.Peek();
            }
        }
    }
}
