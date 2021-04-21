public class Solution {
    public long TotalCost(int[] costs, int k, int candidates) {
        return TotalCosts(costs, costs.Length, k, candidates);
    }

    private long TotalCosts(int[] costs, int n, int k, int c) {
        var heap = new PriorityQueue<int, (int, int)>(
            Comparer<(int cost, int type)>.Create((a, b) => {
                if (a.cost != b.cost) {
                    return a.cost - b.cost;
                } else {
                    return a.type - b.type;
                }
            })
        );
        int head = 0, tail = n - 1;
        while (head <= tail) {
            if (heap.Count < 1 * c) {
                heap.Enqueue(head, (costs[head], 0));
                head = head + 1;
            } else break;
        }
        while (tail >= head) {
            if (heap.Count < 2 * c) {
                heap.Enqueue(tail, (costs[tail], 1));
                tail = tail - 1;
            } else break;
        }
        var answ = 0L;
        for (var i = 0; i < k; ++i) {
            var node = heap.TryDequeue(out var elem, out (int cost, int type) prio);
            answ += prio.cost;
            if (head <= tail) {
                if (prio.type == 0) {
                    heap.Enqueue(head, (costs[head], 0));
                    head = head + 1;
                } else {
                    heap.Enqueue(tail, (costs[tail], 1));
                    tail = tail - 1;
                }
            }
        }
        return answ;
    }
}
