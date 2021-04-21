class Solution {
    public List<List<Integer>> kSmallestPairs(int[] nums1, int[] nums2, int k) {
        var heap = new PriorityQueue<int[]>(
            (a, b) -> a[0] - b[0]
        );
        for (var i = 0; i < nums1.length; ++i) {
            heap.add(new int[] { nums1[i] + nums2[0], i, 0 });
        }
        var answ = new ArrayList<List<Integer>>();
        for (; k > 0; --k) {
            var curr = heap.poll();
            var i1 = curr[1];
            var i2 = curr[2];
            answ.add(List.of(nums1[i1], nums2[i2]));
            if (i2 + 1 < nums2.length) {
                heap.add(new int[] {
                    nums1[i1] + nums2[i2 + 1], i1, i2 + 1
                });
            }
        }
        return answ;
    }
}