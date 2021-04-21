using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0295 {
        public class MedianFinder {
            private PriorityQueue<int, int> ls;
            private PriorityQueue<int, int> rs;

            public MedianFinder() {
                ls = new PriorityQueue<int, int>();
                rs = new PriorityQueue<int, int>();
            }
    
            public void AddNum(int num) {
                ls.Enqueue(num, -num);
                while (ls.Count > rs.Count) {
                    rs.Enqueue(ls.Peek(), +ls.Peek());
                    ls.Dequeue();
                }
                while (rs.Count > ls.Count) {
                    ls.Enqueue(rs.Peek(), -rs.Peek());
                    rs.Dequeue();
                }
            }
    
            public double FindMedian() {
                var count = ls.Count + rs.Count;
                if (count % 2 == 0) {
                    return 0.5 * (ls.Peek() + rs.Peek());
                }
                return ls.Peek();
            }
        }
    }
}
