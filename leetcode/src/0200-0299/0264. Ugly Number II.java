class Solution {
    public int nthUglyNumber(int n) {
        var answ = 1L;
        var fact = new int[] { 2, 3, 5 };
        var seen = new HashSet<Long>();
        var heap = new PriorityQueue<Long>();
        heap.add(1L);
        for (; n > 0; --n) {
            var curr = heap.poll();
            for (var x : fact) {
                if (seen.add(curr * x)) {
                    heap.add(curr * x);
                }
            }
            answ = curr;
        }
        return (int)answ;
    }
}