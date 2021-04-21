/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     int val;
 *     TreeNode left;
 *     TreeNode right;
 *     TreeNode() { }
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
    public String getDirections(TreeNode root, int src, int dst) {
        var lca = find(root, src, dst);
        var pathToSrc = path(lca, src, new StringBuilder());
        var pathToDst = path(lca, dst, new StringBuilder());
        var path = new StringBuilder();
        for (var c : pathToSrc.toString().toCharArray()) {
            path.append('U');
        }
        path.append(pathToDst);
        return path.toString();
    }

    private StringBuilder path(TreeNode node, int val, StringBuilder path) {
        if (node == null) {
            return null;
        }
        if (node.val == val) {
            return path;
        }
        if (have(node.left,  val, 'L', path)) return path;
        if (have(node.right, val, 'R', path)) return path;
        return null;
    }

    private boolean have(TreeNode node, int val, char dir, StringBuilder path) {
        path.append(dir);
        if (path(node, val, path) != null) {
            return true;
        }
        path.setLength(path.length() - 1);
        return false;
    }

    private TreeNode find(TreeNode node, int src, int dst) {
        if (node == null) {
            return null;
        }
        if (node.val == src || node.val == dst) {
            return node;
        }
        var l = find(node.left,  src, dst);
        var r = find(node.right, src, dst);
        if (l != null && r != null) {
            return node;
        }
        return l == null ? r : l;
    }
}