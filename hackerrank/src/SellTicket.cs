/**
There are N ticket windows in the railway station. The ith window has
ai tickets available. The price of a ticket is equal to the number of
tickets remaining in that window at that time.

What is the maximum amount of money the railway station can earn form
selling the first M tickets?

Input
N M
a1 a2 .. aN

Output
S

Sample Input
2 4
2 5

Sample Output
14

Explanation:
Maximum revenue would have been obtained if all 4 tickets were sold
from the the 2nd window at prices 5,4,3 and 2 resulting to total=14.
*/
using System;
using System.Linq;

namespace interview.hackerrank {
    public class SellTicket {
        public long maxEarn(int[] tickets, long sold) {
            Array.Sort(tickets);
            var earn = 0L;
            for (var price = tickets.Last(); sold > 0; --price) {
                var count = tickets.Length - find(tickets, price);
                earn += Math.Min(count, sold) * price;
                sold -= Math.Min(count, sold);
            }
            return earn;
        }

        private int find(int[] tickets, int price) {
            int lo = 0, hi = tickets.Length;
            while (lo < hi) {
                var x = (lo + hi) >> 1;
                if (tickets[x] < price) {
                    lo = x + 1;
                }
                else {
                    hi = x;
                }
            }
            return lo;
        }
    }
}
