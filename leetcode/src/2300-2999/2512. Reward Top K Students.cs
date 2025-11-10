public class Solution {
    public IList<int> TopStudents(
        string[] positive_feedback,
        string[] negative_feedback,
        string[] reports,
        int[] students,
        int k) {
        return TopStudents(
            positive_feedback.ToHashSet(),
            negative_feedback.ToHashSet(),
            reports,
            students,
            k
        );
    }

    private IList<int> TopStudents(
        HashSet<string> positive_feedback,
        HashSet<string> negative_feedback,
        string[] reports,
        int[] students,
        int k) {
        var rank = new Dictionary<int, int>();
        foreach (var stu in students) {
            rank[stu] = 0;
        }
        for (var i = 0; i < reports.Length; ++i) {
            foreach (var word in reports[i].Split(' ')) {
                if (positive_feedback.Contains(word)) {
                    rank[students[i]] += 3;
                }
                if (negative_feedback.Contains(word)) {
                    rank[students[i]] -= 1;
                }
            }
        }
        var heap = new PriorityQueue<int, (int, int)>(
            Comparer<(int indx, int rank)>.Create((a, b) => {
                if (a.rank != b.rank) {
                    return a.rank - b.rank;
                } else {
                    return b.indx - a.indx;
                }
            })
        );
        foreach (var stu in students) {
            heap.Enqueue(stu, (stu, rank[stu]));
            if (heap.Count > k) {
                heap.Dequeue();
            }
        }
        var res = new int[k];
        for ( ; heap.Count > 0; ) {
            res[heap.Count - 1] = heap.Dequeue();
        }
        return res;
    }
}
