using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0124 {
        public int MaxPathSum(TreeNode root) {
            var best = int.MinValue;
            MaxPathSum(root, ref best);
            return best;
        }

        private int MaxPathSum(TreeNode root, ref int best) {
            if (root != null) {
                var l = Math.Max(0, MaxPathSum(root.left , ref best));
                var r = Math.Max(0, MaxPathSum(root.right, ref best));
                best = Math.Max(best, root.val + l + r);
                return Math.Max(root.val + l, root.val + r);
            }
            return int.MinValue;
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
