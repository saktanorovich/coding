/**
public class Node {
    public bool val;
    public bool isLeaf;
    public Node topLeft;
    public Node topRight;
    public Node bottomLeft;
    public Node bottomRight;

    public Node() { }
    public Node(bool val, bool isLeaf,
        Node topLeft,
        Node topRight,
        Node bottomLeft,
        Node bottomRight)
    {
        val         = this.val;
        isLeaf      = this.isLeaf;
        topLeft     = this.topLeft;
        topRight    = this.topRight;
        bottomLeft  = this.bottomLeft;
        bottomRight = this.bottomRight;
    }
}
*/
public class Solution {
    public Node Intersect(Node qt1, Node qt2) {
        if (qt1.isLeaf &&  qt1.val) return qt1;
        if (qt2.isLeaf &&  qt2.val) return qt2;
        if (qt1.isLeaf && !qt1.val) return qt2;
        if (qt2.isLeaf && !qt2.val) return qt1;

        var tl = Intersect(qt1.topLeft,     qt2.topLeft);
        var tr = Intersect(qt1.topRight,    qt2.topRight);
        var bl = Intersect(qt1.bottomLeft,  qt2.bottomLeft);
        var br = Intersect(qt1.bottomRight, qt2.bottomRight);

        if (tl.isLeaf && tr.isLeaf && bl.isLeaf && br.isLeaf) {
            if (
                tl.val == tr.val
            &&  tl.val == bl.val
            &&  tl.val == br.val) {
                return new Node(tl.val, true, null, null, null, null);
            }
        }
        return new Node(false, false, tl, tr, bl, br);
    }
}
