using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0735 {
        public int[] AsteroidCollision(int[] asteroids) {
            var state = new int[asteroids.Length];
            var stack = new Stack<int>();
            for (var i = 0; i < asteroids.Length; ++i) {
                state[i] = asteroids[i];
                if (asteroids[i] > 0) {
                    stack.Push(i);
                } else {
                    while (stack.Count > 0) {
                        var k = stack.Peek();
                        if (asteroids[k] > -asteroids[i]) {
                            state[i] = 0;
                            break;
                        }
                        if (asteroids[k] < -asteroids[i]) {
                            state[k] = 0;
                            stack.Pop();
                        } else {
                            state[i] = 0;
                            state[k] = 0;
                            stack.Pop();
                            break;
                        }
                    }
                }
            }
            return state.Where(x => x != 0).ToArray();
        }
    }
}
