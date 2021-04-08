using System;
using System.Collections.Generic;
using System.Text;

namespace coding.leetcode {
    public class Solution_0239 {
        public int[] MaxSlidingWindow(int[] nums, int k) {
            var res = new List<int>();
            var que = new Queue(k);
            for (var i = 0; i < nums.Length; ++i) {
                que.Push(nums[i]);
                if (que.Cnt == k) {
                    res.Add(que.Max);
                    que.Poll();
                }
            }
            return res.ToArray();
        }

        private class Queue {
            private readonly Stack iStack;
            private readonly Stack oStack;

            public Queue(int capacity) {
                iStack = new Stack(capacity);
                oStack = new Stack(capacity);
            }

            public int Cnt => iStack.Cnt + oStack.Cnt;

            public int Max {
                get {
                    if (iStack.Cnt == 0) return oStack.Max;
                    if (oStack.Cnt == 0) return iStack.Max;

                    if (iStack.Max > oStack.Max) {
                        return iStack.Max;
                    } else {
                        return oStack.Max;
                    }
                }
            }

            public void Push(int x) {
                iStack.Push(x);
            }

            public void Poll() {
                if (oStack.Cnt == 0) {
                    while (iStack.Cnt > 0) {
                        oStack.Push(iStack.Pop());
                    }
                }
                oStack.Pop();
            }
        }

        private class Stack {
            private readonly int[] val;
            private readonly int[] max;

            public Stack(int capacity) {
                val = new int[capacity];
                max = new int[capacity];
                Cnt = 0;
            }

            public int Cnt;
            public int Max => max[Cnt - 1];

            public void Push(int x) {
                val[Cnt] = x;
                max[Cnt] = x;
                if (Cnt > 0) {
                    if (max[Cnt] < max[Cnt - 1]) {
                        max[Cnt] = max[Cnt - 1];
                    }
                }
                Cnt++;
            }

            public int Pop() {
                var res = val[Cnt - 1];
                Cnt--;
                return res;
            }
        }
    }
}
