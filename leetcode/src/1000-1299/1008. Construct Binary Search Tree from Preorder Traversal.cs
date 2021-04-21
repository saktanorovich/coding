using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1008 {
        public TreeNode BstFromPreorder(int[] preorder) {
            if (preorder.Length < 1) {
                return null;
            }
            if (preorder.Length > 1) {
                for (var i = 1; i < preorder.Length; ++i) {
                    if (preorder[i] > preorder[0]) {
                        return new TreeNode(preorder[0]) {
                            left  = BstFromPreorder(preorder.Skip(1).Take(i - 1).ToArray()),
                            right = BstFromPreorder(preorder.Skip(1).Skip(i - 1).ToArray())
                        };
                    }
                }
                return new TreeNode(preorder[0]) {
                    left = BstFromPreorder(preorder.Skip(1).ToArray())
                };
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
