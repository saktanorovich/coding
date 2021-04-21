using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1123 {
        public TreeNode LcaDeepestLeaves(TreeNode root) {
            // if we have deepest leaves [l1,l2,..,ln] then lca of
            // these leaves is the same as lca of leaves l1 and ln
            return lca(root, 0).node;
        }

        private (TreeNode node, int deep) lca(TreeNode root, int d) {
            if (root == null) {
                return (null, d);
            }
            if (root.left == null && root.right == null) {
                return (root, d);
            }
            var l = lca(root.left,  d + 1);
            var r = lca(root.right, d + 1);
            if (l.node == null) return r;
            if (r.node == null) return l;
            if (l.deep == r.deep) {
                return (root, l.deep);
            }
            return l.deep > r.deep ? l : r;
        }

        public class TreeNode {
            public int val;
            public TreeNode left;
            public TreeNode right;

            public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null) {
                this.val = val;
                this.left = left;
                this.right = right;
            }
        }
    }
}