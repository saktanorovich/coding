using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1381 {
        public class CustomStack {
            private readonly int[] stack;
            private readonly int[] delta;
            private int count;

            public CustomStack(int maxSize) {
                stack = new int[maxSize];
                delta = new int[maxSize];
            }
            
            public void Push(int x) {
                if (count < stack.Length) {
                    stack[count] = x;
                    delta[count] = 0;
                    count++;
                }
            }
            
            public int Pop() {
                if (count > 0) {
                    if (--count > 0) {
                        delta[count - 1] += delta[count];
                    }
                    return stack[count] + delta[count];
                }
                return -1;
            }
            
            public void Increment(int k, int val) {
                k = Math.Min(k, count);
                if (k > 0) {
                    delta[k - 1] += val;
                }
            }
        }
    }
}
