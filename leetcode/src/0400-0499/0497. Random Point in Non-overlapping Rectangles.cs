public class Solution {
    private readonly Random rand;
    private readonly int[][] rects;
    private readonly int numPoints;

    public Solution(int[][] rects) {
        this.rand = new Random(50847534);
        this.rects = rects;
        this.numPoints = 0;
        foreach (var rect in rects) {
            numPoints += sqr(rect);
        }
        Array.Sort(this.rects, (a, b) => {
            var sa = sqr(a);
            var sb = sqr(b);
            return sa.CompareTo(sb);
        });
    }

    public int[] Pick() {
        var indx = rand.Next(numPoints);
        foreach (var rect in rects) {
            var have = sqr(rect);
            if (indx > have) {
                indx = indx - have;
            } else {
                var x = rand.Next(rect[0], rect[2] + 1);
                var y = rand.Next(rect[1], rect[3] + 1);
                return new int[] { x, y };
            }
        }
        throw new Exception($"Unsupported index {indx}");
    }

    private int sqr(int[] rect) {
        var h = rect[2] - rect[0] + 1;
        var w = rect[3] - rect[1] + 1;
        return h * w;
    }
}