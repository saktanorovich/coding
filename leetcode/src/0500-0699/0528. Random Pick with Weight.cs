public class Solution {
    private readonly Solver solver;

    public Solution(int[] w) {
        if (w.Length < 0) {
            solver = new Small(w);
        } else {
            solver = new Large(w);
        }
    }

    public int PickIndex() {
        return solver.PickIndex();
    }

    private class Small : Solver {
        private readonly Random rand;
        private readonly List<int> nums;

        public Small(int[] w) {
            this.rand = new Random(50847534);
            this.nums = new List<int>();
            for (var i = 0; i < w.Length; ++i) {
                for (var k = 0; k < w[i]; ++k) {
                    this.nums.Add(i);
                }
            }
        }

        public override int PickIndex() {
            var ptr = rand.Next(nums.Count);
            var res = nums[ptr];
            return res;
        }
    }

    private class Large : Solver {
        private readonly Random rand;
        private readonly int[] sums;
        private readonly int suma;

        public Large(int[] w) {
            this.rand = new Random(50847534);
            this.sums = new int[w.Length];
            this.suma = 0;
            for (var i = 0; i < w.Length; ++i) {
                suma += w[i];
                sums[i] = suma;
            }
        }

        public override int PickIndex() {
            var next = rand.Next(suma) + 1;
            var indx = pick(next);
            return indx;
        }

        private int pick(double target) {
            var lo = 0;
            var hi = sums.Length;
            while (lo < hi) {
                var x = (lo + hi) / 2;
                if (sums[x] < target) {
                    lo = x + 1;
                } else {
                    hi = x;
                }
            }
            return lo;
        }
    }

    private abstract class Solver {
        public abstract int PickIndex();
    }
}
