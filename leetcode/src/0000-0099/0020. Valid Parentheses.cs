using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0020 {
        public bool IsValid(string s) {
            var stack = new Stack<char>();
            foreach (var c in s) {
                switch (c) {
                    case ')':
                        if (!pop(stack, '(')) return false; break;
                    case '}':
                        if (!pop(stack, '{')) return false; break;
                    case ']':
                        if (!pop(stack, '[')) return false; break;
                    default:
                        stack.Push(c);
                        break;
                }
            }
            return stack.Count == 0;
        }

        private bool pop(Stack<char> s, char c) {
            if (s.Count > 0 && s.Peek() == c) {
                s.Pop();
                return true;
            } else {
                return false;
            }
        }
    }
}
