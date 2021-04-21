/*
You are planning production for an order. You have a number of machines that each have a fixed number of days to produce an item. Given that all the machines operate simultaneously, determine the minimum number of days to produce the required order.

For example, you have to produce  items. You have three machines that take  days to produce an item. The following is a schedule of items produced:

Day Production  Count
2   2               2
3   1               3
4   2               5
6   3               8
8   2              10
It takes  days to produce  items using these machines.

Function Description

Complete the minimumTime function in the editor below. It should return an integer representing the minimum number of days required to complete the order.

minimumTime has the following parameter(s):

machines: an array of integers representing days to produce one item per machine
goal: an integer, the number of items required to complete the order
Input Format

The first line consist of two integers  and , the size of  and the target production.
The next line contains  space-separated integers, .

Constraints

Output Format

Return the minimum time required to produce items considering all machines work simultaneously.

Sample Input 0

2 5
2 3
Sample Output 0

6
Explanation 0

In  days  can produce  items and  can produce  items. This totals up to .

Sample Input 1

3 10
1 3 4
Sample Output 1

7
Explanation 1

In  minutes,  can produce  items,  can produce  items and  can produce  item, which totals up to .

Sample Input 2

3 12
4 5 6
Sample Output 2

20
Explanation 2

In  days  can produce  items,  can produce , and  can produce .
*/
import java.io.*;
import java.math.*;
import java.util.*;

public class Solution {
    private static long minTime(long[] machines, long goal) {
        BigInteger lo = BigInteger.ONE;
        BigInteger hi = BigInteger.valueOf(Long.MAX_VALUE);
        BigInteger gb = BigInteger.valueOf(goal);
        while (lo.compareTo(hi) < 0) {
            BigInteger days = lo.add(hi).divide(BigInteger.valueOf(2));
            if (target(machines, days).compareTo(gb) < 0) {
                lo = days.add(BigInteger.ONE);
            } else {
                hi = days;
            }
        }
        return lo.longValue();
    }
  
    private static BigInteger target(long[] machines, BigInteger days) {
        BigInteger res = BigInteger.ZERO;
        for (long machine : machines) {
            BigInteger items = days.divide(BigInteger.valueOf(machine));
            res = res.add(items);
        }
        return res;
    }

    public static void main(String[] args) throws IOException {
        Scanner reader = new Scanner(System.in);
        PrintWriter writer = new PrintWriter(System.out);

        int n = reader.nextInt();
        long goal = reader.nextLong();
        long[] machines = new long[n];
        for (int i = 0; i < n; i++) {
            machines[i] = reader.nextLong();
        }
        long ans = minTime(machines, goal);

        writer.println(ans);
        writer.close();
        reader.close();
    }
}