using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0235 {
        public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q) {
            while (root != null) {
                if (p.val < root.val && q.val < root.val) {
                    root = root.left;
                    continue;
                }
                if (p.val > root.val && q.val > root.val) {
                    root = root.right;
                    continue;
                }
                return root;
            }
            return root;
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
