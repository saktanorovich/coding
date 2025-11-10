using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class BSTIterator {
        private readonly Stack<TreeNode> stack;

        public BSTIterator(TreeNode root) {
            stack = new Stack<TreeNode>();
            Push(root);
        }
        
        /** @return the next smallest number */
        public int Next() {
            var next = stack.Pop();
            Push(next.right);
            return next.val;
        }
        
        /** @return whether we have a next smallest number */
        public bool HasNext() {
            return stack.Count > 0;
        }

        private void Push(TreeNode node) {
            while (node != null) {
                stack.Push(node);
                node = node.left;
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
