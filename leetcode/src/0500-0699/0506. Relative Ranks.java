class Solution {
    public String[] findRelativeRanks(int[] score) {
        var heap = new PriorityQueue<int[]>(
            (a, b) -> b[0] - a[0]
        );
        for (var i = 0; i < score.length; ++i) {
            heap.add(new int[] {
                score[i], i
            });
        }
        var answ = new String[score.length];
        for (var have = 1; heap.isEmpty() == false; ++have) {
            var elem = heap.poll();
            var indx = elem[1];
            if (have == 1)
                answ[indx] = "Gold Medal";
            else if (have == 2)
                answ[indx] = "Silver Medal";
            else if (have == 3)
                answ[indx] = "Bronze Medal";
            else
                answ[indx] = String.valueOf(have);
        }
        return answ;
    }
}