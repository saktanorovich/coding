using System;

namespace TopCoder.Algorithm {
    public class QuickSums {
        public int minSums(string numbers, int sum) {
            var min = new long[numbers.Length + 1, sum + 1];
            for (var target = 1; target <= sum; ++target) {
                min[0, target] = int.MaxValue;
            }
            for (var last = 1; last <= numbers.Length; ++last) {
                for (var target = 0; target <= sum; ++target) {
                    min[last, target] = int.MaxValue;
                    for (var take = 1; take <= last; ++take) {
                        var previous = target - long.Parse(numbers.Substring(last - take, take));
                        if (previous >= 0) {
                            min[last, target] = Math.Min(min[last, target], min[last - take, previous] + 1);
                        }
                    }
                }
            }
            if (min[numbers.Length, sum] < int.MaxValue)
                return (int)min[numbers.Length, sum] - 1;
            return -1;
        }
    }
}