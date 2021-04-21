using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1845 {
        public class SeatManager {
            private readonly PriorityQueue<int, int> seats;

            public SeatManager(int n) {
                seats = new PriorityQueue<int, int>();
                while (n > 0) {
                    seats.Enqueue(n, n);
                    n = n - 1;
                }
            }
            
            public int Reserve() {
                return seats.Dequeue();
            }
            
            public void Unreserve(int seat) {
                seats.Enqueue(seat, seat);
            }
        }
    }
}
