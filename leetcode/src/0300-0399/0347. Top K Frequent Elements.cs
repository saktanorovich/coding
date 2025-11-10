public class Solution {
    public int[] TopKFrequent(int[] nums, int k) {
        var freq = new Dictionary<int, int>();
        foreach (var x in nums) {
            freq.TryAdd(x, 0);
            freq[x] ++;
        }
        var heap = new PriorityQueue<int, int>(
            Comparer<int>.Create((a, b) => {
                return a - b;
        }));
        foreach (var x in freq.Keys) {
            heap.Enqueue(x, freq[x]);
            if (heap.Count > k) {
                heap.Dequeue();
            }
        }
        var res = new int[k];
        for (var i = 0; i < k; ++i) {
            res[i] = heap.Dequeue();
        }
        return res;
    }
}
