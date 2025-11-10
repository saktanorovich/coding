public class Solution {
    public int EarliestFullBloom(int[] plan, int[] grow) {
        var flowers = new (int plan, int grow)[plan.Length];
        for (var i = 0; i < plan.Length; ++i) {
            flowers[i] = (plan[i], grow[i]);
        }
        Array.Sort(flowers, (a, b) => {
            if (a.grow != b.grow) {
                return a.grow - b.grow;
            } else {
                return a.plan - b.plan;
            }
        });
        var best = 0;
        foreach (var f in flowers) {
            best = Math.Max(best, f.grow) + f.plan;
        }
        return best;
    }
}
