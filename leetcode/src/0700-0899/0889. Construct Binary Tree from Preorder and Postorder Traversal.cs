using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0889 {
        public TreeNode ConstructFromPrePost(int[] pre, int[] post) {
            if (pre.Length < 1) {
                return null;
            }
            if (pre.Length > 1) {
                for (var i = 0; i < post.Length; ++i) {
                    if (post[i] == pre[1]) {
                        var lPre = pre.Skip(1).Take(i + 1).ToArray();
                        var rPre = pre.Skip(1).Skip(i + 1).ToArray();
                        var lPost = post.Take(lPre.Length);
                        var rPost = post.Skip(lPre.Length).Take(rPre.Length);
                        return new TreeNode(pre[0]) {
                            left  = ConstructFromPrePost(lPre, lPost.ToArray()),
                            right = ConstructFromPrePost(rPre, rPost.ToArray())
                        };
                    }
                }
            }
            return new TreeNode(pre[0]);
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
