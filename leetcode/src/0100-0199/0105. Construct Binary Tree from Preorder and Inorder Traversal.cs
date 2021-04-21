using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0105 {
        public TreeNode BuildTree(int[] preorder, int[] inorder) {
            if (preorder.Length < 1) {
                return null;
            }
            if (preorder.Length > 1) {
                for (var i = 0; i < inorder.Length; ++i) {
                    if (inorder[i] == preorder[0]) {
                        var lInorder = inorder.Take(i + 0).ToArray();
                        var rInorder = inorder.Skip(i + 1).ToArray();
                        var lPreorder = preorder.Skip(1).Take(lInorder.Length).ToArray();
                        var rPreorder = preorder.Skip(1).Skip(lInorder.Length).ToArray();
                        return new TreeNode(preorder[0]) {
                            left  = BuildTree(lPreorder, lInorder),
                            right = BuildTree(rPreorder, rInorder)
                        };
                    }
                }
                throw new InvalidOperationException();
            }
            return new TreeNode(preorder[0]);
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
