public class Solution {
    private readonly Solver solver;

    public Solution(int n, int[] blacklist) {
        if (n < 0) {
            solver = new Small(n, blacklist);
        } else {
            solver = new Large(n, blacklist);
        }
    }

    public int Pick() {
        return solver.Pick();
    }

    private class Small : Solver {
        private readonly Random rand;
        private readonly List<int> indx;

        public Small(int n, int[] blacklist) {
            this.rand = new Random(50847534);
            this.indx = new List<int>();
            var set = new HashSet<int>(blacklist);
            for (var i = 0; i < n; ++i) {
                if (set.Contains(i) == false) {
                    indx.Add(i);
                }
            }
        }

        public override int Pick() {
            var ptr = rand.Next(indx.Count);
            var res = indx[ptr];
            return res;
        }
    }

    private class Large : Solver {
        private readonly Random rand;
        private readonly List<Interval> ints;

        public Large(int n, int[] blacklist) {
            this.rand = new Random(50847534);
            this.ints = build(n, blacklist);
        }

        public override int Pick() {
            var ind = rand.Next(ints.Count);
            var ptr = rand.Next(ints[ind].hi - ints[ind].lo + 1);
            var res = ints[ind].lo + ptr;
            return res;
        }

        private List<Interval> build(int n, int[] blacklist){
            Array.Sort(blacklist);
            var res = new List<Interval>();
            var ptr = 0;
            for (var i = 0; i < blacklist.Length; ++i) {
                if (ptr < blacklist[i]) {
                    var lo = ptr;
                    var hi = blacklist[i] - 1;
                    res.Add(new Interval(lo, hi));
                }
                ptr = blacklist[i] + 1;
            }
            if (ptr < n) {
                var lo = ptr;
                var hi = n - 1;
                res.Add(new Interval(lo, hi));
            }
            return res;
        }

        private struct Interval {
            public readonly int lo;
            public readonly int hi;

            public Interval(int lo, int hi) {
                this.lo = lo;
                this.hi = hi;
            }

            public override string ToString() {
                return $"{{lo, hi}} = {{{lo}, {hi}}}";
            }
        }
    }

    private abstract class Solver {
        public abstract int Pick();
    }
}
