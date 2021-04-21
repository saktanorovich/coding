/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     TreeNode() {
 *     }
 *     TreeNode(int val) {
 *         this.val = val;
 *     }
 *     TreeNode(int val, TreeNode left, TreeNode right) {
 *         this.val = val;
 *         this.left = left;
 *         this.right = right;
 *     }
 * }
 */
class Solution {
    public int diameterOfBinaryTree(TreeNode root) {
        diameter = 0;
        dfs(root);
        return diameter;
    }

    private int dfs(TreeNode root) {
        if (root == null) {
            return 0;
        }
        var h1 = dfs(root.left);
        var h2 = dfs(root.right);
        diameter = Math.max(h1 + h2, diameter);
        return 1 + Math.max(h1 , h2);
    }

    private int diameter;
}