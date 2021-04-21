/**
Range Minimum Query (RMQ) is a set of problems which deals with
finding a property (here minimum) of a range. Segment Tree can be
very helpful when solving with such problems. A segment tree is a tree
like data structure which is used to store the information about
intervals. Here's the [https://en.wikipedia.org/wiki/Segment_tree] of it.

You are given a array of N integers, a0, a1, .., aN-1. And you are given
a list of ranges. For each range (l, r) you have to find the minimum
value between range al, al+1, al+2, .., ar.

Input: First line will contain two integers N M representing length of array
and number of queries. Then in next line there are N space separated integers
which represent the array a0, a1, .., aN. Then M line follows. Each M line
will contain two integers l r representing a range.

Output: For each range [l, r] you have to print the minimum integer in subarray
a[l, r] in separate line.

Constraints:
1 ≤ N, M ≤ 10^5
-10^5 ≤ ai ≤ 10^5
0 ≤ l ≤ r < N

Sample Input
10 5
10 20 30 40 11 22 33 44 15 5
0 5
1 2
8 9
0 9
4 6

Sample Output
10
20
5
5
11

Explanation
For range (0, 5), subarray will be [10, 20, 30, 40, 11, 22]. So minimum value will be 10.
For range (1, 2), subarray will be [20, 30]. Minimum value = 20.
For range (8, 9), subarray is [15, 5]. Minimum value = 5.
For range (0, 9), Here we have to find the minimum (5) of the whole array.
For range (3, 5), subarray is [40, 11, 22]. Minimum value = 11.
*/
using System;

namespace interview.hackerrank {
    public class RangeMinimumQuery {
        public int[] rmq(int[] a, string[] queries) {
            var lo = new int[queries.Length];
            var hi = new int[queries.Length];
            for (var i = 0; i < queries.Length; ++i) {
                var items = Array.ConvertAll(queries[i].Split(' '), int.Parse);
                lo[i] = items[0];
                hi[i] = items[1];
            }
            return rmq(a, a.Length, lo, hi, lo.Length);
        }

        private int[] rmq(int[] a, int n, int[] l, int[] h, int m) {
            var logn = new int[n + 1];
            for (var i = 2; i <= n; ++i) {
                logn[i] = logn[i >> 1] + 1;
            }
            var min = new int[n, logn[n] + 1];
            for (var i = 0; i < n; ++i) {
                min[i, 0] = a[i];
            }
            for (var k = 1; k <= logn[n]; ++k) {
                for (var i = 0; i + (1 << k) <= n; ++i) {
                    min[i, k] = Math.Min(min[i, k - 1], min[i + (1 << k - 1), k - 1]);
                }
            }
            var result = new int[m];
            for (var i = 0; i < m; ++i) {
                result[i] = Math.Min(min[l[i], logn[h[i] - l[i] + 1]],
                    min[h[i] - (1 << logn[h[i] - l[i] + 1]) + 1, logn[h[i] - l[i] + 1]]);
            }
            return result;
        }
    }
}
