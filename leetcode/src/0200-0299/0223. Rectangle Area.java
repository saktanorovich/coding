class Solution {
    public int computeArea(int ax1, int ay1, int ax2, int ay2, int bx1, int by1, int bx2, int by2) {
        var s1 = (ax2 - ax1) * (ay2 - ay1);
        var s2 = (bx2 - bx1) * (by2 - by1);
        var os = overlap(ax1, ay1, ax2, ay2, bx1, by1, bx2, by2);
        var ss = 0;
        ss += s1 + s2;
        ss -= os;
        return ss;
    }

    private int overlap(int ax1, int ay1, int ax2, int ay2, int bx1, int by1, int bx2, int by2) {
        var x1 = Math.max(ax1, bx1);
        var x2 = Math.min(ax2, bx2);
        var y1 = Math.max(ay1, by1);
        var y2 = Math.min(ay2, by2);
        if (x1 <= x2 && y1 <= y2) {
            return (x2 - x1) * (y2 - y1);
        }
        return 0;
    }
}