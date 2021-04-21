/**
// Definition for a Node.
public class Node {
    public int val;
    public List<Node> children;
    public Node() {}
    public Node(int val) {
        this.val = val;
    }
    public Node(int val, List<Node> children) {
        this.val = val;
        this.children = children;
    }
};
*/
class Solution {
    public int maxDepth(Node root) {
        if (root == null) {
            return 0;
        }
        var best = 0;
        for (var child : root.children) {
            var h = maxDepth(child);
            if (best < h) {
                best = h;
            }
        }
        return 1 + best;
    }
}