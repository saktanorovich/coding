using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0114 {
        public void Flatten(TreeNode root) {
            if (root != null) {
                Flatten(root.left);
                Flatten(root.right);
                if (root.left != null) {
                    var node = root.left;
                    while (node.right != null) {
                        node = node.right;
                    }
                    node.right = root.right;
                    root.right = root.left;
                    root.left  = null;
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
