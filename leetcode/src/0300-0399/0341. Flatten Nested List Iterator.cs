using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0341 {
        public class NestedIterator {
            private readonly Stack<IEnumerator<NestedInteger>> stack;

            public NestedIterator(IList<NestedInteger> nestedList) {
                stack = new Stack<IEnumerator<NestedInteger>>();
                stack.Push(nestedList.GetEnumerator());
            }

            public bool HasNext() {
                if (stack.Count > 0) {
                    var iterator = stack.Peek();
                    if (iterator.MoveNext()) {
                        var element = iterator.Current;
                        if (element.IsInteger()) {
                            return true;
                        }
                        stack.Push(element.GetList().GetEnumerator());
                        return HasNext();
                    } else {
                        stack.Pop();
                        return HasNext();
                    }
                }
                return false;
            }

            public int Next() {
                return stack.Peek().Current.GetInteger();
            }
        }

        public interface NestedInteger {
            /// <summary>
            /// Return true if this NestedInteger holds a single integer, rather than a nested list.
            /// </summary>
            bool IsInteger();

            /// <summary>
            /// Return the single integer that this NestedInteger holds, if it holds a single integer
            /// otherwise undefined if this NestedInteger holds a nested list.
            /// </summary>
            int GetInteger();

            /// <summary>
            /// Return the nested list that this NestedInteger holds, if it holds a nested list
            /// otherwise null if this NestedInteger holds a single integer.
            /// </summary>
            IList<NestedInteger> GetList();
        }
    }
}
