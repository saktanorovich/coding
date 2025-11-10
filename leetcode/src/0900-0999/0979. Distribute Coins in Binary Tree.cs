using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0979 {
        public int DistributeCoins(TreeNode root) {
            if (root != null) {
                var ans = 0;
                if (root.val % 2 == 0) {
                    doit1(root, ref ans);
                } else {
                    doit2(root, ref ans);
                }
                return ans;
            }
            return 0;
        }

        private int doit1(TreeNode root, ref int res) {
            if (root != null) {
                var le = doit1(root.left , ref res);
                var ri = doit1(root.right, ref res);
                res += Math.Abs(le) + Math.Abs(ri);
                return root.val - 1 + le + ri;
            }
            return 0;
        }

        private int[] doit2(TreeNode root, ref int res) {
            if (root != null) {
                var le = doit2(root.left , ref res);
                var ri = doit2(root.right, ref res);
                res += Math.Abs(le[0] - le[1]);
                res += Math.Abs(ri[0] - ri[1]);
                return new[] {
                    le[0] + ri[0] + 1,
                    le[1] + ri[1] + root.val
                };
            }
            return new[] { 0, 0 };
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
