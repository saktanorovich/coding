public class Solution {
    private readonly Solver solver;

    public Solution(int[] nums) {
        if (nums.Length < 0) {
            solver = new Small(nums);
        } else {
            solver = new Large(nums);
        }
    }

    public int Pick(int target) {
        return solver.Pick(target);
    }

    private class Small : Solver {
        private readonly Random rand;
        private readonly int[] indx;
        private readonly int[] nums;

        public Small(int[] nums) {
            this.rand = new Random(50847534);
            this.indx = new int[nums.Length];
            this.nums = new int[nums.Length];
            for (var i = 0; i < nums.Length; ++i) {
                this.indx[i] = i;
                this.nums[i] = nums[i];
            }
            Array.Sort(this.nums, this.indx);
        }

        public override int Pick(int target) {
            var le = lPick(target);
            var ri = rPick(target);
            var pk = rand.Next(ri - le + 1);
            return indx[le + pk];
        }

        private int lPick(int target) {
            var lo = 0;
            var hi = nums.Length - 1;
            while (lo < hi) {
                var x = (lo + hi) / 2;
                if (nums[x] < target) {
                    lo = x + 1;
                } else {
                    hi = x;
                }
            }
            return lo;
        }

        private int rPick(int target) {
            var lo = 0;
            var hi = nums.Length - 1;
            while (lo < hi) {
                var x = (lo + hi + 1) / 2;
                if (nums[x] > target) {
                    hi = x - 1;
                } else {
                    lo = x;
                }
            }
            return hi;
        }
    }

    private class Large : Solver {
        private readonly Random rand;
        private readonly Dictionary<int, List<int>> indx;

        public Large(int[] nums) {
            this.rand = new Random(50847534);
            this.indx = new Dictionary<int, List<int>>();
            for (var i = 0; i < nums.Length; ++i) {
                var num = nums[i];
                if (this.indx.ContainsKey(num) == false) {
                    this.indx.Add(num, new List<int>());
                }
                this.indx[num].Add(i);
            }
        }

        public override int Pick(int target) {
            var len = indx[target].Count;
            var ptr = rand.Next(len);
            return indx[target][ptr];
        }
    }

    private abstract class Solver {
        public abstract int Pick(int target);
    }
}
