/**
K caterpillars are eating their way through N leaves, each
caterpillar falls from leaf to leaf in a unique sequence:

* All caterpillars start at a twig at position 0 and falls onto
  the leaves at position between 1 and N.
* Each caterpillar j has as associated jump number Aj.
* A caterpillar with jump number j eats leaves at positions
  that are multiple of j.
* It will proceed in the order j, 2j, 3j…. till it reaches
  the end of the leaves and it stops and build its cocoon.

Given a set A of K elements K ≤ 15, N ≤ 10^9, we need to
determine the number of uneaten leaves.

Input:
N K
a1 a2 .. aK

Output: The integer number of uneaten leaves.

Sample Input
10 3
2 4 5

Sample Output
4

Explanation: [2, 4, 5] is a j member jump numbers, all leaves
which are multiple of 2,4, and 5 are eaten, leaves 1,3,7,9 are
left, and thus the answer is 4.
*/
using System;

namespace interview.hackerrank {
    public class Caterpillars {
        public int count(int numOfLeafs, int[] jumps) {
            var eaten = 0;
            for (var set = 1; set < 1 << jumps.Length; ++set) {
                var sign = 1;
                var mult = 1;
                for (var i = 0; i < jumps.Length; ++i) {
                    if ((set & (1 << i)) != 0) {
                        if (1L * mult * (jumps[i] / gcd(mult, jumps[i])) > numOfLeafs) {
                            goto next;
                        }
                        mult = lcm(mult, jumps[i]);
                        sign = -sign;
                    }
                }
                eaten += sign * (numOfLeafs / mult);
                next:;
            }
            return numOfLeafs - Math.Abs(eaten);
        }

        private int gcd(int a, int b) {
            while (a != 0 && b != 0) {
                if (a > b) {
                    a %= b;
                }
                else {
                    b %= a;
                }
            }
            return a + b;
        }

        private int lcm(int a, int b) {
            return a * (b / gcd(a, b));
        }
    }
}

/*
9         Testcase 0 20         : 5 8 6 3 5
3636      Testcase 1 5555       : 45 41 17 50 56 61 4 80
1055      Testcase 2 1234       : 32 17 37 86 86 45
7182880   Testcase 3 9876554    : 28 22 65 87 6 68 81 86 35
3050880   Testcase 4 4123412    : 28 67 79 92 16 33 60 43 9 64 99
493768726 Testcase 5 999999999  : 86 57 3 94 50 33 5 65 97 99 20 54 57 23 23
63        Testcase 6 100        : 77 22 3
4         Testcase 7 10         : 2 4 5
6755      Testcase 8 10000      : 77 7 46 52 6 70 50 88
693190581 Testcase 9 987654678  : 72 16 83 11 65 62 43 89 15 55 72 27 67 32
*/