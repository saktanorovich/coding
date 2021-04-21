using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1305 {
        public IList<int> GetAllElements(TreeNode root1, TreeNode root2) {
            var it1 = new TreeNodeEnumerator(root1);
            var it2 = new TreeNodeEnumerator(root2);
            var res = new List<int>();
            while (it1.HasNext() && it2.HasNext()) {
                if (it1.Peek() < it2.Peek()) {
                    res.Add(it1.Next());
                } else {
                    res.Add(it2.Next());
                }
            }
            while (it1.HasNext()) {
                res.Add(it1.Next());
            }
            while (it2.HasNext()) {
                res.Add(it2.Next());
            }
            return res;
        }

        private class TreeNodeEnumerator {
            private readonly Stack<TreeNode> stack;

            public TreeNodeEnumerator(TreeNode root) {
                stack = new Stack<TreeNode>();
                if (root != null) {
                    Push(root);
                }
            }

            public bool HasNext() {
                return stack.Count > 0;
            }

            public int Next() {
                var next = stack.Pop();
                Push(next.right);
                return next.val;
            }

            public int Peek() {
                var head = stack.Peek();
                return head.val;
            }

            private void Push(TreeNode node) {
                while (node != null) {
                    stack.Push(node);
                    node = node.left;
                }
            }
        }

        public class TreeNode {
            public int val;
            public TreeNode left;
            public TreeNode right;

            public TreeNode(int x) {
                val = x;
            }
        }
    }
}
