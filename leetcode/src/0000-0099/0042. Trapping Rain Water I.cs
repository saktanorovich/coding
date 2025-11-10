/*
Consider the following picture:
             _ _
            |7 7|_
   _        |    6
  |5|      _|
  | |    _|4
 _| |  _|3
 2  |_|2
 ____1____________
 0 1 2 3 4 5 6 7 8

In this picture we have walls of different heights. This picture is
represented by an array of integers, where the value at each index
is the height of the wall. The picture above is represented with an
array as [2,5,1,2,3,4,7,7,6].

Now imagine it rains. How much water is going to be accumulated in
puddles between walls?

We count volume in square blocks of 1x1. So in the picture above,
everything to the left of index 1 spills out. Water to the right of
index 7 also spills out. We are left with a puddle between 1 and 6
and the volume is 10.
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace coding.leetcode {
    public class Solution_0024 {
        public int Trap(int[] height) {
            if (height.Length > 2) {
                return Trap(height, 0, height.Length - 1);
            } else {
                return 0;
            }
        }

        private int Trap(int[] height, int le, int ri) {
            var volume = 0;
            var leMax = height[le];
            var riMax = height[ri];
            while (le < ri) {
                if (leMax < riMax) {
                    for (++le; height[le] < leMax; ++le) {
                        volume += leMax - height[le];
                    }
                    leMax = height[le];
                } else {
                    for (--ri; height[ri] < riMax; --ri) {
                        volume += riMax - height[ri];
                    }
                    riMax = height[ri];
                }
            }
            return volume;
        }
    }
}
