using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0968 {
        public int MinCameraCover(TreeNode root) {
            var state = doit(root);
            return Math.Min(state[0], state[1]);
        }

        // 0 - no cams at the root
        // 1 - one cam at the root
        // 2 - root is not covered
        private int[] doit(TreeNode root) {
            if (root == null) {
                return new[] { 0, 1000, 0 };
            }

            var le = doit(root.left);
            var ri = doit(root.right);

            var leOpt = Math.Min(le[0], le[1]);
            var riOpt = Math.Min(ri[0], ri[1]);

            var r0 = Math.Min(le[1] + riOpt, ri[1] + leOpt);

            var r1 = 1;
            r1 += Math.Min(leOpt, le[2]);
            r1 += Math.Min(riOpt, ri[2]);

            var r2 = le[0] + ri[0];

            return new[] { r0, r1, r2 };
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
